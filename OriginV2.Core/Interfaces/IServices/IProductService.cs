using OriginV2.Core.Models.Entities;
using System.Collections.Generic;

namespace OriginV2.Core.Interfaces.IServices
{
    public interface IProductService
    {
        Product GetParent();
        Product GetProduct(string id);
        Product GetProductByHashCode(string hash);
        IEnumerable<Product> PageListAdmin(string search, string supplier, int page, int pageSize);
    }
}
