using Inta.Framework.Web.Base.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inta.Framework.Web.Base.Controllers
{
    [AuthorizationCheck]
    public class CategoryBaseController : Controller
    {
        public ActionResult Deneme()
        {
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}
