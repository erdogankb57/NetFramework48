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
    public class RecordImageManager
    {
        /// <summary>
        /// Banner ve tiplerini döner
        /// </summary>
        /// <returns></returns>
        public List<RecordImageModel> Find(int RecordId)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "RecordId", Value = RecordId });

            List<RecordImageModel> list = new List<RecordImageModel>();
            var data = db.Find("select * from RecordImage where RecordId=@RecordId", System.Data.CommandType.Text, parameters);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new RecordImageModel
                {
                    Name = data.Data.Rows[i]["Name"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    Explanation = data.Data.Rows[i]["Explanation"].ToString(),
                    ImageName = data.Data.Rows[i]["ImageName"].ToString(),
                    ImageTagName = data.Data.Rows[i]["ImageTagName"].ToString(),
                    ImageTitleName = data.Data.Rows[i]["ImageTitleName"].ToString(),
                    TargetId = Convert.ToInt32(data.Data.Rows[i]["TargetId"]),
                    HomePageStatus = Convert.ToBoolean(data.Data.Rows[i]["HomePageStatus"]),
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
        public List<RecordImageModel> Get(int Id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = Id });


            List<RecordImageModel> list = new List<RecordImageModel>();
            var data = db.Find("select * from RecordImage where Id=@Id", System.Data.CommandType.Text, parameters);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new RecordImageModel
                {
                    Name = data.Data.Rows[i]["Name"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    Explanation = data.Data.Rows[i]["Explanation"].ToString(),
                    ImageName = data.Data.Rows[i]["ImageName"].ToString(),
                    ImageTagName = data.Data.Rows[i]["ImageTagName"].ToString(),
                    ImageTitleName = data.Data.Rows[i]["ImageTitleName"].ToString(),
                    TargetId = Convert.ToInt32(data.Data.Rows[i]["TargetId"]),
                    HomePageStatus = Convert.ToBoolean(data.Data.Rows[i]["HomePageStatus"]),
                    OrderNumber = Convert.ToInt32(data.Data.Rows[i]["OrderNumber"]),
                    IsActive = Convert.ToBoolean(data.Data.Rows[i]["IsActive"])
                });
            }

            return list;
        }
    }
}