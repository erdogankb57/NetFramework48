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
    public class BannerManager
    {
        /// <summary>
        /// Banner ve tiplerini döner
        /// </summary>
        /// <returns></returns>
        public List<BannerModel> Find(int Id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "BannerTypeId", Value = Id });

            List<BannerModel> list = new List<BannerModel>();
            var data = db.Find("select bt.Name as BannerTypeName,b.* from BannerType bt inner join Banner b on bt.Id=b.BannerTypeId where b.BannerTypeId=@BannerTypeId", System.Data.CommandType.Text, parameters);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new BannerModel
                {
                    BannerTypeName = data.Data.Rows[i]["BannerTypeName"].ToString(),
                    Name = data.Data.Rows[i]["Name"].ToString(),
                    Link = data.Data.Rows[i]["Link"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    OrderNumber = Convert.ToInt32(data.Data.Rows[i]["OrderNumber"]),
                    TargetId = Convert.ToInt32(data.Data.Rows[i]["TargetId"]),
                    Image = data.Data.Rows[i]["Image"].ToString(),
                    IsActive = Convert.ToBoolean(data.Data.Rows[i]["IsActive"])

                });
            }

            return list;
        }

        /// <summary>
        /// Banner ve tiplerini döner
        /// </summary>
        /// <returns></returns>
        public List<BannerModel> Find()
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            List<BannerModel> list = new List<BannerModel>();
            var data = db.Find("select bt.Name as BannerTypeName,b.* from BannerType bt inner join Banner b on bt.Id=b.BannerTypeId", System.Data.CommandType.Text);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new BannerModel
                {
                    BannerTypeName = data.Data.Rows[i]["BannerTypeName"].ToString(),
                    Name = data.Data.Rows[i]["Name"].ToString(),
                    Link = data.Data.Rows[i]["Link"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    OrderNumber = Convert.ToInt32(data.Data.Rows[i]["OrderNumber"]),
                    TargetId = Convert.ToInt32(data.Data.Rows[i]["TargetId"]),
                    Image = data.Data.Rows[i]["Image"].ToString(),
                    IsActive = Convert.ToBoolean(data.Data.Rows[i]["IsActive"])
                });
            }

            return list;
        }

        /// <summary>
        /// Banner ve tiplerini döner
        /// </summary>
        /// <returns></returns>
        public List<BannerModel> Get(int Id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "BannerId", Value = Id });


            List<BannerModel> list = new List<BannerModel>();
            var data = db.Find("select bt.Name as BannerTypeName,b.* from BannerType bt inner join Banner b on bt.Id=b.BannerTypeId where b.Id=@BannerId", System.Data.CommandType.Text, parameters);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new BannerModel
                {
                    BannerTypeName = data.Data.Rows[i]["BannerTypeName"].ToString(),
                    Name = data.Data.Rows[i]["Name"].ToString(),
                    Link = data.Data.Rows[i]["Link"].ToString(),
                    ShortExplanation = data.Data.Rows[i]["ShortExplanation"].ToString(),
                    OrderNumber = Convert.ToInt32(data.Data.Rows[i]["OrderNumber"]),
                    TargetId = Convert.ToInt32(data.Data.Rows[i]["TargetId"]),
                    Image = data.Data.Rows[i]["Image"].ToString(),
                    IsActive = Convert.ToBoolean(data.Data.Rows[i]["IsActive"])
                });
            }

            return list;
        }
    }
}
