using OriginV2.Core.Constants;
using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OriginV2.Core.Models.Entities;
using OriginV2.Web.Common;

namespace OriginV2.Web.Areas.Admin.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly ISupplierService supplierService;
        private readonly IAccountService accountService;

        public SupplierController(ISupplierService supplierService, IAccountService accountService, IDataContext context) : base(context)
        {
            this.supplierService = supplierService;
            this.accountService = accountService;
        }

        // GET: Admin/Supplier
        public ActionResult SupplierView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadTable(string search, string category, string state, int page = 1, int pageSize = 10)
        {
            var model = supplierService.PageList(search, page, pageSize);
            return PartialView("SupplierTablePartialView", model);
        }

        public ActionResult AddOrEdit(string Id = "")
        {
            if (Id.Equals(""))
            {
                return PartialView("CRUDSupplier", new SupplierViewModel());
            }
            else
            {

                var data = supplierService.GetSupplierByID(Id);
                if (data == null) return View("CRUDSupplier", new SupplierViewModel());
                else
                {
                    var model = new SupplierViewModel();
                    model.Id = data.Id.ToString();
                    model.Name = data.Name;
                    model.Address = data.Address;
                    model.PathImage = data.PathImage;
                    model.Username = data.Account.Username;
                    model.Password = data.Account.Password;
                    model.AccountID = data.Id.ToString();
                    return PartialView("CRUDSupplier", model);
                }
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddOrUpdate(SupplierViewModel model)
        {
            if (model.Id == null)
            {
                var account = accountService.GetAccountByUserName(model.Username);
                if (account != null)
                {
                    TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Tài khoản đã tồn tại",
                        MessageType = GenericMessages.error
                    };
                    return RedirectToAction("SupplierView", "Supplier");
                }
                else
                {
                    Core.Models.Entities.Supplier supplier = new Core.Models.Entities.Supplier()
                    {
                        Name = model.Name,
                        Address = model.Address,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        PathImage = model.PathImage
                    };
                    try
                    {
                        var act = new Account() { Username = model.Username, Password = model.Password };
                        context.Accounts.Add(act);

                        supplier.Account = act;
                        context.Suppliers.Add(supplier);
                        context.SaveChanges();

                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Thêm người dùng thành công",
                            MessageType = GenericMessages.success
                        };
                        return RedirectToAction("SupplierView", "Supplier");
                    }
                    catch (Exception)
                    {

                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Thêm người dùng thất bại",
                            MessageType = GenericMessages.error
                        };
                        return RedirectToAction("SupplierView", "Supplier");
                    }
                }
            }
            else
            {
                var account = accountService.GetAccountByUserName(model.Username);
                if (account != null)
                {
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
                            return RedirectToAction("SupplierView", "Supplier");
                        }
                    }

                    account.Supplier.Name = model.Name;
                    account.Supplier.Address = model.Address;
                    account.Supplier.PathImage = model.PathImage;
                    account.Password = model.Password;
                    account.Supplier.UpdateAt = DateTime.Now;
                    try
                    {

                        context.SaveChanges();
                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Cập nhật người dùng thành công",
                            MessageType = GenericMessages.success
                        };
                        return RedirectToAction("SupplierView", "Supplier");
                    }
                    catch (Exception)
                    {
                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Cập nhật người dùng thất bại",
                            MessageType = GenericMessages.error
                        };
                        return RedirectToAction("SupplierView", "Supplier");
                    }
                }
            }

            TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
            {
                Message = "Đã xảy ra lỗi",
                MessageType = GenericMessages.error
            };
            return RedirectToAction("SupplierView", "Supplier");
        }

        [HttpPost]
        public ActionResult Delete(string Id)
        {
            if (Id.Equals(""))
            {
                return Json(new { success = false, message = "Không tìm thấy!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    var supplier = context.Suppliers.Where(x => x.Id == new Guid(Id)).SingleOrDefault();
                    var account = context.Accounts.Where(x => x.Id == supplier.Account.Id).SingleOrDefault();
                    context.Suppliers.Remove(supplier);
                    context.Accounts.Remove(account);
                    context.SaveChanges();
                    return Json(new { success = true, message = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                }

                return Json(new { success = false, message = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}