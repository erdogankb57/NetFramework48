using Inta.Framework.Ado.Net;
using Inta.Framework.Business.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Business.Manager
{
    public class CategoryManager
    {
        /// <summary>
        /// Kategori ve sayfa türlerinin listesini döner içerisinde kategori urlsini barındırır.
        /// </summary>
        /// <returns></returns>
        public List<CategoryModel> Find(int Id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = Id });


            List<CategoryModel> list = new List<CategoryModel>();
            var data = db.Find("select distinct p.ControllerName,p.ActionName,c.*  from Category c inner join PageType p on c.PageTypeId=p.Id where c.CategoryId=@CategoryId", System.Data.CommandType.Text, parameters);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new CategoryModel
                {
                    CategoryFullRouting = GetCategoryFullRouting(Convert.ToInt32(data.Data.Rows[i]["Id"])),
                    CategoryFullUrl = GetCategoryFullUrl(Convert.ToInt32(data.Data.Rows[i]["Id"])),
                    Id = Convert.ToInt32(data.Data.Rows[i]["Id"]),
                    ControllerName = data.Data.Rows[i]["ControllerName"].ToString(),
                    ActionName = data.Data.Rows[i]["ActionName"].ToString(),
                    CategoryName = data.Data.Rows[i]["Name"].ToString(),
                    Title = data.Data.Rows[i]["Title"].ToString(),
                    MetaDecription = data.Data.Rows[i]["MetaDecription"].ToString(),
                    MetaKeywords = data.Data.Rows[i]["MetaKeywords"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    Image = data.Data.Rows[i]["Name"].ToString(),
                    ImageTag = data.Data.Rows[i]["ImageTag"].ToString(),
                    ImageTitle = data.Data.Rows[i]["ImageTitle"].ToString(),
                    Explanation = data.Data.Rows[i]["Explanation"].ToString(),
                    OrderNumber = data.Data.Rows[i]["OrderNumber"].ToString(),
                    IsActive = data.Data.Rows[i]["IsActive"].ToString(),
                    CategoryLink = data.Data.Rows[i]["CategoryLink"].ToString(),
                    Items = Find(Convert.ToInt32(data.Data.Rows[i]["Id"]))
                });
            }

            return list;
        }

        /// <summary>
        /// Kategori ve sayfa türlerinin listesini döner içerisinde kategori urlsini barındırır.
        /// </summary>
        /// <returns></returns>
        public List<CategoryModel> Find()
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            List<CategoryModel> list = new List<CategoryModel>();
            var data = db.Find("select distinct p.ControllerName,p.ActionName,c.* from Category c inner join PageType p on c.PageTypeId=p.Id", System.Data.CommandType.Text);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new CategoryModel
                {
                    CategoryFullRouting = GetCategoryFullRouting(Convert.ToInt32(data.Data.Rows[i]["Id"])),
                    CategoryFullUrl = GetCategoryFullUrl(Convert.ToInt32(data.Data.Rows[i]["Id"])),
                    Id = Convert.ToInt32(data.Data.Rows[i]["Id"]),
                    ControllerName = data.Data.Rows[i]["ControllerName"].ToString(),
                    ActionName = data.Data.Rows[i]["ActionName"].ToString(),
                    CategoryName = data.Data.Rows[i]["Name"].ToString(),
                    Title = data.Data.Rows[i]["Title"].ToString(),
                    MetaDecription = data.Data.Rows[i]["MetaDecription"].ToString(),
                    MetaKeywords = data.Data.Rows[i]["MetaKeywords"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    Image = data.Data.Rows[i]["Name"].ToString(),
                    ImageTag = data.Data.Rows[i]["ImageTag"].ToString(),
                    ImageTitle = data.Data.Rows[i]["ImageTitle"].ToString(),
                    Explanation = data.Data.Rows[i]["Explanation"].ToString(),
                    OrderNumber = data.Data.Rows[i]["OrderNumber"].ToString(),
                    IsActive = data.Data.Rows[i]["IsActive"].ToString(),
                    CategoryLink = data.Data.Rows[i]["CategoryLink"].ToString(),
                });
            }

            return list;
        }

        /// <summary>
        /// Kategori ve sayfa türlerini döner içerisinde kategori urlsini barındırır.
        /// </summary>
        /// <returns></returns>
        public List<CategoryModel> Get(int Id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = Id });


            List<CategoryModel> list = new List<CategoryModel>();
            var data = db.Find("select distinct p.ControllerName,p.ActionName,c.*  from Category c inner join PageType p on c.PageTypeId=p.Id where c.Id=@CategoryId", System.Data.CommandType.Text, parameters);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new CategoryModel
                {
                    CategoryFullRouting = GetCategoryFullRouting(Convert.ToInt32(data.Data.Rows[i]["Id"])),
                    CategoryFullUrl = GetCategoryFullUrl(Convert.ToInt32(data.Data.Rows[i]["Id"])),
                    Id = Convert.ToInt32(data.Data.Rows[i]["Id"]),
                    ControllerName = data.Data.Rows[i]["ControllerName"].ToString(),
                    ActionName = data.Data.Rows[i]["ActionName"].ToString(),
                    CategoryName = data.Data.Rows[i]["Name"].ToString(),
                    Title = data.Data.Rows[i]["Title"].ToString(),
                    MetaDecription = data.Data.Rows[i]["MetaDecription"].ToString(),
                    MetaKeywords = data.Data.Rows[i]["MetaKeywords"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    Image = data.Data.Rows[i]["Name"].ToString(),
                    ImageTag = data.Data.Rows[i]["ImageTag"].ToString(),
                    ImageTitle = data.Data.Rows[i]["ImageTitle"].ToString(),
                    Explanation = data.Data.Rows[i]["Explanation"].ToString(),
                    OrderNumber = data.Data.Rows[i]["OrderNumber"].ToString(),
                    IsActive = data.Data.Rows[i]["IsActive"].ToString(),
                    CategoryLink = data.Data.Rows[i]["CategoryLink"].ToString(),
                    Items = Find(Convert.ToInt32(data.Data.Rows[i]["Id"]))
                });
            }

            return list;
        }


        public static string GetCategoryFullRouting(int Id)
        {
            return "" + GetCategoryUrl(Convert.ToInt32(Id)) + "{id}";
        }

        public static string GetCategoryFullUrl(int Id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = Id });
            var category = db.Get("Select c.*,p.IsNoLink,p.IsMenuFirstCategory,p.IsMenuFirstRecord from Category c inner join PageType p on c.PageTypeId=p.Id where c.Id=@CategoryId", System.Data.CommandType.Text, parameters);
            if (category.Data != null)
            {
                if (category.Data["IsNoLink"] != DBNull.Value && Convert.ToBoolean(category.Data["IsNoLink"]))
                    return "javascript:void(0)";


                if (category.Data["IsMenuFirstCategory"] != DBNull.Value && Convert.ToBoolean(category.Data["IsMenuFirstCategory"]))
                {
                    //ilk kategoriyi açalım
                    List<SqlParameter> firstCategoryParameters = new List<SqlParameter>();
                    firstCategoryParameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = Id });
                    var firstCategory = db.Get("Select top 1 * from Category where CategoryId=@CategoryId", System.Data.CommandType.Text, firstCategoryParameters);
                    if (firstCategory.Data != null)
                        return GetCategoryFullUrl(Convert.ToInt32(firstCategory.Data["Id"]));
                }



                if (category.Data["IsMenuFirstRecord"] != DBNull.Value && Convert.ToBoolean(category.Data["IsMenuFirstRecord"]))
                {
                    //ilk kaydı açalım
                }


                List<SqlParameter> recordParameters = new List<SqlParameter>();
                recordParameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = Id });
                var record = db.Find("select * from Record where CategoryId=@CategoryId", System.Data.CommandType.Text, recordParameters);
                if (record.Data.Rows.Count == 1)
                {
                    return record.Data.Rows[0]["RecordUrl"].ToString() + "/" + record.Data.Rows[0]["Id"].ToString() + ".html";
                }

            }
            return "/" + GetCategoryUrl(Id) + Id.ToString();
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

                    //tek kategori ise detay/ şeklinde link verilecek
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
