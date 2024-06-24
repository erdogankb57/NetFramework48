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

namespace Inta.Framework.Admin.Controllers
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
                RecordImage = "<a href='/RecordImage/Index/" + s.Id + "'><img src='/Content/images/photo-icon.png' width='20'/></a>",
                RecordFile = "<a href='/RecordFile/Index/" + s.Id + "'><img src='/Content/images/file-icon.png' width='20'/></a>",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/Record/Add','False'," + s.Id.ToString() + ",AddCallBack)\"><img src='/Content/images/edit-icon.png' width='20'/></a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('RecordList','/Record/Delete',SearchDataList," + s.Id.ToString() + ")\"><img src='/Content/images/delete-icon.png' width='20'/></a>"
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
            var result = db.ExecuteNoneQuery("Update Record set Image='' where Id=@Id", System.Data.CommandType.Text, parameters);

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
            else
            {
                var category = db.Get("Select * from Category inner join PageType on Category.PageTypeId=PageType.Id where Category.Id=" + request.CategoryId, System.Data.CommandType.Text);
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

                    return Json(new ReturnObject<Record>
                    {
                        Data = request,
                        RedirectUrl = ImageFile != null
                        ? $"/ImageCrop/Index?ImageName={request.Image}&Dimension=b_&width={500}&height={100}&SaveUrl=/Record/Index?Message=Kayıt ekleme işlemi başarıyla tamamlandı"
                        : "/Record/Index?Message=Kayıt ekleme işlemi başarıyla tamamlandı",

                        ResultType = MessageType.Success
                    });
                }
                else
                {
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

                    return Json(new ReturnObject<Record>
                    {
                        Data = request,
                        ResultType = MessageType.Success,
                        RedirectUrl = ImageFile != null ? $"/ImageCrop/Index?ImageName={request.Image}&Dimension=b_&width={500}&height={100}&SaveUrl=/Record/Index?Message=Kayıt ekleme güncelleme başarıyla tamamlandı" : "/Record/Index?Message=Kayıt güncelleme işlemi başarıyla tamamlandı"
                    });
                }
            }
            else
            {
                return Json(new ReturnObject<Record>
                {
                    Data = request,
                    ResultType = MessageType.Error,
                    Validation = ModelState.ToList().Where(v => v.Value.Errors.Any()).Select(s => new
                    {
                        Key = s.Key,
                        Error = s.Value.Errors
                    })
                });
            }
        }
    }
}