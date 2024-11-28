using Inta.Framework.Admin.Models;
using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
using Inta.Framework.Extension;
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
    public class RecordImageController : Controller
    {
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
                HttpContext.Session["RecordId"] = id ?? 0;

            return View();
        }

        public ActionResult GetList(PagingDataListRequest<RecordImageSearch> request)
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

            Parameters.Add(new SqlParameter { ParameterName = "RecordId", Value = HttpContext.Session["RecordId"] });


            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from RecordImage where RecordId=@RecordId and (@Name is null or Name like '%'+@Name+'%') and (@IsActive is null or IsActive=@IsActive) order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<RecordImage>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/Admin/RecordImage/Add','True'," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/edit-icon.png' width='20'/></a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('PagingDataList','/Admin/RecordImage/Delete',SearchDataList," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/delete-icon.png' width='20'/></a>"
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
                var result = db.Get<RecordImage>("select * from RecordImage where id=" + Convert.ToInt32(item), System.Data.CommandType.Text);
                if (result.Data != null)
                {
                    DeleteImageFile(result.Data.ImageName);
                    db.ExecuteNoneQuery("Delete from RecordImage where id=" + item, System.Data.CommandType.Text);

                }
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        private void DeleteImageFile(string Image)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

            var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
            string filepath = generalSettings.Data.ImageUploadPath;
            if (System.IO.File.Exists(generalSettings.Data.ImageUploadPath + "\\" + "k_" + Image))
                System.IO.File.Delete(generalSettings.Data.ImageUploadPath + "\\" + "k_" + Image);

            if (System.IO.File.Exists(generalSettings.Data.ImageUploadPath + "\\" + "b_" + Image))
                System.IO.File.Delete(generalSettings.Data.ImageUploadPath + "\\" + "b_" + Image);

            if (System.IO.File.Exists(generalSettings.Data.ImageUploadPath + "\\" + Image))
                System.IO.File.Delete(generalSettings.Data.ImageUploadPath + "\\" + Image);

        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update RecordImage set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update RecordImage set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

            var settings = db.Get<GeneralSettings>("select top 1 * from GeneralSettings", System.Data.CommandType.Text);
            ViewBag.ImageFolder = settings.Data.ImageCdnUrl;

            if (id == 0)
                return PartialView("Add", new RecordImage { IsActive = true });
            else
            {
                var model = db.Get<RecordImage>("select * from [RecordImage] where Id=@Id", System.Data.CommandType.Text, parameters);

                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]
        public ActionResult Save(RecordImage request, HttpPostedFileBase ImageName)
        {
            ReturnObject<RecordImage> result = new ReturnObject<RecordImage>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

            string filepath = "";

            if (ImageName != null)
            {
                int imageSmallWidth = 100;
                int imageBigWidth = 500;

                var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
                if (generalSettings.Data != null)
                {
                    imageSmallWidth = generalSettings.Data.GalleryImageSmallWidth;
                    imageBigWidth = generalSettings.Data.GalleryImageBigWidth;
                    filepath = generalSettings.Data.ImageUploadPath;
                }

                request.ImageName = ImageManager.ImageUploadDoubleCopy(ImageName, filepath, imageSmallWidth, imageBigWidth);
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = AuthenticationData.LanguageId });
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

            if (!string.IsNullOrEmpty(request.ImageName))
                parameters.Add(new SqlParameter { ParameterName = "ImageName", Value = request.ImageName });
            else
                parameters.Add(new SqlParameter { ParameterName = "ImageName", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.ImageTagName))
                parameters.Add(new SqlParameter { ParameterName = "ImageTagName", Value = request.ImageTagName });
            else
                parameters.Add(new SqlParameter { ParameterName = "ImageTagName", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.ImageTitleName))
                parameters.Add(new SqlParameter { ParameterName = "ImageTitleName", Value = request.ImageTitleName });
            else
                parameters.Add(new SqlParameter { ParameterName = "ImageTitleName", Value = DBNull.Value });

            parameters.Add(new SqlParameter { ParameterName = "TargetId", Value = request.TargetId });
            parameters.Add(new SqlParameter { ParameterName = "HomePageStatus", Value = request.HomePageStatus });
            parameters.Add(new SqlParameter { ParameterName = "OrderNumber", Value = request.OrderNumber });

            if (request.IsActive)
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
            else
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });

            parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = AuthenticationData.UserId });




            if (request.Id == 0)
            {

                parameters.Add(new SqlParameter { ParameterName = "RecordId", Value = HttpContext.Session["RecordId"] });

                db.ExecuteNoneQuery(@"insert into 
                RecordImage(
                RecordId,
                SystemUserId,
                Name,
                ShortExplanation,
                Explanation,
                ImageName,
                ImageTagName,
                ImageTitleName,
                TargetId,
                HomePageStatus,
                OrderNumber,
                IsActive) values(
                @RecordId,
                @SystemUserId,
                @Name,
                @ShortExplanation,
                @Explanation,
                @ImageName,
                @ImageTagName,
                @ImageTitleName,
                @TargetId,
                @HomePageStatus,
                @OrderNumber,
                @IsActive)", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<RecordImage>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
            else
            {
                if (ImageName != null)
                {
                    var recordImage = db.Get("Select * from RecordImage where Id=" + Convert.ToInt32(request.Id), System.Data.CommandType.Text);

                    if (recordImage.Data != null && recordImage.Data["ImageName"] != null)
                        DeleteImageFile(recordImage.Data["ImageName"].ToString());
                }

                parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                db.ExecuteNoneQuery(@"Update [RecordImage] set 
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

                return Json(new ReturnObject<RecordImage>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
        }


        [HttpPost]
        public ActionResult DeleteImage(string id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
  

            var result = db.Get<RecordImage>("select * from RecordImage where id=" + Convert.ToInt32(id), System.Data.CommandType.Text);
            if (result.Data != null)
            {
                DeleteImageFile(result.Data.ImageName);

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });
                db.ExecuteNoneQuery("Update RecordImage set ImageName='' where Id=@Id", System.Data.CommandType.Text, parameters);
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}