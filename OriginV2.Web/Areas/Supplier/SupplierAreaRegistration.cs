using System.Web.Mvc;

namespace OriginV2.Web.Areas.Supplier
{
    public class SupplierAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Supplier";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Supplier_default",
                "Supplier/{controller}/{action}/{id}",
                new { controller ="Supplier", action = "ProfileView", id = UrlParameter.Optional },
                        namespaces: new[] { "OriginV2.Web.Areas.Supplier.Controllers" }
            );
        }
    }
}