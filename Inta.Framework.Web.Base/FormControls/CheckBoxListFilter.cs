using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Base.FormControls
{
    public static class CheckBoxListFilter
    {
        public static MvcHtmlString CheckBoxListFilterFor(
        this HtmlHelper content,
        string query,
        string ObjectId = null,
        string ObjectName = null,
        string DisplayName = null,
        string ValueName = null,
        List<string> SelectedValueItem = null,
        string PlaceHolder = null,
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
            shtml.Append("<div class=\"CheckBoxListFilter\" id ='" + ObjectId + "'" + (IsRequired ? "required" : "") + " ObjectName='" + ObjectName + "' DisplayName='" + DisplayName + "' ValueName='" + ValueName + "'>");
            shtml.Append("<div class='CheckBoxListFilterInput form-control'><ul class='SelectedValue'></ul><span></span></div>");

            shtml.Append("<div class='CheckBoxListFilterMain' style='float:left;position:relative;width:100%;'>");
            shtml.Append("<div class='CheckBoxListFilterOpen'>");
            shtml.Append("<input type=\"text\" name=\"CheckBoxListFilterSearch\" class=\"form-control shadow-none\" placeholder='" + PlaceHolder + "' autocomplete='off'/>");
            shtml.Append(@"<ul>");

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

                    shtml.Append($@"<li><span><input class='form-check-input shadow-none' type='checkbox' id='CheckBoxListFilter_" + ObjectId + Count.ToString() + "'  name='" + ObjectName + "' value='" + val + "'" + (SelectedValueItem.Any(a => a == val.ToString()) ? "checked" : "") + " /><span><label class='form-check-label' for='CheckBoxListFilter_" + ObjectId + Count.ToString() + "'>" + text + "</label></li>");
                    Count++;
                }
            }

            shtml.Append("</ul>");
            shtml.Append("</div>");
            shtml.Append("</div>");
            shtml.Append("</div>");


            return new MvcHtmlString(shtml.ToString());
        }
    }
}
