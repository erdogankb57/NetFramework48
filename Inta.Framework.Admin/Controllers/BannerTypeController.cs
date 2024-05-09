using Inta.Framework.Admin.Models;
using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Web.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Controllers
{
    [AuthorizationCheck]
    public class BannerTypeController : Controller
    {
        // GET: BannerType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<BannerType> request)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            if (string.IsNullOrEmpty(request.Search.Name))
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Search.Name.ToString() });

            Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = request.Search.IsActive });


            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from BannerType where (@Name is null or Name like '%'+@Name+'%') and IsActive=@IsActive order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<BannerType>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/BannerType/Add','True'," + s.Id.ToString() + ")\">Düzenle</a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('BannerTypeList','/BannerType/Delete',SearchDataList," + s.Id.ToString() + ")\">Sil</a>"
            }).ToList();


            var result = new ReturnPaginationObject<object>
            {
                Data = selectData,
                ResultType = MessageType.Success,
                DataCount = data.Data.Count,
                PageCount = (int)Math.Ceiling((decimal)count / (decimal)(request.PageRowCount))
            };

            return Json(result);
        }

        [HttpPost]
        public ActionResult Delete(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Delete from BannerType where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update BannerType set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update BannerType set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

            var model = db.Get<BannerType>("select * from [BannerType] where Id=@Id", System.Data.CommandType.Text, parameters);
            return PartialView("Add", model.Data);
        }

        [HttpPost]
        public ActionResult Save(BannerType request)
        {
            ReturnObject<BannerType> result = new ReturnObject<BannerType>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (string.IsNullOrEmpty(request.Name))
                parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });

            parameters.Add(new SqlParameter { ParameterName = "SmallImageWidth", Value = request.SmallImageWidth });
            parameters.Add(new SqlParameter { ParameterName = "BigImageWidth", Value = request.BigImageWidth });
            if (request.IsActive)
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
            else
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });

            if (string.IsNullOrEmpty(request.Description))
                parameters.Add(new SqlParameter { ParameterName = "Description", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "Description", Value = request.Description });


            if (request.Id == 0)
            {
                db.ExecuteNoneQuery("insert into [BannerType](Name,SmallImageWidth,BigImageWidth,Description,IsActive) values(@Name,@SmallImageWidth,@BigImageWidth,@Description,@IsActive)", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<BannerType>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
            else
            {


                parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });


                db.ExecuteNoneQuery("Update [BannerType] set Name=@Name,SmallImageWidth=@SmallImageWidth,BigImageWidth=@BigImageWidth,IsActive=@IsActive,Description=@Description where Id=@Id", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<BannerType>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
        }
    }
}