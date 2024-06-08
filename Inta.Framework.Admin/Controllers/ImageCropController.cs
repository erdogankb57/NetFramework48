using Inta.Framework.Ado.Net;
using Inta.Framework.Entity;
using Inta.Framework.Web.Base.Authorization;
using System.Configuration;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Controllers
{
    [AuthorizationCheck]
    public class ImageCropController : Controller
    {
        // GET: ImageCrop
        public ActionResult Index()
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);


            ViewBag.Image = generalSettings.Data.ImageCdnUrl + "b_" + Request["ImageName"];
            return View();
        }
    }
}