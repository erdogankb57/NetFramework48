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
    public class ContactInformationController : Controller
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

            Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = request.Search.IsActive });


            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from ContactInformation where (@Name is null or Name like '%'+@Name+'%') and IsActive=@IsActive order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<ContactInformation>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/ContactInformation/Add','True'," + s.Id.ToString() + ")\">Düzenle</a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('ContactInformationList','/ContactInformation/Delete',SearchDataList," + s.Id.ToString() + ")\">Sil</a>"
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
                var result = db.ExecuteNoneQuery("Delete from ContactInformation where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update ContactInformation set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update ContactInformation set IsActive=0 where id=" + item, System.Data.CommandType.Text);
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
                return PartialView("Add", new ContactInformation { IsActive = true });
            else
            {
                var model = db.Get<ContactInformation>("select * from [ContactInformation] where Id=@Id", System.Data.CommandType.Text, parameters);

                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]
        public ActionResult Save(ContactInformation request, HttpPostedFileBase FileImage)
        {
            AuthenticationData authenticationData = new AuthenticationData();
            ReturnObject<ContactInformation> result = new ReturnObject<ContactInformation>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());


            if (FileImage != null)
            {

                string filepath = ConfigurationManager.AppSettings["ImageUpload"].ToString();

                request.Image = ImageManager.ImageUploadSingleCopy(FileImage, filepath);

            }

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = authenticationData.LanguageId });

            if (!string.IsNullOrEmpty(request.Name))
                parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });
            else
                parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Email))
                parameters.Add(new SqlParameter { ParameterName = "Email", Value = request.Email });
            else
                parameters.Add(new SqlParameter { ParameterName = "Email", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Phone))
                parameters.Add(new SqlParameter { ParameterName = "Phone", Value = request.Phone });
            else
                parameters.Add(new SqlParameter { ParameterName = "Phone", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Gsm))
                parameters.Add(new SqlParameter { ParameterName = "Gsm", Value = request.Gsm });
            else
                parameters.Add(new SqlParameter { ParameterName = "Gsm", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Fax))
                parameters.Add(new SqlParameter { ParameterName = "Fax", Value = request.Fax });
            else
                parameters.Add(new SqlParameter { ParameterName = "Fax", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Adress))
                parameters.Add(new SqlParameter { ParameterName = "Adress", Value = request.Adress });
            else
                parameters.Add(new SqlParameter { ParameterName = "Adress", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.Explanation))
                parameters.Add(new SqlParameter { ParameterName = "Explanation", Value = request.Explanation });
            else
                parameters.Add(new SqlParameter { ParameterName = "Explanation", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.GoogleMapFrame))
                parameters.Add(new SqlParameter { ParameterName = "GoogleMapFrame", Value = request.GoogleMapFrame });
            else
                parameters.Add(new SqlParameter { ParameterName = "GoogleMapFrame", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.GoogleMapLink))
                parameters.Add(new SqlParameter { ParameterName = "GoogleMapLink", Value = request.GoogleMapLink });
            else
                parameters.Add(new SqlParameter { ParameterName = "GoogleMapLink", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.GoogleMapX))
                parameters.Add(new SqlParameter { ParameterName = "GoogleMapX", Value = request.GoogleMapX });
            else
                parameters.Add(new SqlParameter { ParameterName = "GoogleMapX", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.GoogleMapY))
                parameters.Add(new SqlParameter { ParameterName = "GoogleMapY", Value = request.GoogleMapY });
            else
                parameters.Add(new SqlParameter { ParameterName = "GoogleMapY", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.QrCode))
                parameters.Add(new SqlParameter { ParameterName = "QrCode", Value = request.QrCode });
            else
                parameters.Add(new SqlParameter { ParameterName = "QrCode", Value = DBNull.Value });

            if (!string.IsNullOrEmpty(request.FormSendEmail))
                parameters.Add(new SqlParameter { ParameterName = "FormSendEmail", Value = request.FormSendEmail });
            else
                parameters.Add(new SqlParameter { ParameterName = "FormSendEmail", Value = DBNull.Value });

            parameters.Add(new SqlParameter { ParameterName = "OrderNumber", Value = request.OrderNumber });
            parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

            if (!string.IsNullOrEmpty(request.Image))
                parameters.Add(new SqlParameter { ParameterName = "Image", Value = request.Image });
            else
                parameters.Add(new SqlParameter { ParameterName = "Image", Value = DBNull.Value });

            if (request.IsActive)
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
            else
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });

            if (request.Id == 0)
            {
                db.ExecuteNoneQuery(@"insert into 
                [ContactInformation](LanguageId,
                Name,
                Email,
                Phone,
                Gsm,
                Fax,
                Adress,
                Explanation,
                GoogleMapFrame,
                GoogleMapLink,
                GoogleMapX,
                GoogleMapY,
                QrCode,
                FormSendEmail,
                OrderNumber,
                RecordDate,
                Image,
                IsActive) 
                values(@LanguageId,
                @Name,
                @Email,
                @Phone,
                @Gsm,
                @Fax,
                @Adress,
                @Explanation,
                @GoogleMapFrame,
                @GoogleMapLink,
                @GoogleMapX,
                @GoogleMapY,
                @QrCode,
                @FormSendEmail,
                @OrderNumber,
                @RecordDate,
                @Image,
                @IsActive)", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<ContactInformation>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
            else
            {
                parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                db.ExecuteNoneQuery(@"Update [ContactInformation] set 
                LanguageId=@LanguageId,
                Name=@Name,
                Email=@Email,
                Phone=@Phone,
                Gsm=@Gsm,
                Fax=@Fax,
                Adress=@Adress,
                Explanation=@Explanation,
                GoogleMapFrame=@GoogleMapFrame,
                GoogleMapLink=@GoogleMapLink,
                GoogleMapX=@GoogleMapX, 
                GoogleMapY=@GoogleMapY,
                QrCode=@QrCode,
                FormSendEmail=@FormSendEmail,
                OrderNumber=@OrderNumber,
                RecordDate=@RecordDate,
                IsActive=@IsActive
                where Id=@Id", System.Data.CommandType.Text, parameters);

                return Json(new ReturnObject<ContactInformation>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
        }
    }
}