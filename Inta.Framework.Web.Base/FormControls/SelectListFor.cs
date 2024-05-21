using Inta.Framework.Ado.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inta.Framework.Web.Base.FormControls
{
    public static class SelectList
    {
        public static MvcHtmlString SelectListFor(
        this HtmlHelper content,
        string query,
        string ObjectId = null,
        string ObjectName = null,
        string DisplayName = null,
        string ValueName = null,
        string SelectedValue = null,
        string DefaultText = null,
        string DefaultValue = null,
        string IconName = null,
        bool IsRequired = false,
        bool IsDisabled = false
            )
        {
            if (string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(ValueName))
                return new MvcHtmlString("");


            StringBuilder shtml = new StringBuilder();
            shtml.Append($"<select type='select' name='{ObjectName}' id='{ObjectId}' DisplayName='{DisplayName}' ValueName='{ValueName}' DefaultText='{DefaultText}' DefaultValue='{DefaultValue}' class='selectList form-control shadow-none' {(IsRequired ? "required" : "")} {(IsDisabled ? "disabled" : "")}>");
            shtml.Append($"<option value='{DefaultValue}' >{DefaultText}</option>");

            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            var returnObject = db.Find(query, System.Data.CommandType.Text);
            if (returnObject != null)
            {
                for (int i = 0; i < returnObject.Data.Rows.Count; i++)
                {
                    string val = string.Empty;
                    string text = string.Empty;
                    text = returnObject.Data.Rows[i][DisplayName].ToString();
                    val = returnObject.Data.Rows[i][ValueName].ToString();

                    shtml.Append($@"<option value=""{val}"" {(SelectedValue == val.ToString() ? "selected" : "")}>{text}</option>");
                }
            }
            shtml.Append("</select>");

            return new MvcHtmlString(shtml.ToString());
        }
    }
}
