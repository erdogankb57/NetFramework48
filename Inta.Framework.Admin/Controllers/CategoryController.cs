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

namespace Inta.Framework.Admin.Controllers
{

    [AuthorizationCheck]
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            return View("TreeIndex");
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
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Delete from Category where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
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
                return PartialView("Add", new Category { IsActive = true, CategoryId = 0 });
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
            if (ModelState.IsValid)
            {
                ReturnObject<Category> result = new ReturnObject<Category>();
                DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

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
                IsActive
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
                @IsActive
                ); SELECT SCOPE_IDENTITY()
                ");
                    var inserted = db.ExecuteScalar(shtml.ToString(), System.Data.CommandType.Text, parameters);

                    var category = db.Get<Category>("Select * from Category where Id=" + inserted.Data, System.Data.CommandType.Text);


                    return Json(new ReturnObject<Category>
                    {
                        Data = request,
                        ResultType = MessageType.Success,
                        RedirectUrl = ImageFile != null ? $"/ImageCrop/Index?ImageName={category.Data.Image}&Dimension=b_&width={500}&height={100}&SaveUrl=/Category/Index?Message=Kayıt ekleme işlemi başarıyla tamamlandı" : "/Category/Index?Message=Kayıt ekleme işlemi başarıyla tamamlandı"

                    });
                }
                else
                {
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
                OrderNumber=@OrderNumber
                where Id=@Id");
                    db.ExecuteNoneQuery(shtml.ToString(), System.Data.CommandType.Text, parameters);

                    var category = db.Get<Category>("Select * from Category where Id=" + Convert.ToInt32(request.Id), System.Data.CommandType.Text);

                    return Json(new ReturnObject<Category>
                    {
                        Data = request,
                        ResultType = MessageType.Success,
                        RedirectUrl = ImageFile != null ? $"/ImageCrop/Index?ImageName={category.Data.Image}&Dimension=b_&width={500}&height={100}&SaveUrl=/Category/Index?Message=Kayıt güncelleme işlemi başarıyla tamamlandı" : "/Category/Index?Message=Kayıt güncelleme işlemi başarıyla tamamlandı"

                    });
                }
            }
            else
            {
                return Json(new ReturnObject<Category>
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

        [HttpPost]
        public ActionResult DeleteImage(string id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });
            var result = db.ExecuteNoneQuery("Update Category set Image='' where Id=@Id", System.Data.CommandType.Text, parameters);

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
    }
}