using Inta.Framework.Admin.Models;
using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
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
    public class SystemUserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<SystemUser> request)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            if (string.IsNullOrEmpty(request.Search.Name))
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Search.Name.ToString() });

            if (string.IsNullOrEmpty(request.Search.SurName))
                Parameters.Add(new SqlParameter { ParameterName = "SurName", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "SurName", Value = request.Search.SurName.ToString() });

            if (string.IsNullOrEmpty(request.Search.UserName))
                Parameters.Add(new SqlParameter { ParameterName = "UserName", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "UserName", Value = request.Search.UserName.ToString() });

            Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = request.Search.IsActive });


            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from SystemUser where (@Name is null or Name like '%'+@Name+'%') and IsActive=@IsActive order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<SystemUser>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                SurName = s.SurName,
                UserName = s.UserName,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/SystemUser/Add','True'," + s.Id.ToString() + ")\">Düzenle</a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('PagingDataList','/SystemUser/Delete',SearchDataList," + s.Id.ToString() + ")\">Sil</a>"
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
                var result = db.ExecuteNoneQuery("Delete from StaticText where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update SystemUser set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update SystemUser set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });


            if (id == 0)
                return PartialView("Add", new SystemUser { IsActive = true });
            else
            {
                var model = db.Get<SystemUser>("select * from SystemUser where Id=@Id", System.Data.CommandType.Text, parameters);

                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]
        public ActionResult Save(SystemUser request)
        {
            AuthenticationData authenticationData = new AuthenticationData();
            ReturnObject<SystemUser> result = new ReturnObject<SystemUser>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());


            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = 0 });

            if (!string.IsNullOrEmpty(request.Name))
                parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });
            else
                parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Name))
                parameters.Add(new SqlParameter { ParameterName = "SurName", Value = request.SurName });
            else
                parameters.Add(new SqlParameter { ParameterName = "SurName", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.UserName))
                parameters.Add(new SqlParameter { ParameterName = "UserName", Value = request.UserName });
            else
                parameters.Add(new SqlParameter { ParameterName = "UserName", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Password))
                parameters.Add(new SqlParameter { ParameterName = "Password", Value = request.Password });
            else
                parameters.Add(new SqlParameter { ParameterName = "Password", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Email))
                parameters.Add(new SqlParameter { ParameterName = "Email", Value = request.Email });
            else
                parameters.Add(new SqlParameter { ParameterName = "Email", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Phone))
                parameters.Add(new SqlParameter { ParameterName = "Phone", Value = request.Phone });
            else
                parameters.Add(new SqlParameter { ParameterName = "Phone", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Address))
                parameters.Add(new SqlParameter { ParameterName = "Address", Value = request.Address });
            else
                parameters.Add(new SqlParameter { ParameterName = "Address", Value = DBNull.Value });

            parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });


            if (request.Id == 0)
            {
                parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

                db.ExecuteNoneQuery(@"insert into [SystemUser](
                Name,
                SurName,
                UserName,
                Password,
                Email,
                Phone,
                Address,
                IsActive
                ) values(
                @Name,
                @SurName,
                @UserName,
                @Password,
                @Email,
                @Phone,
                @Address,
                @IsActive
                )", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<SystemUser>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
            else
            {
                parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                db.ExecuteNoneQuery(@"Update [SystemUser] set 
                Name=@Name,
                SurName=@SurName,
                UserName=@UserName,
                Password=@Password,
                Email=@Email,
                Phone=@Phone,
                Address=@Address,
                IsActive=@IsActive                
                where Id=@Id", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<SystemUser>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
        }
    }
}