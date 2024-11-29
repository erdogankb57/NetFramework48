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
using System.Web;
using Inta.Framework.Extension;
using System.Text;

namespace Inta.Framework.Admin.Areas.Admin.Controllers
{
    [AuthorizationCheck]
    public class SystemUserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<SystemUserSearch> request)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            if (string.IsNullOrEmpty(request.Search.Name))
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Search.Name.ToString() });

            if (string.IsNullOrEmpty(request.Search.SurName))
                Parameters.Add(new SqlParameter { ParameterName = "SurName", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "SurName", Value = request.Search.SurName.ToString() });

            if (string.IsNullOrEmpty(request.Search.UserName))
                Parameters.Add(new SqlParameter { ParameterName = "UserName", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "UserName", Value = request.Search.UserName.ToString() });

            if (request.Search.IsActive == -1)
                Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = request.Search.IsActive });



            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from SystemUser where (@Name is null or Name like '%'+@Name+'%') and (@IsActive is null or IsActive=@IsActive) order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<SystemUser>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                SurName = s.SurName,
                UserName = s.UserName,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/Admin/SystemUser/Add','False'," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/edit-icon.png' width='20'/></a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('PagingDataList','/Admin/SystemUser/Delete',SearchDataList," + s.Id.ToString() + ")\"><img src='/Areas/Admin/Content/images/delete-icon.png' width='20'/></a>"
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
                var result = db.ExecuteNoneQuery("Delete from SystemUser where Id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update SystemUser set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update SystemUser set IsActive=0 where id=" + item, System.Data.CommandType.Text);
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

            if (id == 0)
                return PartialView("Add", new SystemUser { IsActive = true });
            else
            {
                var model = db.Get<SystemUser>("select * from SystemUser where Id=@Id", System.Data.CommandType.Text, parameters);

                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]
        public ActionResult Save(SystemUser request, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {

                ReturnObject<SystemUser> result = new ReturnObject<SystemUser>();
                DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());


                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "SystemUserId", Value = 0 });
                parameters.Add(new SqlParameter { ParameterName = "SystemRoleId", Value = request.SystemRoleId });

                string filepath = "";
                if (Image != null)
                {

                    var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
                    if (generalSettings.Data != null)
                        filepath = generalSettings.Data.ImageUploadPath;

                    request.Image = ImageManager.ImageUploadDoubleCopy(Image, filepath, 100, 500);
                }

                if (!string.IsNullOrEmpty(request.Name))
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Name))
                    parameters.Add(new SqlParameter { ParameterName = "SurName", Value = request.SurName });
                else
                    parameters.Add(new SqlParameter { ParameterName = "SurName", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.UserName))
                    parameters.Add(new SqlParameter { ParameterName = "UserName", Value = request.UserName });
                else
                    parameters.Add(new SqlParameter { ParameterName = "UserName", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Password))
                    parameters.Add(new SqlParameter { ParameterName = "Password", Value = request.Password });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Password", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Email))
                    parameters.Add(new SqlParameter { ParameterName = "Email", Value = request.Email });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Email", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Phone))
                    parameters.Add(new SqlParameter { ParameterName = "Phone", Value = request.Phone });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Phone", Value = DBNull.Value });

                if (!string.IsNullOrEmpty(request.Address))
                    parameters.Add(new SqlParameter { ParameterName = "Address", Value = request.Address });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Address", Value = DBNull.Value });

                parameters.Add(new SqlParameter { ParameterName = "IsAdmin", Value = request.IsAdmin });
                parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = request.IsActive });

                if (!string.IsNullOrEmpty(request.Image))
                    parameters.Add(new SqlParameter { ParameterName = "Image", Value = request.Image });
                else
                    parameters.Add(new SqlParameter { ParameterName = "Image", Value = DBNull.Value });


                if (request.Id == 0)
                {
                    parameters.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append(@"insert into [SystemUser](
                SystemRoleId,
                Name,
                SurName,
                UserName,
                Password,
                Email,
                Phone,
                Address,
                IsAdmin,
                IsActive
                ");
                    if (Image != null)
                        shtml.Append(@"Image");

                    shtml.Append(@"
                ) values(
                @SystemRoleId,
                @Name,
                @SurName,
                @UserName,
                @Password,
                @Email,
                @Phone,
                @Address,
                @IsAdmin,
                @IsActive
                ");
                    if (Image != null)
                    {
                        shtml.Append("@Image");
                    }
                    shtml.Append(")");

                    db.ExecuteNoneQuery(shtml.ToString(), System.Data.CommandType.Text, parameters);

                    string RedirectUrl = Image != null ? $"/Admin/ImageCrop/Index?ImageName={request.Image}&Dimension=b_&width={500}&height={100}&SaveUrl=/Admin/SystemUser/Index" : "/Admin/SystemUser/Index";

                    return RedirectToAction("Success", "Message", new { area = "Admin", RedirectUrl = RedirectUrl, Message = "Kayıt ekleme işlemi başarıyla tamamlandı" });

                }
                else
                {

                    if (Image != null)
                    {
                        var categoryImage = db.Get("Select * from SystemUser where Id=" + Convert.ToInt32(request.Id), System.Data.CommandType.Text);

                        if (categoryImage.Data != null && categoryImage.Data["Image"] != null)
                            DeleteImageFile(categoryImage.Data["Image"].ToString());
                    }

                    parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append(@"Update [SystemUser] set 
                SystemRoleId=@SystemRoleId,
                Name=@Name,
                SurName=@SurName,
                UserName=@UserName,
                Password=@Password,
                Email=@Email,
                Phone=@Phone,
                Address=@Address,
                IsAdmin=@IsAdmin,
                IsActive=@IsActive,");
                    if (Image != null)
                        shtml.Append("Image=@Image");
                    shtml.Append(" where Id=@Id");

                    db.ExecuteNoneQuery(shtml.ToString(), System.Data.CommandType.Text, parameters);

                    string RedirectUrl = Image != null ? $"/Admin/ImageCrop/Index?ImageName={request.Image}&Dimension=b_&width={500}&height={100}&SaveUrl=/Admin/SystemUser/Index" : "/Admin/SystemUser/Index";

                    return RedirectToAction("Success", "Message", new { area = "Admin", RedirectUrl = RedirectUrl, Message = "Kayıt güncelleme işlemi başarıyla tamamlandı" });

                }
            }

            else
            {
                return View("~/Areas/Admin/Views/SystemUser/Add.cshtml", request);
            }
        }

        [HttpPost]
        public ActionResult DeleteImage(string id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });

            var contact = db.Get<SystemUser>("select * from SystemUser where Id=" + Convert.ToInt32(id), System.Data.CommandType.Text);
            if (contact.Data != null)
            {
                if (!string.IsNullOrEmpty(contact.Data.Image))
                    DeleteImageFile(contact.Data.Image);

                var result = db.ExecuteNoneQuery("Update SystemUser set Image='' where Id=@Id", System.Data.CommandType.Text, parameters);
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
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

    }


}