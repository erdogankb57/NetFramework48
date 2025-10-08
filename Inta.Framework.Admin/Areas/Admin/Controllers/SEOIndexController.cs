using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
using Inta.Framework.Web.Areas.Admin.Models;
using Inta.Framework.Web.Base.Authorization;
using Inta.Framework.Web.Base.FormControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Inta.Framework.Web.Areas.Admin.Controllers
{
    [AuthorizationCheck]
    public class SEOIndexController : Controller
    {
        // GET: BannerType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<SEOIndexSearch> request)
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

            Parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = AuthenticationData.LanguageId });



            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from SEOIndex where (@Name is null or Name like '%'+@Name+'%') and LanguageId=@LanguageId and (@IsActive is null or IsActive=@IsActive) order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<SEOIndex>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/Admin/SEOIndex/Add','True'," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/edit-icon.png' width='20'/></a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('SEOIndexList','/Admin/SEOIndex/Delete',SearchDataList," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/delete-icon.png' width='20'/></a>"
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
                var result = db.ExecuteNoneQuery("Delete from SEOIndex where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update SEOIndex set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update SEOIndex set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            if (id == 0)
                return PartialView("Add", new SEOIndex { IsActive = true });
            else
            {
                DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

                var model = db.Get<SEOIndex>("select * from [SEOIndex] where Id=@Id", System.Data.CommandType.Text, parameters);
                return PartialView("Add", model.Data);
            }
        }

        [HttpPost]
        public ActionResult Save(SEOIndex request)
        {
            if (ModelState.IsValid)
            {
                ReturnObject<SEOIndex> result = new ReturnObject<SEOIndex>();
                DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (string.IsNullOrEmpty(request.Name))
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });

                if (string.IsNullOrEmpty(request.Url))
                    parameters.Add(new SqlParameter { ParameterName = "Url", Value = DBNull.Value });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Url", Value = request.Url });

                if (string.IsNullOrEmpty(request.RedirectUrl))
                    parameters.Add(new SqlParameter { ParameterName = "RedirectUrl", Value = DBNull.Value });
                else
                    parameters.Add(new SqlParameter { ParameterName = "RedirectUrl", Value = request.RedirectUrl });

                if (request.IsActive)
                    parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
                else
                    parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });

                parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = AuthenticationData.LanguageId });
                parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = AuthenticationData.UserId });




                if (request.Id == 0)
                {
                    parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

                    db.ExecuteNoneQuery(@"insert into [SEOIndex](
                SystemUserId,
                LanguageId,
                Name,
                Url,
                RedirectUrl,
                RecordDate,
                IsActive) 
                values(
                @SystemUserId,
                @LanguageId,
                @Name,
                @Url,
                @RedirectUrl,
                @RecordDate,
                @IsActive)", System.Data.CommandType.Text, parameters);

                    return Json(new ReturnObject<SEOIndex>
                    {
                        Data = request,
                        ResultType = MessageType.Success
                    });
                }
                else
                {


                    parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                    db.ExecuteNoneQuery(@"Update [SEOIndex] set 
                LanguageId=@LanguageId,
                Name=@Name,
                Url=@Url,
                RedirectUrl=@RedirectUrl,
                IsActive=@IsActive
                where Id=@Id", System.Data.CommandType.Text, parameters);

                    return Json(new ReturnObject<SEOIndex>
                    {
                        Data = request,
                        ResultType = MessageType.Success
                    });
                }
            }
            else
            {
                return Json(new ReturnObject<SEOIndex>
                {
                    Data = request,
                    ResultType = MessageType.Error,
                    Validation = ModelState.ToList().Where(v => v.Value.Errors.Any()).Select(s => new { Key = s.Key, Error = s.Value.Errors })
                });
            }
        }
    }
}