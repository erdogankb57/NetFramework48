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
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<Category> request)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            if (string.IsNullOrEmpty(request.Search.Name))
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Search.Name.ToString() });

            if (request.Search.IsActive)
                Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
            else
                Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });

            if (request.Search.CategoryId == null)
                Parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = request.Search.CategoryId.ToString() });



            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from Category where (@CategoryId is null or CategoryId=@CategoryId) and (@Name is null or Name like '%'+@Name+'%') and IsActive=@IsActive order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<Category>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/Category/Add','True'," + s.Id.ToString() + ")\">Düzenle</a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('CategoryList','/Category/Delete',SearchDataList," + s.Id.ToString() + ")\">Sil</a>"
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
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

            ViewBag.ImageFolder = System.Configuration.ConfigurationManager.AppSettings["ImageUpload"].ToString();


            if (id == 0)
                return PartialView("Add", new Category { IsActive = true, CategoryId = 0 });
            else
            {
                var model = db.Get<Category>("select * from [Category] where Id=@Id", System.Data.CommandType.Text, parameters);
                
                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]

        [ValidateInput(false)]//Ckeditor data alınamadığı için eklendi.
        public ActionResult Save(Category request, HttpPostedFileBase ImageFile)
        {
            AuthenticationData authenticationData = new AuthenticationData();
            ReturnObject<Category> result = new ReturnObject<Category>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

            if (ImageFile != null)
            {
                int imageSmallWidth = 100;
                int imageBigWidth = 500;

                string filepath = ConfigurationManager.AppSettings["ImageUpload"].ToString();
                request.Image = ImageManager.ImageUploadDoubleCopy(ImageFile, filepath, imageSmallWidth, imageBigWidth);
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = authenticationData.LanguageId });
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

            parameters.Add(new SqlParameter { ParameterName = "OrderNumber", Value = request.OrderNumber });
            parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

            if (request.IsActive)
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
            else
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });


            if (request.Id == 0)
            {
                db.ExecuteNoneQuery(@"
                insert into Category(
                LanguageId,
                CategoryId,
                Code,
                Name,
                CategoryUrl,
                Title,
                MetaDecription,
                MetaKeywords,
                ShortExplanation,
                Image,
                ImageTag,
                ImageTitle,
                Explanation,
                OrderNumber
                )
                values(
                @LanguageId,
                @CategoryId,
                @Code,
                @Name,
                @CategoryUrl,
                @Title,
                @MetaDecription,
                @MetaKeywords,
                @ShortExplanation,
                @Image,
                @ImageTag,
                @ImageTitle,
                @Explanation,
                @OrderNumber
                )
                ", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<Category>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
            else
            {
                parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                db.ExecuteNoneQuery(@"Update [Category] set 
                LanguageId=@LanguageId,
                CategoryId=@CategoryId,
                Code=@Code,
                Name=@Name,
                CategoryUrl=@CategoryUrl,
                Title=@Title,
                MetaDecription=@MetaDecription,
                MetaKeywords=@MetaKeywords,
                ShortExplanation=@ShortExplanation,
                Image=@Image,
                ImageTag=@ImageTag,
                ImageTitle=@ImageTitle,
                Explanation=@Explanation,
                OrderNumber=@OrderNumber
                where Id=@Id", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<Category>
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
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });
            var result = db.ExecuteNoneQuery("Update Category set Image='' where Id=@Id", System.Data.CommandType.Text, parameters);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}