using Inta.Framework.Admin.Models;
using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
using Inta.Framework.Admin.Base.Authorization;
using Inta.Framework.Admin.Base.FormControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Areas.Admin.Controllers
{

    [AuthorizationCheck]
    public class SystemActionController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<SystemActionSearch> request)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            if (string.IsNullOrEmpty(request.Search.ControllerName))
                Parameters.Add(new SqlParameter { ParameterName = "ControllerName", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "ControllerName", Value = request.Search.ControllerName.ToString() });

            if (string.IsNullOrEmpty(request.Search.ActionName))
                Parameters.Add(new SqlParameter { ParameterName = "ActionName", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "ActionName", Value = request.Search.ActionName.ToString() });


            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from SystemAction where (@ControllerName is null or ControllerName like '%'+@ControllerName+'%') and (@ActionName is null or ActionName like '%'+@ActionName+'%') order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<SystemAction>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                ControllerName = s.ControllerName,
                ActionName = s.ActionName,
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/Admin/SystemAction/Add','True'," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/edit-icon.png' width='20'/></a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('SystemActionList','/Admin/SystemAction/Delete',SearchDataList," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/delete-icon.png' width='20'/></a>"
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
                var result = db.ExecuteNoneQuery("Delete from SystemAction where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

            if (id == 0)
                return PartialView("Add", new SystemAction());
            else
            {
                var model = db.Get<SystemAction>("select * from [SystemAction] where Id=@Id", System.Data.CommandType.Text, parameters);

                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]
        public ActionResult Save(SystemAction request, HttpPostedFileBase FileImage)
        {
            if (ModelState.IsValid)
            {
                ReturnObject<SystemAction> result = new ReturnObject<SystemAction>();
                DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter { ParameterName = "SystemMenuId", Value = request.SystemMenuId });
                if (!string.IsNullOrEmpty(request.ControllerName))
                    parameters.Add(new SqlParameter { ParameterName = "ControllerName", Value = request.ControllerName });
                else
                    parameters.Add(new SqlParameter { ParameterName = "ControllerName", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.ActionName))
                    parameters.Add(new SqlParameter { ParameterName = "ActionName", Value = request.ActionName });
                else
                    parameters.Add(new SqlParameter { ParameterName = "ActionName", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Description))
                    parameters.Add(new SqlParameter { ParameterName = "Description", Value = request.Description });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Description", Value = DBNull.Value });

                parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = AuthenticationData.UserId });



                if (request.Id == 0)
                {
                    db.ExecuteNoneQuery(@"insert into [SystemAction]
                (
                SystemUserId,
                SystemMenuId,
                ControllerName,
                ActionName,
                Description) 
                values(
                @SystemUserId,
                @SystemMenuId,
                @ControllerName,
                @ActionName,
                @Description)", System.Data.CommandType.Text, parameters);

                    return Json(new ReturnObject<SystemAction>
                    {
                        Data = request,
                        ResultType = MessageType.Success
                    });
                }
                else
                {
                    parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                    db.ExecuteNoneQuery(@"Update [SystemAction] set 
                SystemMenuId=@SystemMenuId,
                ControllerName=@ControllerName,
                ActionName=@ActionName,
                Description=@Description
                where Id=@Id", System.Data.CommandType.Text, parameters);

                    return Json(new ReturnObject<SystemAction>
                    {
                        Data = request,
                        ResultType = MessageType.Success
                    });
                }
            }
            else
            {
                return Json(new ReturnObject<SystemAction>
                {
                    Data = request,
                    ResultType = MessageType.Error,
                    Validation = ModelState.ToList().Where(v => v.Value.Errors.Any()).Select(s => new { Key = s.Key, Error = s.Value.Errors })
                });
            }
        }
    }
}