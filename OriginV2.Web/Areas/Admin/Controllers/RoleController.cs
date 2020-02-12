using OriginV2.Core.Constants;
using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Core.Models.Entities;
using OriginV2.Web.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OriginV2.Web.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        public readonly IRoleService roleService;
        public RoleController(IRoleService roleService, IDataContext context) : base(context)
        {
            this.roleService = roleService;
        }

        [HttpGet]
        public ActionResult RoleView()
        {
            return View();
        }

        public ActionResult AddOrEdit(string Id = "")
        {
            if (Id.Equals(""))
            {
                return PartialView("CRUDRole", new RoleViewModel());
            }
            else
            {
                try
                {
                    var data = roleService.GetRoleByID(Id);
                    if (data == null) return PartialView("CRUDRole", new RoleViewModel());
                    else
                    {
                        var model = new RoleViewModel();
                        model.Id = data.Id.ToString();
                        model.Name = data.Name;
                        model.CreateAt = data.CreateAt;
                        model.UpdateAt = data.UpdateAt;
                        return PartialView("CRUDRole", model);
                    }
                }
                catch (Exception)
                {
                }
                return PartialView("CRUDRole", new RoleViewModel());
            }
        }

        [HttpGet]
        public ActionResult LoadTable(string search, int page = 1, int pageSize = 10)
        {
            var model = roleService.PageList(search, page, pageSize);
            return PartialView("RoleTablePartialView", model);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(RoleViewModel model)
        {
            if (model.Id == null)
            {
                if (roleService.GetRoleByName(model.Name) != null)
                {
                    TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Quyền đã tồn tại",
                        MessageType = GenericMessages.success
                    };
                    return RedirectToAction("RoleView", "Role");
                }
                else
                {
                    Role role = new Role()
                    {
                        Name = model.Name,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now
                    };

                    try
                    {
                        context.Roles.Add(role);
                        context.SaveChanges();
                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Thêm quyền thành công",
                            MessageType = GenericMessages.success
                        };
                        return RedirectToAction("RoleView", "Role");
                    }
                    catch (Exception)
                    {
                    }

                    TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Thêm thất bại",
                        MessageType = GenericMessages.error
                    };
                    return RedirectToAction("RoleView", "Role");
                }
            }
            else
            {
                if (roleService.GetRolesByName(model.Name).Count > 1)
                {
                    TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Tên quyền đã tồn tại",
                        MessageType = GenericMessages.error
                    };
                    return RedirectToAction("RoleView", "Role");
                }
                else
                {
                    var role = roleService.GetRoleByID(model.Id);
                    if (role == null)
                    {
                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Quyền không tồn tại",
                            MessageType = GenericMessages.error
                        };
                        return RedirectToAction("RoleView", "Role");
                    }
                    else
                    {
                        role.Name = model.Name;
                        role.UpdateAt = DateTime.Now;
                        try
                        {
                            context.SaveChanges();
                            TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                            {
                                Message = "Cập nhật quyền thành công",
                                MessageType = GenericMessages.success
                            };
                            return RedirectToAction("RoleView", "Role");
                        }
                        catch (Exception)
                        {
                        }
                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Cập nhật quyền thất bại",
                            MessageType = GenericMessages.error
                        };
                        return RedirectToAction("RoleView", "Role");
                    }
                }
            }
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
                    var role = context.Roles.Where(x => x.Id == new Guid(Id)).SingleOrDefault();
                    context.Roles.Remove(role);
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