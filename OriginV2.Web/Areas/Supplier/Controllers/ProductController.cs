using OriginV2.Core.Constants;
using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Web.Common;
using OriginV2.Web.ViewModels;
using System.Web.Mvc;

namespace OriginV2.Web.Areas.Supplier.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly ISupplierService supplierService;
        private readonly IAccountService accountService;
        public ProductController(IAccountService accountService, IProductService productService, ISupplierService supplierService, IDataContext context) : base(context)
        {
            this.productService = productService;
            this.supplierService = supplierService;
            this.accountService = accountService;
        }
        public ActionResult ProductView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadTable(string search, string state, int page = 1, int pageSize = 10)
        {
            var session = (SupplierLogin)Session[Constant.SUPPLIER_SESSION];
            var account = accountService.GetAccountByID(session.UserID.ToString());
            var model = productService.PageListAdmin(search, account.Supplier.Id.ToString(), page, pageSize);
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
    }
}