using OriginV2.Core.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OriginV2.Core.Data.Mapping
{
    public class AccountMapping : EntityTypeConfiguration<Account>
    {
        public AccountMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Username).IsRequired();
            Property(x => x.Password).IsRequired();
            
            HasOptional(x => x.Supplier)
                .WithOptionalPrincipal(x => x.Account)
                .Map(x => x.MapKey("Account_Id"))
                .WillCascadeOnDelete(false);

            HasOptional(x => x.User)
                .WithOptionalPrincipal(x => x.Account)
                .Map(x => x.MapKey("Account_Id"))
                .WillCascadeOnDelete(false);
        }
    }
}
