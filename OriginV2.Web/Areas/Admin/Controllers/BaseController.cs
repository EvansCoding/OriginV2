using OriginV2.Core.Constants;
using OriginV2.Core.Interfaces;
using OriginV2.Web.Common;
using System.Web.Mvc;

namespace OriginV2.Web.Areas.Admin.Controllers
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
            var session = (UserLogin)Session[Constant.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Index", Area = string.Empty }));
            }
            base.OnActionExecuted(filterContext);
        }
    }
}