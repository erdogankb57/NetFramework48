using Inta.Framework.Web.Base.Authorization;
using System.Web.Mvc;

namespace Inta.Framework.Web.Areas.Admin.Controllers
{
    [AuthorizationCheck]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}