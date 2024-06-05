using Inta.Framework.Admin.Models;
using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
using Inta.Framework.Web.Base.Authorization;
using Inta.Framework.Web.Base.FormControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Controllers
{
    [AuthorizationCheck]
    public class FirmVariablesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<FirmVariablesSearch> request)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            if (string.IsNullOrEmpty(request.Search.Name))
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Search.Name.ToString() });

            Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = request.Search.IsActive });


            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from FirmVariables where (@Name is null or Name like '%'+@Name+'%') and IsActive=@IsActive order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<FirmVariables>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                Value = s.Value,
                Description = s.Description,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/FirmVariables/Add','True'," + s.Id.ToString() + ")\">Düzenle</a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('PagingDataList','/FirmVariables/Delete',SearchDataList," + s.Id.ToString() + ")\">Sil</a>"
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
                var result = db.ExecuteNoneQuery("Delete from FirmVariables where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update FirmVariables set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update FirmVariables set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });


            if (id == 0)
                return PartialView("Add", new FirmVariables { IsActive = true });
            else
            {
                var model = db.Get<FirmVariables>("select * from [FirmVariables] where Id=@Id", System.Data.CommandType.Text, parameters);

                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]
        public ActionResult Save(FirmVariables request)
        {
            AuthenticationData authenticationData = new AuthenticationData();
            ReturnObject<FirmVariables> result = new ReturnObject<FirmVariables>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());


            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = authenticationData.LanguageId });

            if (!string.IsNullOrEmpty(request.Name))
                parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });
            else
                parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Value))
                parameters.Add(new SqlParameter { ParameterName = "Value", Value = request.Value});
            else
                parameters.Add(new SqlParameter { ParameterName = "Value", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Description))
                parameters.Add(new SqlParameter { ParameterName = "Description", Value = request.Description });
            else
                parameters.Add(new SqlParameter { ParameterName = "Description", Value = DBNull.Value });

            parameters.Add(new SqlParameter { ParameterName = "OrderNumber", Value = request.OrderNumber });
            parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });

            parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = authenticationData.UserId });



            if (request.Id == 0)
            {
                parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

                db.ExecuteNoneQuery(@"insert into [FirmVariables](
                SystemUserId,
                LanguageId,
                Name,
                Value,
                Description,
                OrderNumber,
                RecordDate,
                IsActive
                ) values(
                @SystemUserId,
                @LanguageId,
                @Name,
                @Value,
                @Description,
                @OrderNumber,
                @RecordDate,
                @IsActive
                )", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<FirmVariables>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
            else
            {
                parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                db.ExecuteNoneQuery(@"Update [FirmVariables] set 
                Name=@Name,
                Value=@Value,
                Description=@Description,
                OrderNumber=@OrderNumber,
                IsActive=@IsActive,
                where Id=@Id", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<FirmVariables>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
        }
    }
}