using OriginV2.Core.Models.Entities;

namespace OriginV2.Core.Interfaces.IServices
{
    public interface IAccountService
    {
        bool ValidateUser(string username, string password);
        Account GetAccountByUserName(string username);
        Account GetAccountByID(string id);
        Account GetAccountByIDName(string id, string username);
    }
}
