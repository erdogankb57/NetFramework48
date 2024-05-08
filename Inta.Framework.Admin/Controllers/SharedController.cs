using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult BreadCrump()
        {
            return View();
        }
    }
}