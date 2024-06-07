using Inta.Framework.Admin.Models;
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
    public class BannerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<BannerSearch> request)
        {
            AuthenticationData authenticationData = new AuthenticationData();
            List<SqlParameter> Parameters = new List<SqlParameter>();
            if (string.IsNullOrEmpty(request.Search.Name))
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Search.Name.ToString() });


            if (request.Search.IsActive == -1)
                Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = request.Search.IsActive });

            Parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = authenticationData.LanguageId });



            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from Banner where (@Name is null or Name like '%'+@Name+'%') and (@IsActive is null or IsActive=@IsActive) and LanguageId=@LanguageId order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<Banner>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/Banner/Add','False'," + s.Id.ToString() + ")\">Düzenle</a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('PagingDataList','/Banner/Delete',SearchDataList," + s.Id.ToString() + ")\">Sil</a>"
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
                var result = db.ExecuteNoneQuery("Delete from Banner where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update Banner set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update Banner set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

            var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
            if (generalSettings.Data != null)
                ViewBag.ImageFolder = generalSettings.Data.ImageCdnUrl;

            if (id ==null || id == 0)
                return View("Add", new Banner { IsActive = true });
            else
            {
                var model = db.Get<Banner>("select * from [Banner] where Id=@Id", System.Data.CommandType.Text, parameters);

                return View("Add", model.Data);
            }


        }

        [HttpPost]
        public ActionResult Save(Banner request, HttpPostedFileBase FileImage)
        {
            if (ModelState.IsValid)
            {
                AuthenticationData authenticationData = new AuthenticationData();
                ReturnObject<Banner> result = new ReturnObject<Banner>();
                DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

                if (FileImage != null)
                {
                    int imageSmallWidth = 100;
                    int imageBigWidth = 500;

                    string filepath = "";

                    var bannerType = db.Get("Select * from BannerType where Id=" + request.BannerTypeId, System.Data.CommandType.Text);
                    if (bannerType != null && bannerType.Data != null)
                    {
                        var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
                        if (generalSettings.Data != null)
                            filepath = generalSettings.Data.ImageUploadPath;

                        imageSmallWidth = !string.IsNullOrEmpty(bannerType.Data["SmallImageWidth"].ToString()) && bannerType.Data["SmallImageWidth"].ToString() != "0" ? Convert.ToInt32(bannerType.Data["SmallImageWidth"]) : 100;
                        imageBigWidth = !string.IsNullOrEmpty(bannerType.Data["BigImageWidth"].ToString()) && bannerType.Data["BigImageWidth"].ToString() != "0" ? Convert.ToInt32(bannerType.Data["BigImageWidth"]) : 500;
                        request.Image = ImageManager.ImageUploadDoubleCopy(FileImage, filepath, imageSmallWidth, imageBigWidth);
                    }

                }

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = authenticationData.LanguageId });
                parameters.Add(new SqlParameter { ParameterName = "BannerTypeId", Value = request.BannerTypeId });
                if (!string.IsNullOrEmpty(request.Name))
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Link))
                    parameters.Add(new SqlParameter { ParameterName = "Link", Value = request.Link });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Link", Value = DBNull.Value });

                parameters.Add(new SqlParameter { ParameterName = "TargetId", Value = request.TargetId });

                if (!string.IsNullOrEmpty(request.ShortExplanation))
                    parameters.Add(new SqlParameter { ParameterName = "ShortExplanation", Value = request.ShortExplanation });
                else
                    parameters.Add(new SqlParameter { ParameterName = "ShortExplanation", Value = DBNull.Value });

                parameters.Add(new SqlParameter { ParameterName = "OrderNumber", Value = request.OrderNumber });

                if (!string.IsNullOrEmpty(request.Image))
                    parameters.Add(new SqlParameter { ParameterName = "Image", Value = request.Image });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Image", Value = DBNull.Value });

                parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

                if (request.IsActive)
                    parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
                else
                    parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });

                parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = authenticationData.UserId });

                if (request.Id == 0)
                {
                    db.ExecuteNoneQuery("insert into [Banner](SystemUserId,LanguageId,BannerTypeId,Name,Link,TargetId,ShortExplanation,OrderNumber,Image,RecordDate,IsActive) values(@SystemUserId,@LanguageId,@BannerTypeId,@Name,@Link,@TargetId,@ShortExplanation,@OrderNumber,@Image,@RecordDate,@IsActive)", System.Data.CommandType.Text, parameters);

                    return Json(new ReturnObject<Banner>
                    {
                        Data = request,
                        ResultType = MessageType.Success
                    });
                }
                else
                {
                    parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                    db.ExecuteNoneQuery(@"Update [Banner] set 
                LanguageId=@LanguageId,
                BannerTypeId=@BannerTypeId,
                Name=@Name,
                Link=@Link,
                TargetId=@TargetId,
                ShortExplanation=@ShortExplanation,
                OrderNumber=@OrderNumber,
                Image=@Image,
                RecordDate=@RecordDate,
                IsActive=@IsActive 
                where Id=@Id", System.Data.CommandType.Text, parameters);

                    return Json(new ReturnObject<Banner>
                    {
                        Data = request,
                        ResultType = MessageType.Success
                    });
                }
            }
            else
            {
                return Json(new ReturnObject<Banner>
                {
                    Data = request,
                    ResultType = MessageType.Error,
                    Validation = ModelState.ToList().Where(v => v.Value.Errors.Any()).Select(s => new { Key = s.Key, Error = s.Value.Errors })
                });
            }

        }


        [HttpPost]
        public ActionResult DeleteImage(string id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });
            var result = db.ExecuteNoneQuery("Update Banner set Image='' where Id=@Id", System.Data.CommandType.Text, parameters);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}