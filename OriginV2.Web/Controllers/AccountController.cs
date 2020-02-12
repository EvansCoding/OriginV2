using OriginV2.Core.Constants;
using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Web.Common;
using OriginV2.Web.ViewModels;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace OriginV2.Web.Controllers
{
    public class AccountController : Controller
    {
        public readonly IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn(LogOnViewModel model)
        {
            var username = model.Username;
            var passwork = model.Password;
            if (ModelState.IsValid)
            {
                if (accountService.ValidateUser(username, passwork))
                {
                    FormsAuthentication.SetAuthCookie(username, true);
                    var account = accountService.GetAccountByUserName(username);
                    var userSession = new UserLogin();
                    userSession.UserID = account.Id;
                    userSession.Username = account.Username;
                    Session.Add(Constant.USER_SESSION, userSession);
                    if(account.User != null)
                    {
                        return RedirectToAction("ProfileView", "User", new { Area = "Admin" });
                    }

                    if (account.Supplier != null)
                    {
                        return RedirectToAction("ProfileView", "User", new { Area = "Supplier" });
                    }
                }
            }
            return RedirectToAction("Index", "Account", new { Area = String.Empty });
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Account", new { Area = String.Empty });
        }
    }
}