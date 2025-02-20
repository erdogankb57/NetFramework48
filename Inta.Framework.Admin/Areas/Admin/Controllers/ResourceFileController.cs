using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Web.Areas.Admin.Controllers
{
    public class ResourceFileController : Controller
    {

        //Dosyanın bu şekilde çalıştırılabilmesi için propertiesden embeded resource olarak ayarlanması gerekir.
        public ActionResult GetJavascriptFile(string file)
        {
            var asm = System.Reflection.Assembly.Load("Inta.Framework.Web.Base");
            var names = asm.GetManifestResourceNames();
            if (names.Any(a => a == file))
            {
                var stream = asm.GetManifestResourceStream(file);
                return File(stream, "application/x-javascript");
            }
            else
                return HttpNotFound();
        }
       public ActionResult GetCssFile(string file)
        {
            var asm = System.Reflection.Assembly.Load("Inta.Framework.Web.Base");
            var names = asm.GetManifestResourceNames();
            if (names.Any(a => a == file))
            {
                var stream = asm.GetManifestResourceStream(file);
                return File(stream, "text/css");
            }
            else
                return HttpNotFound();
        }

    }
}