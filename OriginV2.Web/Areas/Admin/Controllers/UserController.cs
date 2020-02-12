using OriginV2.Core.Constants;
using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Core.Models.Entities;
using OriginV2.Web.Common;
using OriginV2.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OriginV2.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        public readonly IAccountService accountService;
        public readonly IRoleService roleService;
        public readonly IUserService userService;
        public UserController(IAccountService accountService, IRoleService roleService, IUserService userService, IDataContext context) : base(context)
        {
            this.accountService = accountService;
            this.roleService = roleService;
            this.userService = userService;
        }

        public ActionResult UserView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProfileView()
        {
            var session = (UserLogin)Session[Constant.USER_SESSION];
            if (session != null)
            {
                var data = accountService.GetAccountByID(session.UserID.ToString());
                if (data == null) return View("ProfileView", new UserViewModel());
                else
                {
                    var model = new UserViewModel();
                    model.Id = data.User.Id.ToString();
                    model.FullName = data.User.FullName;
                    model.Email = data.User.Email;
                    model.Password = "";
                    model.PathImage = data.User.PathImage;
                    model.Username = data.Username;
                    model.AccountID = data.Id.ToString();
                    return View("ProfileView", model);
                }
            }
            return View("ProfileView", new UserViewModel());

        }

        public ActionResult UpdateProfile(UserViewModel model)
        {
            bool checkUpdate = true;
            if (!model.Id.Equals(""))
            {
                var account = accountService.GetAccountByID(model.AccountID.ToString());
                if (account != null)
                {
                    if (model.FullName != null && model.Email != null && model.PathImage != null)
                    {
                        account.User.FullName = model.FullName;
                        account.User.Email = model.Email;
                        account.User.PathImage = model.PathImage;
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
                            return RedirectToAction("ProfileView", "User", new { Area = "Admin" });
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
                return RedirectToAction("ProfileView", "User", new { Area = "Admin" });
            }
            TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
            {
                Message = "Cập nhật thông tin thất bại",
                MessageType = GenericMessages.error
            };
            return RedirectToAction("ProfileView", "User", new { Area = "Admin" });
        }

        [HttpGet]
        public ActionResult LoadTable(string search, string category, string state, int page = 1, int pageSize = 10)
        {
            var model = userService.PageList(search, page, pageSize);
            return PartialView("UserTablePartialView", model);
        }

        public ActionResult AddOrEdit(string Id = "")
        {
            List<Role> ListRoles = roleService.GetRoles();
            if (Id.Equals(""))
            {
                ViewBag.ListOfRoles = new SelectList(ListRoles, "Id", "Name");
                return PartialView("CRUDUser", new UserViewModel());
            }
            else
            {

                var data = userService.GetUserByID(Id);
                if (data == null) return View("UserView", new UserViewModel());
                else
                {
                    var model = new UserViewModel();
                    model.Id = data.Id.ToString();
                    model.FullName = data.FullName;
                    model.Email = data.Email;
                    model.PathImage = data.PathImage;
                    model.Username = data.Account.Username;
                    model.Password = data.Account.Password;
                    model.AccountID = data.Id.ToString();
                    if (data.Role == null)
                    {
                        ViewBag.ListOfRoles = new SelectList(ListRoles, "Id", "Name");
                    }
                    else
                    {
                        ViewBag.ListOfRoles = new SelectList(ListRoles, "Id", "Name", data.Role.Id);
                    }
                    return PartialView("CRUDUser", model);
                }
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddOrUpdate(UserViewModel model)
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
                    return RedirectToAction("UserView", "User");
                }
                else
                {
                    User user = new User()
                    {
                        FullName = model.FullName,
                        Email = model.Email,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        PathImage = model.PathImage,
                    };
                    try
                    {
                        user.Role = context.Roles.Where(x => x.Id == new Guid(model.RoleID)).SingleOrDefault();
                        var act = new Account() { Username = model.Username, Password = model.Password };
                        context.Accounts.Add(act);

                        user.Account = act;
                        context.Users.Add(user);
                        context.SaveChanges();

                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Thêm người dùng thành công",
                            MessageType = GenericMessages.success
                        };
                        return RedirectToAction("UserView", "User");
                    }
                    catch (Exception)
                    {

                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Thêm người dùng thất bại",
                            MessageType = GenericMessages.error
                        };
                        return RedirectToAction("UserView", "User");
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
                            return RedirectToAction("UserView", "User");
                        }
                    }

                    account.User.FullName = model.FullName;
                    account.User.Email = model.Email;
                    account.User.PathImage = model.PathImage;
                    account.Password = model.Password;
                    account.User.UpdateAt = DateTime.Now;
                    try
                    {
                        account.User.Role = context.Roles.Where(x => x.Id == new Guid(model.RoleID)).SingleOrDefault();

                        context.SaveChanges();
                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Cập nhật người dùng thành công",
                            MessageType = GenericMessages.success
                        };
                        return RedirectToAction("UserView", "User");
                    }
                    catch (Exception)
                    {
                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Cập nhật người dùng thất bại",
                            MessageType = GenericMessages.error
                        };
                        return RedirectToAction("UserView", "User");
                    }
                }
            }

            TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
            {
                Message = "Đã xảy ra lỗi",
                MessageType = GenericMessages.error
            };
            return RedirectToAction("UserView", "User");
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
                    var user = context.Users.Where(x => x.Id == new Guid(Id)).SingleOrDefault();
                    var account = context.Accounts.Where(x => x.Id == user.Account.Id).SingleOrDefault();
                    context.Users.Remove(user);
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