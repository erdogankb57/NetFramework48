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
    public class MessageHistoryController : Controller
    {
        // GET: BannerType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<MessageHistory> request)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            if (string.IsNullOrEmpty(request.Search.Subject))
                Parameters.Add(new SqlParameter { ParameterName = "Subject", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "Subject", Value = request.Search.Subject.ToString() });

            if (request.Search.IsActive)
                Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
            else
                Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });


            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from MessageHistory where (@Subject is null or Subject like '%'+@Subject+'%') and IsActive=@IsActive order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<MessageHistory>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.ClientName,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/MessageHistory/Add','True'," + s.Id.ToString() + ")\">Düzenle</a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('MessageHistoryList','/MessageHistory/Delete',SearchDataList," + s.Id.ToString() + ")\">Sil</a>"
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
                var result = db.ExecuteNoneQuery("Delete from MessageHistory where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update MessageHistory set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update MessageHistory set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            if (id == 0)
                return PartialView("Add", new MessageHistory { IsActive = true });
            else
            {
                DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

                var model = db.Get<MessageHistory>("select * from [MessageHistory] where Id=@Id", System.Data.CommandType.Text, parameters);

                return PartialView("Add", model.Data);
            }
        }

        [HttpPost]
        public ActionResult Save(MessageHistory request)
        {
            ReturnObject<MessageHistory> result = new ReturnObject<MessageHistory>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (string.IsNullOrEmpty(request.ClientName))
                parameters.Add(new SqlParameter { ParameterName = "ClientName", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "ClientName", Value = request.ClientName });

            if (string.IsNullOrEmpty(request.ClientSurname))
                parameters.Add(new SqlParameter { ParameterName = "ClientSurname", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "ClientSurname", Value = request.ClientSurname });

            if (string.IsNullOrEmpty(request.Email))
                parameters.Add(new SqlParameter { ParameterName = "Email", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "Email", Value = request.Email });

            if (string.IsNullOrEmpty(request.Subject))
                parameters.Add(new SqlParameter { ParameterName = "Subject", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "Subject", Value = request.Subject });

            if (string.IsNullOrEmpty(request.Phone))
                parameters.Add(new SqlParameter { ParameterName = "Phone", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "Phone", Value = request.Phone });

            if (string.IsNullOrEmpty(request.Phone))
                parameters.Add(new SqlParameter { ParameterName = "Explanation", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "Explanation", Value = request.Explanation });

            if (request.IsActive)
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
            else
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });

            if (request.IsRead)
                parameters.Add(new SqlParameter { ParameterName = "IsRead", Value = 1 });
            else
                parameters.Add(new SqlParameter { ParameterName = "IsRead", Value = 0 });




            if (request.Id == 0)
            {
                parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

                db.ExecuteNoneQuery(@"insert into [MessageHistory](ClientName,
                ClientSurname,
                Email,
                Subject,
                Phone,
                Explanation,
                IsActive,
                IsRead)
                values(@ClientName,
                @ClientSurname,
                @Email,
                @Subject,
                @Phone,
                @Explanation,
                @IsActive,
                @IsRead) 
                ", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<MessageHistory>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
            else
            {


                parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                db.ExecuteNoneQuery(@"Update [MessageHistory] set 
                ClientName=@ClientName,
                ClientSurname=@ClientSurname,
                Email=@Email,
                Subject=@Subject,
                Phone=@Phone,
                Explanation=@Explanation,
                IsActive=@IsActive,
                IsRead=@IsRead
                where Id=@Id", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<MessageHistory>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
        }
    }
}