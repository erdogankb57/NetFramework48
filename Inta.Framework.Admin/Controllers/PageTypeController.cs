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

namespace Inta.Framework.Admin.Controllers
{

    [AuthorizationCheck]
    public class PageTypeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<PageTypeSearch> request)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            if (string.IsNullOrEmpty(request.Search.Name))
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Search.Name.ToString() });

            if (request.Search.IsActive == -1)
                Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = request.Search.IsActive });

            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from PageType where (@Name is null or Name like '%'+@Name+'%') and (@IsActive is null or IsActive=@IsActive) order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<PageType>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/PageType/Add','True'," + s.Id.ToString() + ")\"><img src='/Content/images/edit-icon.png' width='20'/></a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('PageTypeList','/PageType/Delete',SearchDataList," + s.Id.ToString() + ")\"><img src='/Content/images/delete-icon.png' width='20'/></a>"
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
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update PageType set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update PageType set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Delete from PageType where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

            if (id == 0)
                return PartialView("Add", new PageType());
            else
            {
                var model = db.Get<PageType>("select * from PageType where Id=@Id", System.Data.CommandType.Text, parameters);

                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]
        public ActionResult Save(PageType request, HttpPostedFileBase FileImage)
        {
            if (ModelState.IsValid)
            {

                ReturnObject<PageType> result = new ReturnObject<PageType>();
                DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

                List<SqlParameter> parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(request.Code))
                    parameters.Add(new SqlParameter { ParameterName = "Code", Value = request.Code });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Code", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Name))
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.ControllerName))
                    parameters.Add(new SqlParameter { ParameterName = "ControllerName", Value = request.ControllerName });
                else
                    parameters.Add(new SqlParameter { ParameterName = "ControllerName", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.ActionName))
                    parameters.Add(new SqlParameter { ParameterName = "ActionName", Value = request.ActionName });
                else
                    parameters.Add(new SqlParameter { ParameterName = "ActionName", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.ViewName))
                    parameters.Add(new SqlParameter { ParameterName = "ViewName", Value = request.ViewName });
                else
                    parameters.Add(new SqlParameter { ParameterName = "ViewName", Value = DBNull.Value });

                parameters.Add(new SqlParameter { ParameterName = "IsExplanationEnabled", Value = request.IsExplanationEnabled });
                parameters.Add(new SqlParameter { ParameterName = "IsShortExplanationEnabled", Value = request.IsShortExplanationEnabled });
                parameters.Add(new SqlParameter { ParameterName = "IsMenuFirstRecord", Value = request.IsMenuFirstRecord });
                parameters.Add(new SqlParameter { ParameterName = "IsMenuFirstCategory", Value = request.IsMenuFirstCategory });
                parameters.Add(new SqlParameter { ParameterName = "IsPagingEnabled", Value = request.IsPagingEnabled });
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = request.IsActive });

                parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = AuthenticationData.UserId });



                if (request.Id == 0)
                {
                    parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

                    db.ExecuteNoneQuery(@"insert into [PageType]
                (
                SystemUserId,
                Code,
                Name,
                ControllerName,
                ActionName,
                ViewName,
                IsExplanationEnabled,
                IsShortExplanationEnabled,
                IsMenuFirstRecord,
                IsMenuFirstCategory,
                IsPagingEnabled,
                IsActive) 
                values
                (
                @SystemUserId,
                @Code,
                @Name,
                @ControllerName,
                @ActionName,
                @ViewName,
                @IsExplanationEnabled,
                @IsShortExplanationEnabled,
                @IsMenuFirstRecord,
                @IsMenuFirstCategory,
                @IsPagingEnabled,
                @IsActive) ", System.Data.CommandType.Text, parameters);

                    return Json(new ReturnObject<PageType>
                    {
                        Data = request,
                        ResultType = MessageType.Success
                    });
                }
                else
                {
                    parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                    db.ExecuteNoneQuery(@"Update [PageType] set 
                Code=@Code,
                Name=@Name,
                ControllerName=@ControllerName,
                ActionName=@ActionName,
                ViewName=@ViewName,
                IsExplanationEnabled=@IsExplanationEnabled,
                IsShortExplanationEnabled=@IsShortExplanationEnabled,
                IsMenuFirstRecord=@IsMenuFirstRecord,
                IsMenuFirstCategory=@IsMenuFirstCategory,   
                IsPagingEnabled=@IsPagingEnabled,
                IsActive=@IsActive
                where Id=@Id", System.Data.CommandType.Text, parameters);

                    return Json(new ReturnObject<PageType>
                    {
                        Data = request,
                        ResultType = MessageType.Success
                    });
                }
            }
            else
            {
                return Json(new ReturnObject<PageType>
                {
                    Data = request,
                    ResultType = MessageType.Error,
                    Validation = ModelState.ToList().Where(v => v.Value.Errors.Any()).Select(s => new { Key = s.Key, Error = s.Value.Errors })
                });
            }
        }
    }
}