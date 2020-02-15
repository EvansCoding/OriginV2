using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Core.Utilities;
using OriginV2.Web.Common;
using OriginV2.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OriginV2.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly ISupplierService supplierService;
        public ProductController(IProductService productService, ISupplierService supplierService, IDataContext context) : base(context)
        {
            this.productService = productService;
            this.supplierService = supplierService;
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
            var model = productService.PageListAdmin(search, supplier, page, pageSize);
            return PartialView("ProductTablePartialView", model);
        }

        [HttpGet]
        public ActionResult AddOrEdit(string Id = "")
        {
            if (Id.Equals(""))
            {
                return PartialView("ShowProductView", new ProductViewModel());
            }
            else
            {
                var data = productService.GetProduct(Id);
                if (data == null) return View("ShowProductView", new ProductViewModel());
                else
                {
                    var model = new ProductViewModel();
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

        public ActionResult ExportQR(string Id)
        {
            string s = "";
            var product = productService.GetProduct(Id);
            if (product != null)
            {
                string url = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);
                string p = Url.Action("Index", "Product", new { Id = product.QRHashCode, Area = String.Empty });

                p = url + p;
                var imageQR = SaveImageQR.Instance.createQRCode(p, product.Id.ToString(), Server);
                report reportQR = new report();
                var uri = new Uri(product.ImageOne).LocalPath;
                //var path = Path.GetFileName(uri.AbsolutePath);

                //var file = GetFile(path);
                var imageOne = Image.FromFile(Server.MapPath(uri));
                reportQR.create( imageQR, imageOne, product.Name, product.HarvestAt.ToShortDateString());

                string path = "~/Media/Uploads/FolderPDF";
                bool exist = System.IO.Directory.Exists(Server.MapPath(path));
                if (!exist) System.IO.Directory.CreateDirectory(Server.MapPath(path));

                reportQR.CreateDocument();
                reportQR.ExportToPdf(Server.MapPath(path) + "\\" + product.QRHashCode + ".pdf");

                s = url + "/Media/Uploads/FolderPDF/" + product.QRHashCode + ".pdf";

                return Json(new { success = true, message = "Xuất tệp tin thành công", linkScript = s }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Xuất tệp tin thất bại", linkScript = s }, JsonRequestBehavior.AllowGet);

        }
    }
}