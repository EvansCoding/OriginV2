using OriginV2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginV2.Core.Interfaces.IServices
{
    public interface IRoleService
    {
        Role GetRoleByID(string id);
        List<Role> GetRoles();
        Role GetRoleByName(string name);
        List<Role> GetRolesByName(string name);
        IEnumerable<Role> PageList(string name, int page, int pageSize);
    }
}
