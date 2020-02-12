namespace OriginV2.Core.Data.Context
{
    using Interfaces;
    using Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    public class DataContext : DbContext, IDataContext
    {
        public DataContext()
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductView > ProductViews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }  
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            try
            {
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        /// <inheritdoc />
        public void RollBack()
        {
            foreach (var entry in base.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                               && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
