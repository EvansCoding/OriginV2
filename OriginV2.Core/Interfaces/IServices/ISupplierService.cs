using OriginV2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginV2.Core.Interfaces.IServices
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> PageList(string name, int page, int pageSize);
        Supplier GetSupplierByID(string id);
        List<Supplier> GetSuppliers();
    }
}
