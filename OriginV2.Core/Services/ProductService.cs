using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PagedList;

namespace OriginV2.Core.Services
{
    public class ProductService : IProductService
    {
        private IDataContext context;
        public ProductService(IDataContext context)
        {
            this.context = context;
        }

        public Product GetParent()
        {
            return context.Products.OrderByDescending(x => x.CreateAt).Take(1).SingleOrDefault();
        }
        public Product GetProductByHashCode(string hash)
        {
            try
            {
                return context.Products
                    .Include(x => x.Supplier)
                    .Where(x => x.QRHashCode == hash).SingleOrDefault();
            }
            catch (Exception)
            {
            }
            return null;
        }
        public Product GetProduct(string id)
        {
            try
            {
                return context.Products
                    .Include(x => x.Supplier)
                    .Where(x => x.Id == new Guid(id)).SingleOrDefault();
            }
            catch (Exception)
            {
            }
            return null;
        }

        public IEnumerable<Product> PageListAdmin(string search, string supplier, int page, int pageSize)
        {
            if (string.IsNullOrEmpty(search) && string.IsNullOrEmpty(supplier))
            {
                return context.Products.OrderBy(x => x.Name).ToPagedList(page, pageSize);
            }

            if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(supplier))
            {
                try
                {
                    return context.Products.Where(x => x.Name.Contains(search) && x.Supplier.Id == new Guid(supplier)).OrderBy(x => x.Name).ToPagedList(page, pageSize);
                }
                catch (Exception)
                {
                }
            }
            if (!string.IsNullOrEmpty(search) || !string.IsNullOrEmpty(supplier))
            {
                var products = context.Products.OrderBy(x => x.Name).ToList();

                if (!string.IsNullOrEmpty(search))
                    products = products.Where(x => x.Name.Contains(search)).ToList();

                if (!string.IsNullOrEmpty(supplier))
                {
                    try
                    {
                        products = products.Where(x => x.Supplier.Id == new Guid(supplier)).ToList();
                    }
                    catch (Exception)
                    {
                    }
                }
                return products.ToPagedList(page, pageSize);
            }
            return context.Products.OrderBy(x => x.Name).ToPagedList(page, pageSize);
        }

    }
}
