using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Core.Models.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace OriginV2.Core.Services
{
    public class SupplierService : ISupplierService
    {
        private IDataContext context;

        public SupplierService(IDataContext _context)
        {
            context = _context;
        }

        public IEnumerable<Supplier> PageList(string name, int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return context.Suppliers
                    .Include(x => x.Account)
                    .Where(x => x.Name.Contains(name))
                    .OrderBy(x => x.Name).ToPagedList(page, pageSize);
            }
            return context.Suppliers.OrderBy(x => x.Name).ToPagedList(page, pageSize);
        }

        public Supplier GetSupplierByID(string id)
        {
            try
            {
                return context.Suppliers
                    .Include(x => x.Account)
                    .Where(x => x.Id == new Guid(id)).SingleOrDefault();
            }
            catch (Exception)
            {
            }
            return null;
        }
        public List<Supplier> GetSuppliers()
        {
            return context.Suppliers.ToList();
        }
    }
}
