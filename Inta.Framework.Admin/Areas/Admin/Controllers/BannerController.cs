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
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Areas.Admin.Controllers
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
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/Admin/Banner/Add','False'," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/edit-icon.png' width='20'/></a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('PagingDataList','/Admin/Banner/Delete',SearchDataList," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/delete-icon.png' width='20'/></a>"
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
                var banner = db.Get<Banner>("select * from Banner where id=" + item, System.Data.CommandType.Text).Data;
                if (banner != null)
                {
                    DeleteImageFile(banner.Image);
                    var result = db.ExecuteNoneQuery("Delete from Banner where id=" + item, System.Data.CommandType.Text);
                }
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

            ViewBag.ImageFolder = ConfigurationManager.AppSettings["ImageUpload"].ToString();

            if (id == null || id == 0)
                return View("Add", new Banner { IsActive = true });
            else
            {
                var model = db.Get<Banner>("select * from [Banner] where Id=@Id", System.Data.CommandType.Text, parameters);

                var bannerType = db.Get("Select * from BannerType where Id=" + model.Data.BannerTypeId, System.Data.CommandType.Text);
                if (bannerType != null && bannerType.Data != null)
                {

                    ViewBag.ImageSmallWidth = !string.IsNullOrEmpty(bannerType.Data["SmallImageWidth"].ToString()) && bannerType.Data["SmallImageWidth"].ToString() != "0" ? Convert.ToInt32(bannerType.Data["SmallImageWidth"]) : 100;
                    ViewBag.ImageBigWidth = !string.IsNullOrEmpty(bannerType.Data["BigImageWidth"].ToString()) && bannerType.Data["BigImageWidth"].ToString() != "0" ? Convert.ToInt32(bannerType.Data["BigImageWidth"]) : 500;
                    ViewBag.ImageSmallHeight = !string.IsNullOrEmpty(bannerType.Data["SmallImageHeight"].ToString()) && bannerType.Data["SmallImageHeight"].ToString() != "0" ? Convert.ToInt32(bannerType.Data["SmallImageHeight"]) : 100;
                    ViewBag.ImageBigHeight = !string.IsNullOrEmpty(bannerType.Data["BigImageHeight"].ToString()) && bannerType.Data["BigImageHeight"].ToString() != "0" ? Convert.ToInt32(bannerType.Data["BigImageHeight"]) : 500;

                }

                return View("Add", model.Data);
            }


        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(Banner request, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                ReturnObject<Banner> result = new ReturnObject<Banner>();
                DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
                var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);

                if (Image != null)
                {
                    int imageSmallWidth = 100;
                    int imageBigWidth = 500;

                    string filepath = "";

                    var bannerType = db.Get("Select * from BannerType where Id=" + request.BannerTypeId, System.Data.CommandType.Text);
                    if (bannerType != null && bannerType.Data != null)
                    {
                        filepath = Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"].ToString());

                        imageSmallWidth = !string.IsNullOrEmpty(bannerType.Data["SmallImageWidth"].ToString()) && bannerType.Data["SmallImageWidth"].ToString() != "0" ? Convert.ToInt32(bannerType.Data["SmallImageWidth"]) : 100;
                        imageBigWidth = !string.IsNullOrEmpty(bannerType.Data["BigImageWidth"].ToString()) && bannerType.Data["BigImageWidth"].ToString() != "0" ? Convert.ToInt32(bannerType.Data["BigImageWidth"]) : 500;
                        request.Image = ImageManager.ImageUploadDoubleCopy(Image, filepath, imageSmallWidth, imageBigWidth);
                    }

                }

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = AuthenticationData.LanguageId });
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

                if (Image != null)
                    parameters.Add(new SqlParameter { ParameterName = "Image", Value = request.Image });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Image", Value = DBNull.Value });

                parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

                if (request.IsActive)
                    parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
                else
                    parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });

                parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = AuthenticationData.UserId });


                if (request.Id == 0)
                {
                    var inserted = db.ExecuteScalar("insert into [Banner](SystemUserId,LanguageId,BannerTypeId,Name,Link,TargetId,ShortExplanation,OrderNumber,Image,RecordDate,IsActive) values(@SystemUserId,@LanguageId,@BannerTypeId,@Name,@Link,@TargetId,@ShortExplanation,@OrderNumber,@Image,@RecordDate,@IsActive); SELECT SCOPE_IDENTITY()", System.Data.CommandType.Text, parameters);

                    var banner = db.Get<Banner>("Select * from Banner where Id=" + inserted.Data, System.Data.CommandType.Text);
                    var bannerType = db.Get<BannerType>("Select * from BannerType where Id=" + Convert.ToInt32(banner.Data.BannerTypeId), System.Data.CommandType.Text);

                    string RedirectUrl = Image != null ? $"/Admin/ImageCrop/Index?ImageName={banner.Data.Image}&Dimension=b_&width={bannerType.Data.BigImageWidth}&height={bannerType.Data.BigImageHeight}&SaveUrl=/Admin/Banner/Index" : $"/Admin/Banner/Index";

                    return RedirectToAction("Success", "Message", new { area = "Admin", RedirectUrl = RedirectUrl, Message = "Kayıt ekleme işlemi başarıyla tamamlandı" });
                }
                else
                {
                    if (Image != null)
                    {
                        var bannerImage = db.Get("Select * from Banner where Id=" + Convert.ToInt32(request.Id), System.Data.CommandType.Text);

                        if (bannerImage.Data != null && bannerImage.Data["Image"] != null)
                            DeleteImageFile(bannerImage.Data["Image"].ToString());
                    }
                    parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                    StringBuilder shtml = new StringBuilder();

                    shtml.Append(@"Update [Banner] set 
                    LanguageId=@LanguageId,
                    BannerTypeId=@BannerTypeId,
                    Name=@Name,
                    Link=@Link,
                    TargetId=@TargetId,
                    ShortExplanation=@ShortExplanation,
                    OrderNumber=@OrderNumber,");

                    if (Image != null)
                    {
                        shtml.Append("Image=@Image,");
                    }

                    shtml.Append(@"RecordDate=@RecordDate,
                    IsActive=@IsActive 
                    where Id=@Id");


                    db.ExecuteNoneQuery(shtml.ToString(), System.Data.CommandType.Text, parameters);

                    var banner = db.Get<Banner>("Select * from Banner where Id=" + Convert.ToInt32(request.Id), System.Data.CommandType.Text);
                    var bannerType = db.Get<BannerType>("Select * from BannerType where Id=" + Convert.ToInt32(banner.Data.BannerTypeId), System.Data.CommandType.Text);


                    string RedirectUrl = Image != null ? $"/Admin/ImageCrop/Index?ImageName={banner.Data.Image}&Dimension=b_&width={bannerType.Data.BigImageWidth}&height={bannerType.Data.BigImageHeight}&SaveUrl=/Admin/Banner/Index" : $"/Admin/Banner/Index";

                    return RedirectToAction("Success", "Message", new { area = "Admin", RedirectUrl = RedirectUrl, Message = "Kayıt güncelleme işlemi başarıyla tamamlandı" });

                }
            }
            else
            {
                return View("~/Areas/Admin/Views/Banner/Add.cshtml", request);
            }

        }


        [HttpPost]
        public ActionResult DeleteImage(string id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });
            var banner = db.Get<Banner>("select * from Banner where Id=" + Convert.ToInt32(id), System.Data.CommandType.Text).Data;
            if (banner != null && !string.IsNullOrEmpty(banner.Image))
            {
                DeleteImageFile(banner.Image);
                var result = db.ExecuteNoneQuery("Update Banner set Image='' where Id=@Id", System.Data.CommandType.Text, parameters);
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        private void DeleteImageFile(string Image)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

            var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
            string filepath = generalSettings.Data.ImageUploadPath;
            if (System.IO.File.Exists(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + "k_" + Image))
                System.IO.File.Delete(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + "k_" + Image);

            if (System.IO.File.Exists(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + "b_" + Image))
                System.IO.File.Delete(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + "b_" + Image);

            if (System.IO.File.Exists(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + Image))
                System.IO.File.Delete(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + Image);

        }
    }
}