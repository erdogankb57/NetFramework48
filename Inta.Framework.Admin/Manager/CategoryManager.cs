using Inta.Framework.Ado.Net;
using Inta.Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Inta.Framework.Web.Manager
{
    public class CategoryManager
    {
        public List<CategoryPageType> FindCategoryPageType()
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            List<CategoryPageType> list = new List<CategoryPageType>();
            var data = db.Find("select distinct p.ControllerName,p.ActionName,c.Id,c.CategoryUrl,c.Name from Category c inner join PageType p on c.PageTypeId=p.Id", System.Data.CommandType.Text);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new CategoryPageType
                {
                    CategoryFullRouting = GetCategoryFullRouting(Convert.ToInt32(data.Data.Rows[i]["Id"])),
                    CategoryFullUrl = GetCategoryFullUrl(Convert.ToInt32(data.Data.Rows[i]["Id"])),
                    Id = Convert.ToInt32(data.Data.Rows[i]["Id"]),
                    ControllerName = data.Data.Rows[i]["ControllerName"].ToString(),
                    ActionName = data.Data.Rows[i]["ActionName"].ToString(),
                    CategoryName  = data.Data.Rows[i]["Name"].ToString()
                });
            }

            return list;
        }

        public static string GetCategoryFullRouting(int Id)
        {
            return "category/" + GetCategoryUrl(Convert.ToInt32(Id)) + "{id}";
        }

        public static string GetCategoryFullUrl(int Id)
        {
            return "/category/" + GetCategoryUrl(Id) + Id.ToString();
        }

        public static string GetCategoryUrl(int Id)
        {
            string url = "";
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = Id });
            var category = db.Get("Select * from Category where Id=@CategoryId", System.Data.CommandType.Text, parameters);

            if (category.Data != null)
            {
                if (category.Data["CategoryId"] != null && Convert.ToInt32(category.Data["CategoryId"]) != 0)
                {
                    url = "" + category.Data["CategoryUrl"].ToString() + "/";
                    url = GetCategoryUrl(Convert.ToInt32(category.Data["CategoryId"])) + url;
                }
                else
                {
                    url = "" + category.Data["CategoryUrl"].ToString() + "/";
                }
            }

            return url;
        }

    }

    
}