using OriginV2.Core.Models.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace OriginV2.Core.Interfaces
{
    public partial interface IDataContext : IDisposable
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductView> ProductViews { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
        void RollBack();
    }
}
