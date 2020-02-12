using Microsoft.Security.Application;
using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Core.Models.Entities;
using OriginV2.Core.Models.Enums;
using System;
using System.Data.Entity;
using System.Linq;
namespace OriginV2.Core.Services
{
    public class AccountService : IAccountService
    {
        private IDataContext context;

        public AccountService(IDataContext _context)
        {
            context = _context;
        }

        public Account GetAccountByUserName(string username)
        {
            return context.Accounts
                .Include(x => x.User)
                .Include(x => x.Supplier)
                .Where(x => x.Username == username).SingleOrDefault();
        }

        public LoginAttemptStatus LastLoginStatus { get; private set; } = LoginAttemptStatus.LoginSuccessful;
        public bool ValidateUser(string username, string password)
        {
            username = Sanitizer.GetSafeHtmlFragment(username);
            password = Sanitizer.GetSafeHtmlFragment(password);

            LastLoginStatus = LoginAttemptStatus.LoginSuccessful;

            var account = GetAccountByUserName(username);
            if (account == null)
            {
                LastLoginStatus = LoginAttemptStatus.UserNotFound;
                return false;
            }

            var passwordMatches = password == account.Password;
            if (!passwordMatches)
            {
                LastLoginStatus = LoginAttemptStatus.PasswordIncorrect;
                return false;
            }

            return LastLoginStatus == LoginAttemptStatus.LoginSuccessful;
        }
        public Account GetAccountByID(string id)
        {
            try
            {
                return context.Accounts
                    .Include(x => x.User)
                    .Where(x => x.Id == new Guid(id)).SingleOrDefault();
            }
            catch (Exception)
            {
            }
            return null;
        }

        public Account GetAccountByIDName(string id, string username)
        {
            try
            {
                return context.Accounts
                    .Include(x => x.User)
                    .Where(x => x.Id == new Guid(id) && x.Username == username).SingleOrDefault();
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}
