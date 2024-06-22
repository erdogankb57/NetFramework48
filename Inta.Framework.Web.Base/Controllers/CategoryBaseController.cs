using Inta.Framework.Admin.Base.Authorization;
using Inta.Framework.Admin.Base.FormControls;
using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Base.Controllers
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

            //int TopCategoryId = Convert.ToInt32(db.Get("Select * from Category where Id=" + Id, System.Data.CommandType.Text).Data["CategoryId"]);


            StringBuilder shtml = new StringBuilder();
            shtml.Append($"<input type='hidden' name='{ObjectName}' id='{ObjectId}' value='{Id}'/>");
            shtml.Append("<ul>");
            shtml.Append("<li id='0'>Başa dön</li>");
            shtml.Append(CategorySelectBox.GetSubCategory(Id));
            shtml.Append("</ul>");

            shtml.Append($"<select type='select' ObjectName='{ObjectName}' ObjectId='{ObjectId}' DisplayName='{DisplayName}' ValueName='{ValueName}' DefaultText='{DefaultText}' DefaultValue='{DefaultValue}' class='selectList shadow-none'>");
            shtml.Append($"<option value='{DefaultValue}' >{DefaultText}</option>");


            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = Id });
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
    }
}
