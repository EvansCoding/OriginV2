using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using OriginV2.Core.Models.Entities;

namespace OriginV2.Core.Data.Mapping
{
    public class ProductViewMapping : EntityTypeConfiguration<ProductView>
    {
        public ProductViewMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).IsRequired();
            Property(x => x.ImageOne).IsRequired();
            Property(x => x.ImageTwo).IsRequired();
            Property(x => x.Origin).IsRequired();
            Property(x => x.Attribute).IsRequired();
            Property(x => x.Technology).IsRequired();
            Property(x => x.QRHashCode).IsOptional();
            Property(x => x.HarvestAt).IsRequired();
            Property(x => x.CreateAt).IsRequired();
            Property(x => x.UpdateAt).IsOptional();
        }
    }
}
