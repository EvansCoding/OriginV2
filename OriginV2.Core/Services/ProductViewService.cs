using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Core.Models.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace OriginV2.Core.Services
{
   public class ProductViewService : IProductViewService
    {
        public IDataContext context;
        public ProductViewService(IDataContext context)
        {
            this.context = context;
        }

        public IEnumerable<ProductView> PageList(Guid supplierID,string name, int page, int pageSize)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    return context.ProductViews
                        .Include(x => x.Supplier)
                        .Where(x => x.Name.Contains(name) && x.Supplier.Id == supplierID)
                        .OrderBy(x => x.Name).ToPagedList(page, pageSize);
                }
                return context.ProductViews.Where(x => x.Supplier.Id == supplierID).OrderBy(x => x.Name).ToPagedList(page, pageSize);
            }
            catch (Exception)
            {
            }
            return null;
        }

        public IEnumerable<ProductView> PageListAdmin(string search, string supplier, int page, int pageSize)
        {
            if (string.IsNullOrEmpty(search) && string.IsNullOrEmpty(supplier))
            {
                return context.ProductViews.OrderBy(x => x.Name).ToPagedList(page, pageSize);
            }

            if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(supplier))
            {
                try
                {
                    return context.ProductViews.Where(x => x.Name.Contains(search) && x.Supplier.Id == new Guid(supplier)).OrderBy(x => x.Name).ToPagedList(page, pageSize);
                }
                catch (Exception)
                {
                }
            }
            if (!string.IsNullOrEmpty(search) || !string.IsNullOrEmpty(supplier) )
            {
                var productViews = context.ProductViews.OrderBy(x => x.Name).ToList();

                if (!string.IsNullOrEmpty(search))
                    productViews = productViews.Where(x => x.Name.Contains(search)).ToList();

                if (!string.IsNullOrEmpty(supplier))
                {
                    try
                    {
                        productViews = productViews.Where(x => x.Supplier.Id == new Guid(supplier)).ToList();
                    }
                    catch (Exception)
                    {
                    }
                }
                return productViews.ToPagedList(page, pageSize);
            }
            return context.ProductViews.OrderBy(x => x.Name).ToPagedList(page, pageSize);
        }

        public ProductView GetProductViewByID(string id)
        {
            try
            {
                return context.ProductViews
                    .Include(x => x.Supplier)
                    .Where(x => x.Id == new Guid(id)).SingleOrDefault();
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}
