using OriginV2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginV2.Core.Interfaces.IServices
{
    public interface IUserService
    {
        IEnumerable<User> PageList(string name, int page, int pageSize);
        //User GetUserByName(string name);
        User GetUserByID(string id);
    }
}
