using OriginV2.Core.Constants;
using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Core.Utilities;
using OriginV2.Web.Common;
using OriginV2.Web.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OriginV2.Web.Areas.Supplier.Controllers
{
    public class ProductViewController : BaseController
    {
        public readonly IProductViewService productViewService;
        public readonly ISupplierService supplierService;
        public readonly IAccountService accountService;
        public ProductViewController(IAccountService accountService, IProductViewService productViewService, ISupplierService supplierService, IDataContext context) : base(context)
        {
            this.productViewService = productViewService;
            this.supplierService = supplierService;
            this.accountService = accountService;
        }
        // GET: Supplier/ProductView
        public ActionResult ProductView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadTable(string search, string category, string state, int page = 1, int pageSize = 10)
        {
            var session = (SupplierLogin)Session[Constant.SUPPLIER_SESSION];
            var account = accountService.GetAccountByID(session.UserID.ToString());
            var model = productViewService.PageList(account.Supplier.Id, search, page, pageSize);
            return PartialView("ProductViewTablePartialView", model);
        }

        public ActionResult AddOrEdit(string Id = "")
        {
            if (Id.Equals(""))
            {
                return PartialView("CRUDProductView", new ProductViewViewModel());
            }
            else
            {

                var data = productViewService.GetProductViewByID(Id);
                if (data == null) return View("CRUDProductView", new ProductViewViewModel());
                else
                {
                    var model = new ProductViewViewModel();
                    model.Id = data.Id.ToString();
                    model.Name = data.Name;
                    model.ImageOne = data.ImageOne;
                    model.ImageTwo = data.ImageTwo;
                    model.Harvest = data.HarvestAt;
                    model.Origin = data.Origin;
                    model.Attribute = data.Attribute;
                    model.Technology = data.Technology;
                    return PartialView("CRUDProductView", model);
                }
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddOrUpdate(ProductViewViewModel model)
        {
            if (model.Id == null)
            {
                var supplier = (SupplierLogin)Session[Constant.SUPPLIER_SESSION];
                if (supplier != null)
                {
                    var account = accountService.GetAccountByID(supplier.UserID.ToString());
                    Core.Models.Entities.ProductView productView = new Core.Models.Entities.ProductView()
                    {
                        Name = model.Name,
                        ImageOne = model.ImageOne,
                        ImageTwo = model.ImageTwo,
                        Origin = model.Origin,
                        Attribute = model.Attribute,
                        Technology = model.Technology,
                        HarvestAt = model.Harvest,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Supplier = account.Supplier,

                    };
                    productView.QRHashCode = Hash.Instance.ComputeSha256Hash(productView.Name + productView.Origin + productView.Attribute + productView.Technology + productView.HarvestAt);

                    try
                    {
                        context.ProductViews.Add(productView);
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {

                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Thêm sản phẩm thất bại",
                            MessageType = GenericMessages.error
                        };
                        return RedirectToAction("ProductView", "ProductView", new { Area = "Supplier" });
                    }

                    TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Thêm sản phẩm thành công",
                        MessageType = GenericMessages.success
                    };
                    return RedirectToAction("ProductView", "ProductView", new { Area = "Supplier" });
                }
            }
            else
            {
                var supplier = (SupplierLogin)Session[Constant.SUPPLIER_SESSION];
                if (supplier != null)
                {
                    var account = accountService.GetAccountByID(supplier.UserID.ToString());
                    var productView = productViewService.GetProductViewByID(model.Id);
                    if (productView != null && account != null)
                    {
                        productView.Name = model.Name;
                        productView.ImageOne = model.ImageOne;
                        productView.ImageTwo = model.ImageTwo;
                        productView.Origin = model.Origin;
                        productView.Attribute = model.Attribute;
                        productView.Technology = model.Technology;
                        productView.HarvestAt = model.Harvest;
                        productView.UpdateAt = DateTime.Now;
                        productView.Supplier = account.Supplier;
                        productView.QRHashCode = Hash.Instance.ComputeSha256Hash(productView.Name + productView.Origin + productView.Attribute + productView.Technology + productView.HarvestAt);
                    }
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {

                        TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Cập nhật sản phẩm thất bại",
                            MessageType = GenericMessages.error
                        };
                        return RedirectToAction("ProductView", "ProductView", new { Area = "Supplier" });
                    }

                    TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Thêm sản phẩm thành công",
                        MessageType = GenericMessages.success
                    };
                    return RedirectToAction("ProductView", "ProductView", new { Area = "Supplier" });
                }
            }

            TempData[Constant.MessageViewBagName] = new GenericMessageViewModel
            {
                Message = "Đã xảy ra lỗi",
                MessageType = GenericMessages.error
            };
            return RedirectToAction("ProductView", "ProductView", new { Area = "Supplier" });
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
                    var productView = context.ProductViews.Where(x => x.Id == new Guid(Id)).SingleOrDefault();
                    context.ProductViews.Remove(productView);
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