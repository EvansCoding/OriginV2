namespace OriginV2.Core.Migrations
{
    using OriginV2.Core.Constants;
    using OriginV2.Core.Data.Context;
    using OriginV2.Core.Models.Entities;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<DataContext>
    {
        private readonly bool _pendingMigrations;
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            var migrator = new DbMigrator(this);

            _pendingMigrations = migrator.GetPendingMigrations().Any();

            if (_pendingMigrations)
            {
                migrator.Update();
                Seed(new DataContext());
            }
        }

        protected override void Seed(DataContext context)
        {
            //var adminRole = context.Roles.Where(x => x.Name == Constant.USER_ROLE_ADMIN).SingleOrDefault();
            //if(adminRole == null)
            //{
            //    adminRole = new Role()
            //    {
            //        Name = Constant.USER_ROLE_ADMIN,
            //        CreateAt = DateTime.Now,
            //        UpdateAt = DateTime.Now
            //    };

            //    context.Roles.Add(adminRole);
            //}

            //var supplierRole = context.Roles.Where(x => x.Name == Constant.USER_ROLE_SUPPLIER).SingleOrDefault();
            //if (supplierRole == null)
            //{
            //    supplierRole = new Role()
            //    {
            //        Name = Constant.USER_ROLE_SUPPLIER,
            //        CreateAt = DateTime.Now,
            //        UpdateAt = DateTime.Now
            //    };

            //    context.Roles.Add(supplierRole);
            //}



            //var adminAccount = context.Accounts.Where(x => x.Username == Constant.ACCOUNT_ADMIN).SingleOrDefault();
            //if(adminAccount == null)
            //{
            //    adminAccount = new Account()
            //    {
            //        Username = Constant.ACCOUNT_ADMIN,
            //        Password = Constant.ACCOUNT_ADMIN
            //    };
            //    context.Accounts.Add(adminAccount);
            //}

            //var admin = new User()
            //{
            //    FullName = "Administrator",
            //    Email = "admin@gmail.com",
            //    CreateAt = DateTime.Now,
            //    UpdateAt = DateTime.Now,
            //};
            //admin.Role = adminRole;
            //admin.Account = adminAccount;
            //context.Users.Add(admin);
            //var supplierAccount = context.Accounts.Where(x => x.Username == Constant.ACCOUNT_SUPPLIER).SingleOrDefault();
            //if (supplierAccount == null)
            //{
            //    supplierAccount = new Account()
            //    {
            //        Username = Constant.ACCOUNT_SUPPLIER,
            //        Password = Constant.ACCOUNT_SUPPLIER
            //    };
            //    context.Accounts.Add(supplierAccount);
            //}

            //var supplier = new Supplier()
            //{
            //    Name = "Quốc Vinh",
            //    Address = "Cao Lãnh, Đồng Tháp",
            //    CreateAt = DateTime.Now,
            //    UpdateAt = DateTime.Now,
            //    Account = supplierAccount,
            //    Role = supplierRole
            //};

            //context.Suppliers.Add(supplier);


            //context.SaveChanges();
        }
    }
}
