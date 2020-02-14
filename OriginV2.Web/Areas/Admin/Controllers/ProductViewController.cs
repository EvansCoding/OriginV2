using OriginV2.Core.Constants;
using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Core.Models.Entities;
using OriginV2.Web.Common;
using OriginV2.Web.ViewModels;
using System;
using System.Web.Mvc;

namespace OriginV2.Web.Areas.Admin.Controllers
{
    public class ProductViewController : BaseController
    {
        private readonly IProductViewService productViewService;
        private readonly IUserService userService;
        private readonly ISupplierService supplierService;
        private readonly IProductService productService;
        private readonly IAccountService accountService;
        public ProductViewController(IProductViewService productViewService, IProductService productService, IAccountService accountService, IUserService userService, ISupplierService supplierService, IDataContext context) : base(context)
        {
            this.productViewService = productViewService;
            this.userService = userService;
            this.supplierService = supplierService;
            this.productService = productService;
            this.accountService = accountService;
        }

        public ActionResult ProductView()
        {
            var suppliers = supplierService.GetSuppliers();
            if (suppliers != null)
                ViewBag.ListOfSupplier = new SelectList(suppliers, "Id", "Name");
            return View();
        }

        [HttpGet]
        public ActionResult LoadTable(string search, string supplier, string state, int page = 1, int pageSize = 10)
        {
            var model = productViewService.PageListAdmin(search, supplier, page, pageSize);
            return PartialView("ProductViewTablePartialView", model);
        }

        [HttpGet]
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
                    model.AddressSupplier = data.Supplier.Address;
                    model.NameSupplier = data.Supplier.Name;
                    return PartialView("ShowProductView", model);
                }
            }
        }

        public ActionResult Publish(string Id = "")
        {

            var data = productViewService.GetProductViewByID(Id);
            var product = new Product()
            {
                Name = data.Name,
                ImageOne = data.ImageOne,
                ImageTwo = data.ImageTwo,
                Origin = data.Origin,
                Attribute = data.Attribute,
                QRHashCode = data.QRHashCode,
                Technology = data.Technology,
                Publish = true,
                HarvestAt = data.HarvestAt,
                CreateAt = DateTime.Now,
                Supplier = data.Supplier
            };

            var account = (UserLogin)Session[Constant.USER_SESSION];
            var user = accountService.GetAccountByID(account.UserID.ToString());
            product.User = user.User;
            if(productService.GetParent() != null)
            product.Parent = productService.GetParent().QRHashCode;

            try
            {
                context.Products.Add(product);
                context.ProductViews.Remove(data);
                context.SaveChanges();
                return Json(new { success = true, message = "Chấp nhận thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { success = false, message = "Chấp nhận thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(string Id = "")
        {
            try
            {
                var productView = productViewService.GetProductViewByID(Id);
                context.ProductViews.Remove(productView);
                context.SaveChanges();
                return Json(new { success = true, message = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

