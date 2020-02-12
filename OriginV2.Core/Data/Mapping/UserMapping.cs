using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using OriginV2.Core.Models.Entities;

namespace OriginV2.Core.Data.Mapping
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.FullName).IsRequired().HasMaxLength(500);
            Property(x => x.CreateAt).IsRequired();
            Property(x => x.UpdateAt).IsOptional();
            Property(x => x.PathImage).IsOptional();

            HasMany(x => x.Products)
                .WithRequired(x => x.User)
                .Map(x => x.MapKey("User_Id"))
                .WillCascadeOnDelete(false);
        }
    }
}
