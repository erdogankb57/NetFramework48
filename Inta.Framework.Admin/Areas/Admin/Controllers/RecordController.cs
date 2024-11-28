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
using System.Transactions;

namespace Inta.Framework.Admin.Areas.Admin.Controllers
{
    [AuthorizationCheck]
    public class RecordController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<RecordSearch> request)
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

            if (request.Search.CategoryId == null || request.Search.CategoryId == 0)
                Parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = request.Search.CategoryId });

            Parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = AuthenticationData.LanguageId });

            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from Record where (@Name is null or Name like '%'+@Name+'%') and LanguageId=@LanguageId and (@IsActive is null or IsActive=@IsActive) and (@CategoryId is null or CategoryId=@CategoryId) order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<Record>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                RecordImage = "<a href='/RecordImage/Index/" + s.Id + "'><img src='/Areas/Admin/Content/images/photo-icon.png' width='20'/></a>",
                RecordFile = "<a href='/RecordFile/Index/" + s.Id + "'><img src='/Areas/Admin/Content/images/file-icon.png' width='20'/></a>",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/Admin/Record/Add','False'," + s.Id.ToString() + ",AddCallBack)\"><img src='/Areas/Admin/Content/images/edit-icon.png' width='20'/></a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('RecordList','/Admin/Record/Delete',SearchDataList," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/delete-icon.png' width='20'/></a>"
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
            ReturnObject<Record> returnObject = new ReturnObject<Record>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    foreach (var item in ids.Split(',').ToList())
                    {
                        var recordList = db.Find<Record>("Select * from Record where Id=" + Convert.ToInt32(item), System.Data.CommandType.Text);
                        if (recordList.Data != null)
                        {
                            foreach (var record in recordList.Data)
                            {

                                db.ExecuteNoneQuery("Delete from Record where Id=" + record.Id, System.Data.CommandType.Text);
                                //Record resimler silinecek
                                DeleteImageFile(record.Image);

                                var recordImageList = db.Find<RecordImage>("Select * from RecordImage where RecordId=" + record.Id, System.Data.CommandType.Text);
                                if (recordImageList.Data != null)
                                {
                                    foreach (var recordImage in recordImageList.Data)
                                    {
                                        db.ExecuteNoneQuery("delete from RecordImage where Id=" + recordImage.Id, System.Data.CommandType.Text);
                                        //RecordImage resimler silinecek
                                        DeleteImageFile(recordImage.ImageName);
                                    }
                                }

                                var recordFileList = db.Find<RecordFile>("Select * from RecordFile where RecordId=" + record.Id, System.Data.CommandType.Text);
                                if (recordFileList.Data != null)
                                {
                                    foreach (var recordFile in recordFileList.Data)
                                    {
                                        db.ExecuteNoneQuery("delete from RecordFile where Id=" + recordFile.Id, System.Data.CommandType.Text);
                                        //RecordFile dosyalar silinecek
                                        DeleteFile(recordFile.FileName);
                                    }
                                }
                            }
                        }

                        returnObject.ErrorMessage = "Kayıt başarıyla silindi";
                        returnObject.ResultType = MessageType.Success;

                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {

                }

            }


            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Delete from Record where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update Record set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update Record set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteImage(string id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });
            var record = db.Get<Record>("select * from Record where Id="+Convert.ToInt32(id), System.Data.CommandType.Text);
            if (record.Data != null)
            {
                db.ExecuteNoneQuery("Update Record set Image='' where Id=@Id", System.Data.CommandType.Text, parameters);
                DeleteImageFile(record.Data.Image);

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

            if (id == null || id == 0)
                return PartialView("Add", new Record { IsActive = true, TargetId = 1 });
            else
            {
                var model = db.Get<Record>("select * from [Record] where Id=@Id", System.Data.CommandType.Text, parameters);

                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]

        [ValidateInput(false)]//Ckeditor data alınamadığı için eklendi.
        public ActionResult Save(Record request, HttpPostedFileBase ImageFile)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

            if (request.CategoryId == 0)
                ModelState.AddModelError("CategoryId", "Lütfen kategori seçiniz.");
            else if (request.Id == 0)
            {
                var category = db.Get("Select * from Category where Category.Id=" + Convert.ToInt32(request.CategoryId), System.Data.CommandType.Text);
                if (!Convert.ToBoolean(category.Data["CanContentBeAdded"]))
                {
                    ModelState.AddModelError("CategoryId", "Seçtiğiniz kategoriye içerik eklenemez. Lütfen başka bir kategori seçiniz.");
                }
            }

            if (ModelState.IsValid)
            {
                ReturnObject<Banner> result = new ReturnObject<Banner>();

                if (string.IsNullOrEmpty(request.RecordUrl))
                    request.RecordUrl = StringManager.TextUrlCharSeoReplace(!String.IsNullOrEmpty(request.Name) ? request.Name : "");

                if (string.IsNullOrEmpty(request.Title))
                    request.Title = request.Name;

                if (string.IsNullOrEmpty(request.MetaDescription))
                    request.MetaDescription = request.Name;

                if (string.IsNullOrEmpty(request.MetaKeywords))
                    request.MetaKeywords = request.Name;


                if (ImageFile != null)
                {
                    int imageSmallWidth = 100;
                    int imageBigWidth = 500;

                    string filepath = "";

                    var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
                    if (generalSettings.Data != null)
                    {
                        imageSmallWidth = generalSettings.Data.ContentImageSmallWidth;
                        imageBigWidth = generalSettings.Data.ContentImageBigWidth;
                        filepath = generalSettings.Data.ImageUploadPath;
                    }

                    request.Image = ImageManager.ImageUploadDoubleCopy(ImageFile, filepath, imageSmallWidth, imageBigWidth);
                }

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = AuthenticationData.LanguageId });
                parameters.Add(new SqlParameter { ParameterName = "BannerTypeId", Value = request.BannerTypeId });
                parameters.Add(new SqlParameter { ParameterName = "TargetId", Value = request.TargetId });
                parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = request.CategoryId });

                if (!string.IsNullOrEmpty(request.Name))
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.RecordUrl))
                    parameters.Add(new SqlParameter { ParameterName = "RecordUrl", Value = request.RecordUrl });
                else
                    parameters.Add(new SqlParameter { ParameterName = "RecordUrl", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Title))
                    parameters.Add(new SqlParameter { ParameterName = "Title", Value = request.Title });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Title", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.MetaDescription))
                    parameters.Add(new SqlParameter { ParameterName = "MetaDescription", Value = request.MetaDescription });
                else
                    parameters.Add(new SqlParameter { ParameterName = "MetaDescription", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.MetaKeywords))
                    parameters.Add(new SqlParameter { ParameterName = "MetaKeywords", Value = request.MetaKeywords });
                else
                    parameters.Add(new SqlParameter { ParameterName = "MetaKeywords", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Url))
                    parameters.Add(new SqlParameter { ParameterName = "Url", Value = request.Url });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Url", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.ShortContent))
                    parameters.Add(new SqlParameter { ParameterName = "ShortContent", Value = request.ShortContent });
                else
                    parameters.Add(new SqlParameter { ParameterName = "ShortContent", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Link))
                    parameters.Add(new SqlParameter { ParameterName = "Link", Value = request.Link });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Link", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.ShortExplanation))
                    parameters.Add(new SqlParameter { ParameterName = "ShortExplanation", Value = request.ShortExplanation });
                else
                    parameters.Add(new SqlParameter { ParameterName = "ShortExplanation", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Explanation))
                    parameters.Add(new SqlParameter { ParameterName = "Explanation", Value = request.Explanation });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Explanation", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Image))
                    parameters.Add(new SqlParameter { ParameterName = "Image", Value = request.Image });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Image", Value = DBNull.Value });

                parameters.Add(new SqlParameter { ParameterName = "OrderNumber", Value = request.OrderNumber });

                parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

                if (request.IsActive)
                    parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
                else
                    parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });

                parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = AuthenticationData.UserId });



                if (request.Id == 0)
                {
                    db.ExecuteNoneQuery(@"
                insert into Record(
                SystemUserId,
                LanguageId,
                TargetId,
                CategoryId,
                Name,
                RecordUrl,
                Title,
                MetaDescription,
                MetaKeywords,
                Url,
                ShortContent,
                Link,
                ShortExplanation,
                Explanation,
                Image,
                OrderNumber,
                RecordDate,
                IsActive)
                values(
                @SystemUserId,
                @LanguageId,
                @TargetId,
                @CategoryId,
                @Name,
                @RecordUrl,
                @Title,
                @MetaDescription,
                @MetaKeywords,
                @Url,
                @ShortContent,
                @Link,
                @ShortExplanation,
                @Explanation,
                @Image,
                @OrderNumber,
                @RecordDate,
                @IsActive)
                ", System.Data.CommandType.Text, parameters);

                    string RedirectUrl = ImageFile != null
                        ? $"/Admin/ImageCrop/Index?ImageName={request.Image}&Dimension=b_&width={500}&height={100}&SaveUrl=/Admin/Record/Index"
                        : "/Admin/Record/Index";



                    return RedirectToAction("Success", "Message", new { area = "Admin", RedirectUrl = RedirectUrl, Message = "Kayıt ekleme işlemi başarıyla tamamlandı" });

                }
                else
                {

                    if (ImageFile != null)
                    {
                        var recordImage = db.Get("Select * from Record where Id=" + Convert.ToInt32(request.Id), System.Data.CommandType.Text);

                        if (recordImage.Data != null && recordImage.Data["Image"] != null)
                            DeleteImageFile(recordImage.Data["Image"].ToString());
                    }

                    parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                    string query = @"Update [Record] set 
                LanguageId=@LanguageId,
                CategoryId=@CategoryId,
                TargetId=@TargetId,
                Name=@Name,
                RecordUrl=@RecordUrl,
                Title=@Title,
                MetaDescription=@MetaDescription,
                MetaKeywords=@MetaKeywords,
                Url=@Url,
                ShortContent=@ShortContent,
                Link=@Link,
                ShortExplanation=@ShortExplanation,
                Explanation=@Explanation,
                ";
                    if (!string.IsNullOrEmpty(request.Image))
                        query += @"Image=@Image,";

                    query += @"
                OrderNumber=@OrderNumber,
                RecordDate=@RecordDate,
                IsActive=@IsActive
                where Id=@Id";

                    db.ExecuteNoneQuery(query, System.Data.CommandType.Text, parameters);

                    string RedirectUrl = ImageFile != null ? $"/Admin/ImageCrop/Index?ImageName={request.Image}&Dimension=b_&width={500}&height={100}&SaveUrl=/Record/Index" : "/Admin/Record/Index";
                    

                    return RedirectToAction("Success", "Message", new { area = "Admin", RedirectUrl = RedirectUrl, Message = "Kayıt ekleme güncelleme başarıyla tamamlandı" });

                }
            }
            else
            {
                return View("~/Areas/Admin/Views/Record/Add.cshtml", request);
            }
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

        private void DeleteFile(string File)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

            var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
            if (System.IO.File.Exists(generalSettings.Data.FileUploadPath + "\\" + File))
                System.IO.File.Delete(generalSettings.Data.FileUploadPath + "\\" + File);

        }
    }
}