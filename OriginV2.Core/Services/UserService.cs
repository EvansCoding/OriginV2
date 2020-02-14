using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Core.Models.Entities;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace OriginV2.Core.Services
{
    public class UserService : IUserService
    {
        private IDataContext context;

        public UserService(IDataContext _context)
        {
            context = _context;
        }

        public IEnumerable<User> PageList(string name, int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return context.Users
                    .Include(x => x.Account)
                    .Where(x => x.FullName.Contains(name))
                    .OrderBy(x => x.FullName).ToPagedList(page, pageSize);
            }
            return context.Users.OrderBy(x => x.FullName).ToPagedList(page, pageSize);
        }

        //public User GetUserByName(string name)
        //{
        //    try
        //    {
        //        return context.Users
        //            .Include(x => x.Account)
        //            .Include(x => x.Role)
        //            .Where(x => x.nam)
        //    }
        //    catch (System.Exception)
        //    {

        //    }

        //    return null;
        //}

        public User GetUserByID(string id)
        {
            try
            {
                return context.Users
                    .Include(x => x.Account)
                    .Where(x => x.Id == new Guid(id)).SingleOrDefault();
            }
            catch (Exception)
            { 
            }
            return null;
        }
    }
}
