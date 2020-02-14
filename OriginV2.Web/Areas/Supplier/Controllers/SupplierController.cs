using OriginV2.Core.Constants;
using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Web.Common;
using OriginV2.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OriginV2.Web.Areas.Supplier.Controllers
{
    public class SupplierController : BaseController
    {
        public readonly IAccountService accountService;
        public readonly ISupplierService supplierService;
        public SupplierController(IAccountService accountService, ISupplierService supplierService, IDataContext context) : base(context)
        {
            this.accountService = accountService;
            this.supplierService = supplierService;
        }

        public ActionResult SupplierView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProfileView()
        {
            var session = (SupplierLogin)Session[Constant.SUPPLIER_SESSION];
            if (session != null)
            {
                var data = accountService.GetAccountByID(session.UserID.ToString());
                if (data == null) return View("ProfileView", new SupplierViewModel());
                else
                {
                    var model = new SupplierViewModel();
                    model.Id = data.Supplier.Id.ToString();
                    model.Name = data.Supplier.Name;
                    model.Address = data.Supplier.Address;
                    model.Password = "";
                    model.PathImage = data.Supplier.PathImage;
                    model.Username = data.Username;
                    model.AccountID = data.Id.ToString();
                    return View("ProfileView", model);
                }
            }
            return View("ProfileView", new SupplierViewModel());
        }

        [HttpPost]
        public ActionResult UpdateProfile(SupplierViewModel model)
        {
            bool checkUpdate = true;
            if (!model.Id.Equals(""))
            {
                var account = accountService.GetAccountByID(model.AccountID.ToString());
                if (account != null)
                {
                    if (model.Name != null && model.Address != null && model.PathImage != null)
                    {
                        account.Supplier.Name = model.Name;
                        account.Supplier.Address     = model.Address;
                        account.Supplier.PathImage = model.PathImage;
                    }
                    else checkUpdate = false;

                    if (model.Password != null)
                        account.Password = model.Password;


                    if (!account.Username.Equals(model.Username) && !model.Username.Equals(""))
                    {
                        var listCount = context.Accounts.Where(x => x.Username.Equals(model.Username)).ToList();
                        if (listCount.Count == 0)
                        {
                            account.Username = model.Username;
                        }
                        else
                        {
                            TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                            {
                                Message = "Tên người dùng đã tồn tại",
                                MessageType = GenericMessages.error
                            };
                            return RedirectToAction("ProfileView", "Supplier", new { Area = "Supplier" });
                        }
                    }
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (System.Exception)
            {

                checkUpdate = false;
            }

            if (checkUpdate)
            {

                TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                {
                    Message = "Cập nhật thông tin thành công",
                    MessageType = GenericMessages.success
                };
                return RedirectToAction("ProfileView", "Supplier", new { Area = "Supplier" });
            }
            TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
            {
                Message = "Cập nhật thông tin thất bại",
                MessageType = GenericMessages.error
            };
            return RedirectToAction("ProfileView", "Supplier", new { Area = "Supplier" });
        }

    }
}