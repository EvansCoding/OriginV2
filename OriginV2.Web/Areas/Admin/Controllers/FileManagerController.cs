using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OriginV2.Web.Areas.Admin.Controllers
{
    public class FileManagerController : Controller
    {
        // GET: Admin/FileManager
        public ActionResult Index()
        {
            return View();
        }
    }
}