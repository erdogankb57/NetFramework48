using Inta.Framework.Admin.Models;
using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
using Inta.Framework.Admin.Base.Authorization;
using Inta.Framework.Admin.Base.FormControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Areas.Admin.Controllers
{
    [AuthorizationCheck]
    public class BannerTypeController : Controller
    {
        // GET: BannerType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<BannerTypeSearch> request)
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
            string sqlQuery = "Select * from BannerType where (@Name is null or Name like '%'+@Name+'%') and LanguageId=@LanguageId and  (@IsActive is null or IsActive=@IsActive) order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<BannerType>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/Admin/BannerType/Add','True'," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/edit-icon.png' width='20'/></a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('/Admin/BannerTypeList','/Admin/BannerType/Delete',SearchDataList," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/delete-icon.png' width='20'/></a>"
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
                var bannerList = db.Find<Banner>("Select * from Banner where BannerTypeId=" + Convert.ToInt32(item), System.Data.CommandType.Text);

                if (bannerList.Data != null)
                {
                    foreach (var banner in bannerList.Data)
                    {
                        if (!string.IsNullOrEmpty(banner.Image))
                        {
                            if (System.IO.File.Exists(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + "k_" + banner.Image))
                                System.IO.File.Delete(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + "k_" + banner.Image);

                            if (System.IO.File.Exists(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + "b_" + banner.Image))
                                System.IO.File.Delete(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + "b_" + banner.Image);

                            if (System.IO.File.Exists(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + banner.Image))
                                System.IO.File.Delete(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + banner.Image);

                        }
                        //Banner sil
                        db.ExecuteNoneQuery("Delete Banner where Id=" + Convert.ToInt32(banner.Id), System.Data.CommandType.Text);
                    }
                }
                var result = db.ExecuteNoneQuery("Delete from BannerType where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update BannerType set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update BannerType set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

            var model = db.Get<BannerType>("select * from [BannerType] where Id=@Id", System.Data.CommandType.Text, parameters);
            return PartialView("Add", model.Data);
        }

        [HttpPost]
        public ActionResult Save(BannerType request)
        {
            if (ModelState.IsValid)
            {
                ReturnObject<BannerType> result = new ReturnObject<BannerType>();
                DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (string.IsNullOrEmpty(request.Name))
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });

                parameters.Add(new SqlParameter { ParameterName = "SmallImageWidth", Value = request.SmallImageWidth });
                parameters.Add(new SqlParameter { ParameterName = "BigImageWidth", Value = request.BigImageWidth });
                parameters.Add(new SqlParameter { ParameterName = "SmallImageHeight", Value = request.SmallImageHeight });
                parameters.Add(new SqlParameter { ParameterName = "BigImageHeight", Value = request.BigImageHeight });


                if (request.IsActive)
                    parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 1 });
                else
                    parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = 0 });

                if (string.IsNullOrEmpty(request.Description))
                    parameters.Add(new SqlParameter { ParameterName = "Description", Value = DBNull.Value });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Description", Value = request.Description });

                parameters.Add(new SqlParameter { ParameterName = "LanguageId", Value = AuthenticationData.LanguageId });
                parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = AuthenticationData.UserId });



                if (request.Id == 0)
                {
                    db.ExecuteNoneQuery("insert into [BannerType](SystemUserId,LanguageId,Name,SmallImageWidth,BigImageWidth,SmallImageHeight,BigImageHeight,Description,IsActive) values(@SystemUserId,@LanguageId,@Name,@SmallImageWidth,@BigImageWidth,@SmallImageHeight,@BigImageHeight,@Description,@IsActive)", System.Data.CommandType.Text, parameters);

                    return Json(new ReturnObject<BannerType>
                    {
                        Data = request,
                        ResultType = MessageType.Success
                    });
                }
                else
                {


                    parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });


                    db.ExecuteNoneQuery("Update [BannerType] set LanguageId=@LanguageId,Name=@Name,SmallImageWidth=@SmallImageWidth,BigImageWidth=@BigImageWidth,SmallImageHeight=@SmallImageHeight,BigImageHeight=@BigImageHeight,IsActive=@IsActive,Description=@Description where Id=@Id", System.Data.CommandType.Text, parameters);

                    return Json(new ReturnObject<BannerType>
                    {
                        Data = request,
                        ResultType = MessageType.Success
                    });
                }
            }
            else
            {
                return Json(new ReturnObject<BannerType>
                {
                    Data = request,
                    ResultType = MessageType.Error,
                    Validation = ModelState.ToList().Where(v => v.Value.Errors.Any()).Select(s => new { Key = s.Key, Error = s.Value.Errors })
                });
            }
        }
    }
}