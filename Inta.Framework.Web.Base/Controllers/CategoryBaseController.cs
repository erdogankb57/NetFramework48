using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
using Inta.Framework.Web.Base.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inta.Framework.Web.Base.Controllers
{
    [AuthorizationCheck]
    public class CategoryBaseController : Controller
    {
        public ActionResult GetCategory(int Id,
        string ObjectId = null,
        string ObjectName = null,
        string DisplayName = null,
        string ValueName = null,
        string DefaultValue = null,
        string DefaultText = null)
        {
            ReturnObject<string> result = new ReturnObject<string>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

            int TopCategoryId = Convert.ToInt32(db.Get("Select * from Category where Id=" + Id, System.Data.CommandType.Text).Data["CategoryId"]);


            StringBuilder shtml = new StringBuilder();
            shtml.Append("<ul>");
            shtml.Append(GetSubCategory(TopCategoryId));
            shtml.Append("</ul>");

            shtml.Append($"<select type='select' name='{ObjectName}' id='{ObjectId}' DisplayName='{DisplayName}' ValueName='{ValueName}' DefaultText='{DefaultText}' DefaultValue='{DefaultValue}' class='selectList shadow-none'>");
            shtml.Append($"<option value='{DefaultValue}' >{DefaultText}</option>");


            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = TopCategoryId });
            var returnObject = db.Find("Select * from Category where CategoryId=@Id", System.Data.CommandType.Text, parameters);
            if (returnObject != null)
            {
                for (int i = 0; i < returnObject.Data.Rows.Count; i++)
                {
                    string RowId = returnObject.Data.Rows[i]["Id"].ToString();
                    string val = string.Empty;
                    string text = string.Empty;
                    text = returnObject.Data.Rows[i][DisplayName].ToString();
                    val = returnObject.Data.Rows[i][ValueName].ToString();

                    shtml.Append($@"<option value=""{val}"" {(RowId == Id.ToString() ? "selected" : "")}>{text}</option>");
                }
            }
            shtml.Append("</select>");

            result.Data = shtml.ToString();

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        private static string GetSubCategory(int Id)
        {
            StringBuilder shtml = new StringBuilder();

            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = Id });
            var returnObject = db.Get<Category>("Select * from Category where Id=@Id", System.Data.CommandType.Text, parameters);
            if (returnObject != null)
            {
                if (returnObject.Data.CategoryId != 0)
                {
                    shtml.Append(GetSubCategory(returnObject.Data.CategoryId));
                }
                shtml.Append($"<li>{returnObject.Data.Name}</li>");
            }

            return shtml.ToString();
        }
    }
}
