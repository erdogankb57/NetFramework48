using Inta.Framework.Contract;
using Inta.Framework.Web.Base.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Inta.Framework.Admin.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult BreadCrump()
        {
            return View();
        }

        public ActionResult SelectLanguage(int id)
        {
            ReturnObject<int> returnObject = new ReturnObject<int>();

            AuthenticationData authenticationData = new AuthenticationData();
            if (id > 0)
            {
                authenticationData.SetLangugageId(id.ToString());
                returnObject.ResultType = MessageType.Success;
                returnObject.Data = id;
            }

            return Json(returnObject, JsonRequestBehavior.AllowGet);
        }
    }
}