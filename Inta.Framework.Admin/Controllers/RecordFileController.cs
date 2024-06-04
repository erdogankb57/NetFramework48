using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
using Inta.Framework.Extension;
using Inta.Framework.Web.Base.Authorization;
using Inta.Framework.Web.Base.FormControls;
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
    public class RecordFileController : Controller
    {
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
                HttpContext.Session["RecordId"] = id ?? 0;

            return View();
        }

        public ActionResult GetList(PagingDataListRequest<RecordFile> request)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            if (string.IsNullOrEmpty(request.Search.Name))
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Search.Name.ToString() });

            Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = request.Search.IsActive });


            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from RecordFile where (@Name is null or Name like '%'+@Name+'%') and IsActive=@IsActive order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<RecordFile>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/RecordFile/Add','True'," + s.Id.ToString() + ")\">Düzenle</a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('PagingDataList','/RecordFile/Delete',SearchDataList," + s.Id.ToString() + ")\">Sil</a>"
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
                var result = db.ExecuteNoneQuery("Delete from RecordFile where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update RecordFile set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update RecordFile set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

            ViewBag.ImageFolder = System.Configuration.ConfigurationManager.AppSettings["ImageUpload"].ToString();

            if (id == 0)
                return PartialView("Add", new RecordFile { IsActive = true });
            else
            {
                var model = db.Get<RecordFile>("select * from [RecordFile] where Id=@Id", System.Data.CommandType.Text, parameters);

                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]
        public ActionResult Save(RecordFile request, HttpPostedFileBase FileName)
        {
            AuthenticationData authenticationData = new AuthenticationData();
            ReturnObject<RecordFile> result = new ReturnObject<RecordFile>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

            string filepath = ConfigurationManager.AppSettings["ImageUpload"].ToString();

            if (FileName != null)
            {
                request.FileName = ImageManager.ImageUploadSingleCopy(FileName, filepath);
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = authenticationData.LanguageId });
            if (!string.IsNullOrEmpty(request.Name))
                parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });
            else
                parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.ShortExplanation))
                parameters.Add(new SqlParameter { ParameterName = "ShortExplanation", Value = request.Name });
            else
                parameters.Add(new SqlParameter { ParameterName = "ShortExplanation", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Explanation))
                parameters.Add(new SqlParameter { ParameterName = "Explanation", Value = request.Name });
            else
                parameters.Add(new SqlParameter { ParameterName = "Explanation", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.FileName))
                parameters.Add(new SqlParameter { ParameterName = "FileName", Value = request.FileName });
            else
                parameters.Add(new SqlParameter { ParameterName = "FileName", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.FileTagName))
                parameters.Add(new SqlParameter { ParameterName = "FileTagName", Value = request.FileTagName });
            else
                parameters.Add(new SqlParameter { ParameterName = "FileTagName", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.FileTitleName))
                parameters.Add(new SqlParameter { ParameterName = "FileTitleName", Value = request.FileTitleName });
            else
                parameters.Add(new SqlParameter { ParameterName = "FileTitleName", Value = DBNull.Value });

            parameters.Add(new SqlParameter { ParameterName = "TargetId", Value = request.TargetId });
            parameters.Add(new SqlParameter { ParameterName = "HomePageStatus", Value = request.HomePageStatus });
            parameters.Add(new SqlParameter { ParameterName = "OrderNumber", Value = request.OrderNumber });

            if (request.IsActive)
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
            else
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });

            parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = authenticationData.UserId });



            if (request.Id == 0)
            {
                db.ExecuteNoneQuery(@"insert into 
                RecordFile(
                SystemUserId,
                Name,
                ShortExplanation,
                Explanation,
                FileName,
                FileTagName,
                FileTitleName,
                TargetId,
                HomePageStatus,
                OrderNumber,
                IsActive) values(
                @SystemUserId,
                @Name,
                @ShortExplanation,
                @Explanation,
                @FileName,
                @FileTagName,
                @FileTitleName,
                @TargetId,
                @HomePageStatus,
                @OrderNumber,
                @IsActive)", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<RecordFile>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
            else
            {
                parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                db.ExecuteNoneQuery(@"Update [RecordFile] set 
                Name=@Name,
                ShortExplanation=@ShortExplanation,
                Explanation=@Explanation,
                ImageName=@ImageName,
                ImageTagName=@ImageTagName,
                ImageTitleName=@ImageTitleName,
                TargetId=@TargetId,
                HomePageStatus=@HomePageStatus,
                OrderNumber=@OrderNumber,
                IsActive=@IsActive
                where Id=@Id", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<RecordFile>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
        }
    }
}