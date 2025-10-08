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
    public class RecordFileManager
    {
        /// <summary>
        /// Banner ve tiplerini döner
        /// </summary>
        /// <returns></returns>
        public List<RecordFileModel> Find(int RecordId)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "RecordId", Value = RecordId });

            List<RecordFileModel> list = new List<RecordFileModel>();
            var data = db.Find("select * from RecordFile where RecordId=@RecordId", System.Data.CommandType.Text, parameters);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new RecordFileModel
                {
                    Name = data.Data.Rows[i]["Name"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    Explanation = data.Data.Rows[i]["Explanation"].ToString(),
                    FileName = data.Data.Rows[i]["FileName"].ToString(),
                    FileTagName = data.Data.Rows[i]["FileTagName"].ToString(),
                    FileTitleName = data.Data.Rows[i]["FileTitleName"].ToString(),
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
        public List<RecordFileModel> Get(int Id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = Id });


            List<RecordFileModel> list = new List<RecordFileModel>();
            var data = db.Find("select * from RecordFile where Id=@Id", System.Data.CommandType.Text, parameters);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new RecordFileModel
                {
                    Name = data.Data.Rows[i]["Name"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    Explanation = data.Data.Rows[i]["Explanation"].ToString(),
                    FileName = data.Data.Rows[i]["FileName"].ToString(),
                    FileTagName = data.Data.Rows[i]["FileTagName"].ToString(),
                    FileTitleName = data.Data.Rows[i]["FileTitleName"].ToString(),
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
