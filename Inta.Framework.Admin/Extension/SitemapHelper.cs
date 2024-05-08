using Inta.Framework.Ado.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Extension
{
    public static class SitemapHelper
    {
        public static string Breadcrump(this HtmlHelper helper, string controllerName, string actionName)
        {
            StringBuilder shtml = new StringBuilder();
            DBLayer dbLayer = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "ControllerName", Value = controllerName });
            parameters.Add(new SqlParameter { ParameterName = "ActionName", Value = actionName });
            var data = dbLayer.Find("Select * from SystemMenu where ControllerName=@ControllerName and ActionName=@ActionName", System.Data.CommandType.Text, parameters).Data;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                shtml.Append(GetTopMenu(Convert.ToInt32(data.Rows[i]["Id"])));
                //shtml.Append("<li class=\"breadcrumb-item\"><a href='#'>" + data.Rows[i]["Name"] + "</a></li>");
            }

            return shtml.ToString();
        }

        public static string Caption(this HtmlHelper helper, string controllerName, string actionName)
        {
            StringBuilder shtml = new StringBuilder();
            return shtml.ToString();
        }

        private static string GetTopMenu(int menuId)
        {
            StringBuilder shtml = new StringBuilder();

            DBLayer dbLayer = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = menuId });
            var data = dbLayer.Find("Select * from SystemMenu where Id=@Id", System.Data.CommandType.Text, parameters).Data;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i]["SystemMenuId"] != "0")
                    shtml.Append(GetTopMenu(Convert.ToInt32(data.Rows[i]["SystemMenuId"])));

                shtml.Append("<li class=\"breadcrumb-item\"><a href='#'>" + data.Rows[i]["Name"] + "</a></li>");
            }

            return shtml.ToString();
        }
    }
}