using OriginV2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginV2.Core.Interfaces.IServices
{
    public interface IProductViewService
    {
        IEnumerable<ProductView> PageList(Guid supplierID, string name, int page, int pageSize);
        IEnumerable<ProductView> PageListAdmin(string search, string supplier, int page, int pageSize);
        ProductView GetProductViewByID(string id);
    }
}
