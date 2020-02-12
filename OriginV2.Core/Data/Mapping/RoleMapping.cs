using OriginV2.Core.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OriginV2.Core.Data.Mapping
{
    public class RoleMapping : EntityTypeConfiguration<Role>
    {
        public RoleMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired();
            Property(x => x.CreateAt).IsRequired();
            Property(x => x.UpdateAt).IsOptional();

            HasMany(x => x.Suppliers)
                .WithRequired(x => x.Role)
                .Map(x => x.MapKey("Role_Id"))
                .WillCascadeOnDelete(false);

            HasMany(x => x.Users)
                .WithRequired(x => x.Role)
                .Map(x => x.MapKey("Role_Id"))
                .WillCascadeOnDelete(false);
        }
    }
}
