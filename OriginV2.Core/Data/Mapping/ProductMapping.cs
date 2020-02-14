using OriginV2.Core.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OriginV2.Core.Data.Mapping
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public ProductMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired();
            Property(x => x.ImageOne).IsRequired();
            Property(x => x.ImageTwo).IsRequired();
            Property(x => x.Origin).IsRequired();
            Property(x => x.Attribute).IsRequired();
            Property(x => x.QRHashCode).IsOptional();
            Property(x => x.Technology).IsRequired();
            Property(x => x.Publish).IsRequired();
            Property(x => x.HarvestAt).IsRequired();
            Property(x => x.CreateAt).IsRequired();
            Property(x => x.Parent).IsOptional();
        }
    }
}
