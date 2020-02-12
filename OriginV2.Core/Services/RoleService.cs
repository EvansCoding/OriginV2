using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Core.Models.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginV2.Core.Services
{
    public class RoleService : IRoleService
    {
        private IDataContext context;
        public RoleService(IDataContext context)
        {
            this.context = context;
        }

        public Role GetRoleByID(string id)
        {
            try
            {
                return context.Roles.Where(x => x.Id == new Guid(id)).SingleOrDefault();
            }
            catch (Exception)
            {
            }
            return null;
        }

        public IEnumerable<Role> PageList(string name, int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return context.Roles.Where(x => x.Name.Contains(name)).OrderBy(x => x.Name).ToPagedList(page, pageSize);
            }
            return context.Roles.OrderBy(x => x.Name).ToPagedList(page, pageSize);
        }

        public Role GetRoleByName(string name)
        {
            try
            {
                return context.Roles
                       .Where(x => x.Name == name)
                       .Select(x => x).SingleOrDefault();
            }
            catch (Exception)
            {

            }
            return null;
        }
        public List<Role> GetRolesByName(string name)
        {
            try
            {
                return context.Roles
                       .Where(x => x.Name == name)
                       .Select(x => x).ToList();
            }
            catch (Exception)
            {

            }
            return null;
        }
        public List<Role> GetRoles()
        {
            try
            {
                return context.Roles
                    .Select(x => x).ToList();
            }
            catch (Exception)
            {

            }
            return null;
        }
    }
}
