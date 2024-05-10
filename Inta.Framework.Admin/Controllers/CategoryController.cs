﻿using Inta.Framework.Admin.Models;
using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
using Inta.Framework.Extension;
using Inta.Framework.Web.Base;
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

            
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from Category where (@Name is null or Name like '%'+@Name+'%') and IsActive=@IsActive order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
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
                return PartialView("Add", new Category { IsActive = true });
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
           


            parameters.Add(new SqlParameter { ParameterName = "OrderNumber", Value = request.OrderNumber });
            parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

            if (request.IsActive)
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
            else
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });


            if (request.Id == 0)
            {
                db.ExecuteNoneQuery(@"
                insert into Record(Name,
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
                values(@Name,
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

                return Json(new ReturnObject<Category>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
            else
            {
                parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                db.ExecuteNoneQuery(@"Update [Record] set 
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
                Image=@Image,
                OrderNumber=@OrderNumber,
                RecordDate=@RecordDate,
                IsActive=@IsActive
                where Id=@Id", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<Category>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
        }
    }
}