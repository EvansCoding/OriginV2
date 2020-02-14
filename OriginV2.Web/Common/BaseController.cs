using OriginV2.Core.Constants;
using OriginV2.Core.Interfaces;
using System.Web.Mvc;

namespace OriginV2.Web.Common
{
    public class BaseController : Controller
    {
        protected readonly IDataContext context;

        public BaseController(IDataContext context)
        {
            this.context = context;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            var sessionAdmin = (UserLogin)Session[Constant.USER_SESSION];
            var sessionSupplier = (SupplierLogin)Session[Constant.SUPPLIER_SESSION];

            if (sessionAdmin == null && sessionSupplier == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Index", Area = string.Empty }));
            }

            base.OnActionExecuted(filterContext);
        }
    }
}