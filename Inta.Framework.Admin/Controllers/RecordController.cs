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
    public class RecordController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<Banner> request)
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

            if (request.Search.BannerTypeId == null)
                Parameters.Add(new SqlParameter { ParameterName = "BannerTypeId", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "BannerTypeId", Value = request.Search.BannerTypeId });

            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from Record where (@Name is null or Name like '%'+@Name+'%') and IsActive=@IsActive and (@BannerTypeId='' or BannerTypeId=@BannerTypeId) order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<Record>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/Record/Add','True'," + s.Id.ToString() + ")\">Düzenle</a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('RecordList','/Record/Delete',SearchDataList," + s.Id.ToString() + ")\">Sil</a>"
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
        public ActionResult Add(int? id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

            ViewBag.ImageFolder = System.Configuration.ConfigurationManager.AppSettings["ImageUpload"].ToString();

            if (id == 0)
                return PartialView("Add", new Record());
            else
            {
                var model = db.Get<Record>("select * from [Record] where Id=@Id", System.Data.CommandType.Text, parameters);

                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]

        [ValidateInput(false)]//Ckeditor data alınamadığı için eklendi.
        public ActionResult Save(Record request, HttpPostedFileBase FileImage)
        {
            AuthenticationData authenticationData = new AuthenticationData();
            ReturnObject<Banner> result = new ReturnObject<Banner>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

            if (FileImage != null)
            {
                int imageSmallWidth = 100;
                int imageBigWidth = 500;

                string filepath = ConfigurationManager.AppSettings["ImageUpload"].ToString();

                var bannerType = db.Get("Select * from Record where Id=" + request.Id, System.Data.CommandType.Text);
                if (bannerType != null && bannerType.Data != null)
                {
                    imageSmallWidth = !string.IsNullOrEmpty(bannerType.Data["SmallImageWidth"].ToString()) ? Convert.ToInt32(bannerType.Data["SmallImageWidth"]) : 100;
                    imageBigWidth = !string.IsNullOrEmpty(bannerType.Data["BigImageWidth"].ToString()) ? Convert.ToInt32(bannerType.Data["BigImageWidth"]) : 500;
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


            if (request.Id == 0)
            {
                db.ExecuteNoneQuery("insert into [Record](LanguageId,BannerTypeId,Name,Link,TargetId,ShortExplanation,OrderNumber,Image,RecordDate,IsActive) values(@LanguageId,@BannerTypeId,@Name,@Link,@TargetId,@ShortExplanation,@OrderNumber,@Image,@RecordDate,@IsActive)", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<Record>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
            else
            {
                parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                db.ExecuteNoneQuery(@"Update [Record] set 
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

                return Json(new ReturnObject<Record>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
        }
    }
}