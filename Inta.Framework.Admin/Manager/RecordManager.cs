using Inta.Framework.Ado.Net;
using Inta.Framework.Web.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Inta.Framework.Web.Manager
{
    public class RecordManager
    {
        /// <summary>
        /// Banner ve tiplerini döner
        /// </summary>
        /// <returns></returns>
        public List<RecordModel> Find(int CategoryId)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = CategoryId });

            List<RecordModel> list = new List<RecordModel>();
            var data = db.Find("select * from Record where CategoryId=@CategoryId", System.Data.CommandType.Text, parameters);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new RecordModel
                {
                    Name = data.Data.Rows[i]["Name"].ToString(),
                    RecordUrl = data.Data.Rows[i]["RecordUrl"].ToString(),
                    Title = data.Data.Rows[i]["Title"].ToString(),
                    MetaDescription = data.Data.Rows[i]["MetaDescription"].ToString(),
                    MetaKeywords = data.Data.Rows[i]["MetaKeywords"].ToString(),
                    Url = data.Data.Rows[i]["Url"].ToString(),
                    ShortContent = data.Data.Rows[i]["ShortContent"].ToString(),
                    Link = data.Data.Rows[i]["Link"].ToString(),
                    TargetType = data.Data.Rows[i]["TargetType"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    Explanation = data.Data.Rows[i]["Explanation"].ToString(),
                    OrderNumber = Convert.ToInt32(data.Data.Rows[i]["OrderNumber"]),
                    IsActive = Convert.ToBoolean(data.Data.Rows[i]["IsActive"])


                });
            }

            return list;
        }

        /// <summary>
        /// Banner ve tiplerini döner
        /// </summary>
        /// <returns></returns>
        public List<RecordModel> Find()
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            List<RecordModel> list = new List<RecordModel>();
            var data = db.Find("select * from Record", System.Data.CommandType.Text);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new RecordModel
                {
                    Name = data.Data.Rows[i]["Name"].ToString(),
                    RecordUrl = data.Data.Rows[i]["RecordUrl"].ToString(),
                    Title = data.Data.Rows[i]["Title"].ToString(),
                    MetaDescription = data.Data.Rows[i]["MetaDescription"].ToString(),
                    MetaKeywords = data.Data.Rows[i]["MetaKeywords"].ToString(),
                    Url = data.Data.Rows[i]["Url"].ToString(),
                    ShortContent = data.Data.Rows[i]["ShortContent"].ToString(),
                    Link = data.Data.Rows[i]["Link"].ToString(),
                    TargetType = data.Data.Rows[i]["TargetType"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    Explanation = data.Data.Rows[i]["Explanation"].ToString(),
                    OrderNumber = Convert.ToInt32(data.Data.Rows[i]["OrderNumber"]),
                    IsActive = Convert.ToBoolean(data.Data.Rows[i]["IsActive"])
                });
            }

            return list;
        }

        /// <summary>
        /// Banner ve tiplerini döner
        /// </summary>
        /// <returns></returns>
        public List<RecordModel> Get(int Id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = Id });


            List<RecordModel> list = new List<RecordModel>();
            var data = db.Find("select * from Record where Id=@Id", System.Data.CommandType.Text, parameters);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new RecordModel
                {
                    Name = data.Data.Rows[i]["Name"].ToString(),
                    RecordUrl = data.Data.Rows[i]["RecordUrl"].ToString(),
                    Title = data.Data.Rows[i]["Title"].ToString(),
                    MetaDescription = data.Data.Rows[i]["MetaDescription"].ToString(),
                    MetaKeywords = data.Data.Rows[i]["MetaKeywords"].ToString(),
                    Url = data.Data.Rows[i]["Url"].ToString(),
                    ShortContent = data.Data.Rows[i]["ShortContent"].ToString(),
                    Link = data.Data.Rows[i]["Link"].ToString(),
                    TargetType = data.Data.Rows[i]["TargetType"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    Explanation = data.Data.Rows[i]["Explanation"].ToString(),
                    OrderNumber = Convert.ToInt32(data.Data.Rows[i]["OrderNumber"]),
                    IsActive = Convert.ToBoolean(data.Data.Rows[i]["IsActive"])
                });
            }

            return list;
        }
    }
}