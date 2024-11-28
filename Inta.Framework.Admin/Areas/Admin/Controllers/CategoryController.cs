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
using System.Transactions;

namespace Inta.Framework.Admin.Areas.Admin.Controllers
{

    [AuthorizationCheck]
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            return View("TreeIndex");
        }
        public ActionResult GetTreeList(CategorySearch request)
        {
            StringBuilder shtml = new StringBuilder();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> categoryParameters = new List<SqlParameter>();
            categoryParameters.Add(new SqlParameter { ParameterName = "Id", Value = request.CategoryId });

            if (request.IsActive == -1)
                categoryParameters.Add(new SqlParameter { ParameterName = "IsActive", Value = DBNull.Value });
            else
                categoryParameters.Add(new SqlParameter { ParameterName = "IsActive", Value = request.IsActive });

            categoryParameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = AuthenticationData.LanguageId });


            var category = db.Find("Select * from Category where CategoryId=@Id and LanguageId=@LanguageId and (@IsActive is null or IsActive=@IsActive)", System.Data.CommandType.Text, categoryParameters);

            shtml.Append("<ul>");
            for (int i = 0; i < category.Data.Rows.Count; i++)
            {
                shtml.Append("<li><a id='" + category.Data.Rows[i]["Id"] + "'>" + category.Data.Rows[i]["Name"] + "</a>");
                shtml.Append(GetSubCategory(Convert.ToInt32(category.Data.Rows[i]["Id"])));
                shtml.Append("</li>");
            }
            shtml.Append("</ul>");

            var result = new ReturnObject<string>
            {
                Data = shtml.ToString(),
                ResultType = MessageType.Success
            };

            return Json(result);
        }

        private static string GetSubCategory(int Id)
        {
            StringBuilder shtml = new StringBuilder();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> categoryParameters = new List<SqlParameter>();
            categoryParameters.Add(new SqlParameter { ParameterName = "Id", Value = Id });
            var category = db.Find("Select * from Category where CategoryId=@Id", System.Data.CommandType.Text, categoryParameters);
            shtml.Append("<ul>");
            for (int i = 0; i < category.Data.Rows.Count; i++)
            {
                shtml.Append("<li><a id='" + category.Data.Rows[i]["Id"] + "'>" + category.Data.Rows[i]["Name"] + "</a>");
                shtml.Append(GetSubCategory(Convert.ToInt32(category.Data.Rows[i]["Id"])));
                shtml.Append("</li>");
            }
            shtml.Append("</ul>");

            return shtml.ToString();
        }
        public ActionResult GetList(PagingDataListRequest<CategorySearch> request)
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

            if (request.Search.CategoryId == null)
                Parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = request.Search.CategoryId.ToString() });

            Parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = AuthenticationData.LanguageId });


            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from Category where (@CategoryId is null or CategoryId=@CategoryId) and LanguageId=@LanguageId and (@Name is null or Name like '%'+@Name+'%') and  (@IsActive is null or IsActive=@IsActive) order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<Category>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/Category/Add','False'," + s.Id.ToString() + ",AddCallBack)\"><img src='/Content/images/edit-icon.png' width='20'/></a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('CategoryList','/Category/Delete',SearchDataList," + s.Id.ToString() + ")\"><img src='/Content/images/delete-icon.png' width='20'/></a>"
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
            ReturnObject<Category> returnObject = new ReturnObject<Category>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    foreach (var item in ids.Split(',').ToList())
                    {
                        var category = db.Get<Category>("select * from Category where id=" + Convert.ToInt32(item), System.Data.CommandType.Text);
                        //Kategori silinebilme özelliği var mı
                        if (category.Data != null && category.Data.CanBeDeleted)
                        {
                            DeleteImageFile(category.Data.Image);

                            var recordList = db.Find<Record>("Select * from Record where CategoryId=" + category.Data.Id, System.Data.CommandType.Text);
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

                            db.ExecuteNoneQuery("Delete from Category where id=" + Convert.ToInt32(item), System.Data.CommandType.Text);
                            returnObject.ErrorMessage = "Kayıt başarıyla silindi";
                            returnObject.ResultType = MessageType.Success;
                        }
                        else
                        {
                            returnObject.ErrorMessage = "Bu kategori silinemez.Lütfen kategori silinebilme özelliğini düzenleyiniz.";
                            returnObject.ResultType = MessageType.Error;
                        }

                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {

                }

            }

            return Json(returnObject, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update Category set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update Category set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

            var settings = db.Get<GeneralSettings>("select top 1 * from GeneralSettings", System.Data.CommandType.Text);
            ViewBag.ImageFolder = settings.Data.ImageCdnUrl;

            if (id == 0 || id == null)
            {
                var category = new Category { IsActive = true, CategoryId = 0, CanSubCategoryBeAdded = true, CanContentBeAdded = true, CanBeDeleted = true };
                if (!string.IsNullOrEmpty(Request["MainCategoryId"]))
                    category.CategoryId = Convert.ToInt32(Request["MainCategoryId"]);

                return PartialView("Add", category);
            }
            else
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

                var model = db.Get<Category>("select * from [Category] where Id=@Id", System.Data.CommandType.Text, parameters);

                var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
                if (generalSettings.Data != null)
                {
                    ViewBag.ImageSmallWidth = generalSettings.Data.CategoryImageSmallWidth;
                    ViewBag.ImageBigWidth = generalSettings.Data.CategoryImageBigWidth;
                }

                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]

        [ValidateInput(false)]//Ckeditor data alınamadığı için eklendi.
        public ActionResult Save(Category request, HttpPostedFileBase ImageFile)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());


            if (request.Id == 0 && request.CategoryId != 0)
            {
                var category = db.Get("Select * from Category where Id=" + request.CategoryId, System.Data.CommandType.Text);
                if (!Convert.ToBoolean(category.Data["CanSubCategoryBeAdded"]))
                {
                    ModelState.AddModelError("CategoryId", "Seçtiğiniz kategoriye alt kategori eklenemez. Lütfen başka bir kategori seçiniz.");
                }
            }
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(request.CategoryUrl))
                    request.CategoryUrl = StringManager.TextUrlCharSeoReplace(!String.IsNullOrEmpty(request.Name) ? request.Name : "");

                if (string.IsNullOrEmpty(request.Title))
                    request.Title = request.Name;

                if (string.IsNullOrEmpty(request.MetaDecription))
                    request.MetaDecription = request.Name;

                if (string.IsNullOrEmpty(request.MetaKeywords))
                    request.MetaKeywords = request.Name;

                ReturnObject<Category> result = new ReturnObject<Category>();

                if (ImageFile != null)
                {
                    int imageSmallWidth = 100;
                    int imageBigWidth = 500;
                    string filepath = "";

                    var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
                    if (generalSettings.Data != null)
                    {
                        imageSmallWidth = generalSettings.Data.CategoryImageSmallWidth;
                        imageBigWidth = generalSettings.Data.CategoryImageBigWidth;
                        filepath = generalSettings.Data.ImageUploadPath;
                    }

                    request.Image = ImageManager.ImageUploadDoubleCopy(ImageFile, filepath, imageSmallWidth, imageBigWidth);
                }

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = AuthenticationData.LanguageId });
                parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = request.CategoryId });


                if (!string.IsNullOrEmpty(request.Code))
                    parameters.Add(new SqlParameter { ParameterName = "Code", Value = request.Code });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Code", Value = DBNull.Value });


                if (!string.IsNullOrEmpty(request.Name))
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.CategoryUrl))
                    parameters.Add(new SqlParameter { ParameterName = "CategoryUrl", Value = request.CategoryUrl });
                else
                    parameters.Add(new SqlParameter { ParameterName = "CategoryUrl", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Title))
                    parameters.Add(new SqlParameter { ParameterName = "Title", Value = request.Title });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Title", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.MetaDecription))
                    parameters.Add(new SqlParameter { ParameterName = "MetaDecription", Value = request.MetaDecription });
                else
                    parameters.Add(new SqlParameter { ParameterName = "MetaDecription", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.MetaKeywords))
                    parameters.Add(new SqlParameter { ParameterName = "MetaKeywords", Value = request.MetaKeywords });
                else
                    parameters.Add(new SqlParameter { ParameterName = "MetaKeywords", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.ShortExplanation))
                    parameters.Add(new SqlParameter { ParameterName = "ShortExplanation", Value = request.ShortExplanation });
                else
                    parameters.Add(new SqlParameter { ParameterName = "ShortExplanation", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Image))
                    parameters.Add(new SqlParameter { ParameterName = "Image", Value = request.Image });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Image", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.ImageTag))
                    parameters.Add(new SqlParameter { ParameterName = "ImageTag", Value = request.ImageTag });
                else
                    parameters.Add(new SqlParameter { ParameterName = "ImageTag", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.ImageTitle))
                    parameters.Add(new SqlParameter { ParameterName = "ImageTitle", Value = request.ImageTitle });
                else
                    parameters.Add(new SqlParameter { ParameterName = "ImageTitle", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Explanation))
                    parameters.Add(new SqlParameter { ParameterName = "Explanation", Value = request.Explanation });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Explanation", Value = DBNull.Value });

                if (request.PageTypeId != null)
                    parameters.Add(new SqlParameter { ParameterName = "PageTypeId", Value = request.PageTypeId });
                else
                    parameters.Add(new SqlParameter { ParameterName = "PageTypeId", Value = DBNull.Value });

                parameters.Add(new SqlParameter { ParameterName = "OrderNumber", Value = request.OrderNumber });
                parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

                if (request.IsActive)
                    parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
                else
                    parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });

                if (request.CanSubCategoryBeAdded)
                    parameters.Add(new SqlParameter { ParameterName = "CanSubCategoryBeAdded", Value = 1 });
                else
                    parameters.Add(new SqlParameter { ParameterName = "CanSubCategoryBeAdded", Value = 0 });

                if (request.CanContentBeAdded)
                    parameters.Add(new SqlParameter { ParameterName = "CanContentBeAdded", Value = 1 });
                else
                    parameters.Add(new SqlParameter { ParameterName = "CanContentBeAdded", Value = 0 });


                if (request.CanBeDeleted)
                    parameters.Add(new SqlParameter { ParameterName = "CanBeDeleted", Value = 1 });
                else
                    parameters.Add(new SqlParameter { ParameterName = "CanBeDeleted", Value = 0 });

                parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = AuthenticationData.UserId });



                if (request.Id == 0)
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append(@"
                insert into Category(
                SystemUserId,
                LanguageId,
                PageTypeId,
                CategoryId,
                Code,
                Name,
                CategoryUrl,
                Title,
                MetaDecription,
                MetaKeywords,
                ShortExplanation,");

                    if (ImageFile != null)
                        shtml.Append("Image,");
                    shtml.Append(@"ImageTag,
                ImageTitle,
                Explanation,
                OrderNumber,
                IsActive,
                CanSubCategoryBeAdded,
                CanBeDeleted,
                CanContentBeAdded
                )
                values(
                @SystemUserId,
                @LanguageId,
                @PageTypeId,
                @CategoryId,
                @Code,
                @Name,
                @CategoryUrl,
                @Title,
                @MetaDecription,
                @MetaKeywords,
                @ShortExplanation,");
                    if (ImageFile != null)
                        shtml.Append("@Image,");
                    shtml.Append(@"@ImageTag,
                @ImageTitle,
                @Explanation,
                @OrderNumber,
                @IsActive,
                @CanSubCategoryBeAdded,
                @CanBeDeleted,
                @CanContentBeAdded
                ); SELECT SCOPE_IDENTITY()
                ");
                    var inserted = db.ExecuteScalar(shtml.ToString(), System.Data.CommandType.Text, parameters);

                    var category = db.Get<Category>("Select * from Category where Id=" + inserted.Data, System.Data.CommandType.Text);

                    string RedirectUrl = ImageFile != null ? $"/ImageCrop/Index?ImageName={category.Data.Image}&Dimension=b_&width={500}&height={100}&SaveUrl=/Category/Index" : "/Category/Index";

                    return RedirectToAction("Success", "Message", new MessageModel { RedirectUrl = RedirectUrl, Message = "Kayıt ekleme işlemi başarıyla tamamlandı" });


                }
                else
                {
                    if (ImageFile != null)
                    {
                        var categoryImage = db.Get("Select * from Category where Id=" + Convert.ToInt32(request.Id), System.Data.CommandType.Text);

                        if (categoryImage.Data != null && categoryImage.Data["Image"] != null)
                            DeleteImageFile(categoryImage.Data["Image"].ToString());
                    }

                    parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append(@"Update [Category] set 
                LanguageId=@LanguageId,
                PageTypeId=@PageTypeId,
                CategoryId=@CategoryId,
                Code=@Code,
                Name=@Name,
                CategoryUrl=@CategoryUrl,
                Title=@Title,
                MetaDecription=@MetaDecription,
                MetaKeywords=@MetaKeywords,
                ShortExplanation=@ShortExplanation,");
                    if (ImageFile != null)
                        shtml.Append("Image=@Image,");

                    shtml.Append(@"ImageTag=@ImageTag,
                ImageTitle=@ImageTitle,
                Explanation=@Explanation,
                OrderNumber=@OrderNumber,
                CanSubCategoryBeAdded=@CanSubCategoryBeAdded,
                CanBeDeleted=@CanBeDeleted,
                CanContentBeAdded=@CanContentBeAdded
                where Id=@Id");
                    db.ExecuteNoneQuery(shtml.ToString(), System.Data.CommandType.Text, parameters);

                    var category = db.Get<Category>("Select * from Category where Id=" + Convert.ToInt32(request.Id), System.Data.CommandType.Text);


                    string RedirectUrl = ImageFile != null ? $"/ImageCrop/Index?ImageName={category.Data.Image}&Dimension=b_&width={500}&height={100}&SaveUrl=/Category/Index" : "/Category/Index";



                    return RedirectToAction("Success", "Message", new MessageModel { RedirectUrl = RedirectUrl, Message = "Kayıt güncelleme işlemi başarıyla tamamlandı" });

                }
            }
            else
            {
                return View("Add", request);
            }
        }

        [HttpPost]
        public ActionResult DeleteImage(string id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });
            var category = db.Get<Category>("select * from Category where Id=@Id", System.Data.CommandType.Text);
            if (category.Data != null)
            {
                DeleteImageFile(category.Data.Image);
                db.ExecuteNoneQuery("Update Category set Image='' where Id=@Id", System.Data.CommandType.Text, parameters);
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetPageType(string id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });
            var result = db.Get<PageType>("select * from PageType where Id=@Id", System.Data.CommandType.Text, parameters);

            return Json(result, JsonRequestBehavior.AllowGet);
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