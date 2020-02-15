using OriginV2.Core.Interfaces;
using OriginV2.Core.Interfaces.IServices;
using OriginV2.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OriginV2.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private IDataContext context;
        public ProductController(IProductService productService, IDataContext context)
        {
            this.productService = productService;
            this.context = context;
        }

        public ActionResult Index(string Id = "")
        {
            if (Id.Equals(""))
            {
                return RedirectToAction("Index", "Account", new { Area = String.Empty });
            }
            else
            {
                var data = productService.GetProductByHashCode(Id);
                if (data == null) return RedirectToAction("Index", "Account", new { Area = String.Empty });
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
                    return View(model);
                }
            }

        }
    }
}