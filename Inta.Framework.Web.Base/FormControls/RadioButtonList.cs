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
    public static class RadioButtonList
    {
        public static MvcHtmlString RadioButtonListFor(
        this HtmlHelper content,
        string query,
        string ObjectId = null,
        string ObjectName = null,
        string DisplayName = null,
        string ValueName = null,
        string SelectedValue = null,
        string SearchPlaceHolder = null,
        string DefaultText = null,
        string DefaultValue = null,
        string IconName = null,
        bool IsRequired = false)
        {
            if (ObjectId is null)
            {
                return new MvcHtmlString("");
            }

            if (string.IsNullOrEmpty(ObjectId) || string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(ValueName))
                return new MvcHtmlString("");

            StringBuilder shtml = new StringBuilder();
            shtml.Append("<div class=\"RadioButtonList\" id='" + ObjectId + "' ObjectName='" + ObjectName + "' DisplayName = '" + DisplayName + "' ValueName= '" + ValueName + "'" + (IsRequired ? "required" : "") + ">");
            shtml.Append("<div class='RadioButtonListOpen'>");
            shtml.Append("<input type=\"text\" name=\"RadioButtonListSearch\" class=\"form-control shadow-none\" placeholder='" + SearchPlaceHolder + "' autocomplete='off'/>");

            shtml.Append("<ul>");


            int Count = 0;

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

                    shtml.Append($@"<li><input class='form-check-input shadow-none' type='radio' id='RadioButtonList_" + ObjectId + Count.ToString() + "' name='" + ObjectName + "' value='" + val + "'" + (SelectedValue == val.ToString() ? "checked" : "") + " /><label for='RadioButtonList_" + ObjectId + Count.ToString() + "'>" + text + "</label></li>");
                    Count++;

                }
            }

            shtml.Append("</ul>");
            shtml.Append("</div>");
            shtml.Append("</div>");

            return new MvcHtmlString(shtml.ToString());
        }
    }

}
