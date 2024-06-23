using Inta.Framework.Ado.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Base.FormControls
{
    public static class CategoryTree
    {
        public static MvcHtmlString CategoryTreeFor(
        this HtmlHelper content,
        int Id
        )
        {
            StringBuilder shtml = new StringBuilder();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> categoryParameters = new List<SqlParameter>();
            categoryParameters.Add(new SqlParameter { ParameterName = "Id", Value = Id });
            var category = db.Find("Select * from Category where CategoryId=@Id", System.Data.CommandType.Text, categoryParameters);
            shtml.Append("<div class='CategoryTreeBox'>");
            shtml.Append("<ul>");
            for (int i = 0; i < category.Data.Rows.Count; i++)
            {
                shtml.Append("<li>"+ category.Data.Rows[i]["Name"]);
                shtml.Append(GetSubCategory(Convert.ToInt32(category.Data.Rows[i]["Id"])));
                shtml.Append("</li>");
            }
            shtml.Append("</ul>");
            shtml.Append("</div>");
            return new MvcHtmlString(shtml.ToString());
        }

        private static string GetSubCategory(int Id)
        {
            StringBuilder shtml = new StringBuilder();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> categoryParameters = new List<SqlParameter>();
            categoryParameters.Add(new SqlParameter { ParameterName = "Id", Value = Id });
            var category = db.Find("Select * from Category where CategoryId=@Id", System.Data.CommandType.Text, categoryParameters);
            shtml.Append("<ul>");
            for (int i = 0; i < category.Data.Rows.Count; i++)
            {
                shtml.Append("<li>" + category.Data.Rows[i]["Name"]);
                shtml.Append(GetSubCategory(Convert.ToInt32(category.Data.Rows[i]["Id"])));
                shtml.Append("</li>");
            }
            shtml.Append("</ul>");

            return shtml.ToString();
        }
    }
}
