using OriginV2.Core.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OriginV2.Core.Data.Mapping
{
    public class SupplierMapping : EntityTypeConfiguration<Supplier>
    {
        public SupplierMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(500);
            Property(x => x.Address).IsRequired().HasMaxLength(500);
            Property(x => x.CreateAt).IsRequired();
            Property(x => x.UpdateAt).IsOptional();
            Property(x => x.PathImage).IsOptional();

            HasMany(x => x.Products)
                .WithRequired(x => x.Supplier)
                .Map(x => x.MapKey("Supplier_Id"))
                .WillCascadeOnDelete(false);

            HasMany(x => x.ProductViews)
                .WithRequired(x => x.Supplier)
                .Map(x => x.MapKey("Supplier_Id"))
                .WillCascadeOnDelete(false);
        }
    }
}
