using Inta.Framework.Admin.Models;
using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
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
    public class SystemRoleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(PagingDataListRequest<SystemRole> request)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            if (string.IsNullOrEmpty(request.Search.Name))
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
            else
                Parameters.Add(new SqlParameter { ParameterName = "Name", Value = request.Search.Name.ToString() });

            Parameters.Add(new SqlParameter { ParameterName = "IsActive", Value = request.Search.IsActive });

            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string sqlQuery = "Select * from SystemRole where (@Name is null or Name like '%'+@Name+'%') and IsActive=@IsActive order by " + request.OrderColumn + (request.OrderType == PagingDataListOrderType.Ascending ? " asc" : " desc");
            var data = db.Find<SystemRole>(sqlQuery, System.Data.CommandType.Text, Parameters);
            int count = data?.Data?.ToList()?.Count ?? 0;

            var pagingData = data.Data.Skip((Convert.ToInt32(request.ActivePageNumber) - 1) * request.PageRowCount).Take(request.PageRowCount).ToList();

            var selectData = pagingData.Select(s => new
            {
                Select = "<input type='checkbox' name='" + s.Id + "' />",
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive ? "Aktif" : "Pasif",
                Edit = "<a href='javascript:void(0)' onclick=\"$PagingDataList.AddRecordModal('/SystemRole/Add','True'," + s.Id.ToString() + ")\">Düzenle</a>",
                Delete = "<a href='javascript:void(0)' onclick=\"$PagingDataList.DeleteRecordModal('PagingDataList','/SystemRole/Delete',SearchDataList," + s.Id.ToString() + ")\">Sil</a>"
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
                var result = db.ExecuteNoneQuery("Delete from SystemRole where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Active(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update SystemRole set IsActive=1 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passive(string ids)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            foreach (var item in ids.Split(',').ToList())
            {
                var result = db.ExecuteNoneQuery("Update SystemRole set IsActive=0 where id=" + item, System.Data.CommandType.Text);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(int? id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });


            var actionList = db.Find<SystemAction>("Select * from SystemAction", System.Data.CommandType.Text).Data ?? new List<SystemAction>();
            ViewBag.actionList = actionList;

            var menuList = db.Find<SystemMenu>("Select * from SystemMenu", System.Data.CommandType.Text).Data ?? new List<SystemMenu>();
            ViewBag.menuList = menuList;

            if (id.HasValue)
            {
                List<SqlParameter> actionRoleParameters = new List<SqlParameter>();
                actionRoleParameters.Add(new SqlParameter { ParameterName = "SystemRoleId", Value = id });

                List<int> ActionRoles = new List<int>();
                var ActionRolesTable = db.Find("select SystemActionId from SystemActionRole where SystemRoleId=@SystemRoleId", System.Data.CommandType.Text, actionRoleParameters).Data;
                for (int i = 0; i < ActionRolesTable.Rows.Count; i++)
                    ActionRoles.Add(Convert.ToInt32(ActionRolesTable.Rows[i]["SystemActionId"]));
                ViewBag.ActionRole = ActionRoles;

                List<SqlParameter> menuRoleParameters = new List<SqlParameter>();
                menuRoleParameters.Add(new SqlParameter { ParameterName = "SystemRoleId", Value = id });

                List<int> MenuRoles = new List<int>();
                var MenuRolesTable = db.Find("select SystemMenuId from SystemMenuRole where SystemRoleId=@SystemRoleId", System.Data.CommandType.Text, menuRoleParameters).Data;
                for (int i = 0; i < MenuRolesTable.Rows.Count; i++)
                    MenuRoles.Add(Convert.ToInt32(MenuRolesTable.Rows[i]["SystemMenuId"]));
                ViewBag.MenuRole = MenuRoles;
            }


            if (id == 0)
                return PartialView("Add", new SystemRole { IsActive = true });
            else
            {
                var model = db.Get<SystemRole>("select * from SystemRole where Id=@Id", System.Data.CommandType.Text, parameters);

                return PartialView("Add", model.Data);
            }


        }

        [HttpPost]
        public ActionResult Save(SystemRole request, List<string> actions, List<string> menuList)
        {
            AuthenticationData authenticationData = new AuthenticationData();
            ReturnObject<SystemRole> result = new ReturnObject<SystemRole>();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

            List<SqlParameter> SystemRoleParameter = new List<SqlParameter>();
            if (string.IsNullOrEmpty(request.Name))
                SystemRoleParameter.Add(new SqlParameter { ParameterName = "Name", Value = DBNull.Value });
            else
                SystemRoleParameter.Add(new SqlParameter { ParameterName = "Name", Value = request.Name });

            if (string.IsNullOrEmpty(request.Explanation))
                SystemRoleParameter.Add(new SqlParameter { ParameterName = "Explanation", Value = DBNull.Value });
            else
                SystemRoleParameter.Add(new SqlParameter { ParameterName = "Explanation", Value = request.Explanation });

            SystemRoleParameter.Add(new SqlParameter { ParameterName = "RecordDate", Value = DateTime.Now });
            SystemRoleParameter.Add(new SqlParameter { ParameterName = "IsActive", Value = request.IsActive });

            if (request.Id == 0)
            {
                object roleId = db.ExecuteScalar(@"
                    insert into SystemRole(Name,Explanation,RecordDate,IsActive)
                    values(@Name,@Explanation,@RecordDate,@IsActive);select RoleId = SCOPE_IDENTITY();
                    ", System.Data.CommandType.Text, SystemRoleParameter).Data;

                foreach (var item in actions)
                {

                    List<SqlParameter> actionRoleParameter = new List<SqlParameter>();
                    actionRoleParameter.Add(new SqlParameter { ParameterName = "SystemActionId", Value = item });
                    actionRoleParameter.Add(new SqlParameter { ParameterName = "SystemRoleId", Value = roleId });

                    db.ExecuteNoneQuery(@"
                    insert into SystemActionRole(SystemActionId,SystemRoleId)
                    values(@SystemActionId,@SystemRoleId)
                    ", System.Data.CommandType.Text, actionRoleParameter);
                }


                foreach (var item in menuList)
                {
                    List<SqlParameter> menuRoleParameter = new List<SqlParameter>();
                    menuRoleParameter.Add(new SqlParameter { ParameterName = "SystemMenuId", Value = item });
                    menuRoleParameter.Add(new SqlParameter { ParameterName = "SystemRoleId", Value = roleId });

                    db.ExecuteNoneQuery(@"
                    insert into SystemMenuRole(SystemMenuId,SystemRoleId)
                    values(@SystemMenuId,@SystemRoleId)
                    ", System.Data.CommandType.Text, menuRoleParameter);
                }


                return Json(new ReturnObject<SystemRole>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
            else
            {
                SystemRoleParameter.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                db.ExecuteNoneQuery(@"
                Update SystemRole set
                Name=@Name,
                Explanation=@Explanation,
                RecordDate=@RecordDate,
                IsActive=@IsActive
                where Id=@Id
                ", System.Data.CommandType.Text, SystemRoleParameter);


                /*Action listesi güncellenir*/
                var savedActionList = db.Find<SystemActionRole>(@"
                Select * from SystemActionRole where SystemRoleId=" + request.Id
                , System.Data.CommandType.Text);
                foreach (var item in savedActionList.Data)
                {
                    if (actions == null || !actions.Any(a => a == item.SystemActionId.ToString()))
                        db.ExecuteNoneQuery(@"Delete SystemActionRole where Id=" + item.Id, System.Data.CommandType.Text);
                }

                if (actions != null)
                {
                    foreach (var item in actions)
                    {
                        var actionRole = db.Find<SystemActionRole>(@"
                        Select * from SystemActionRole where SystemRoleId=" + request.Id + " SystemActionId=" + item, System.Data.CommandType.Text);
                        if (actionRole.Data == null)
                        {
                            db.ExecuteNoneQuery("insert into SystemActionRole(SystemActionId,SystemRoleId) values(" + item + "," + request.Id + ")", System.Data.CommandType.Text);
                        }
                    }
                }
                /*Action listesi güncellenir*/

                /*Menu listesi güncellenir*/
                var savedMenuList = db.Find<SystemMenuRole>(@"
                Select * from SystemMenuRole where SystemRoleId=" + request.Id, System.Data.CommandType.Text);
                foreach (var item in savedMenuList.Data)
                {
                    if (menuList == null || !menuList.Any(a => a == item.SystemMenuId.ToString()))
                        db.ExecuteNoneQuery("Delete SystemMenuRole where Id=" + item.Id, System.Data.CommandType.Text);
                }

                if (menuList != null)
                {
                    foreach (var item in menuList)
                    {
                        var menuRole = db.Find<SystemMenuRole>("Select * from SystemMenuRole where SystemRoleId=" + request.Id + " SystemMenuId=" + item, System.Data.CommandType.Text);
                        if (menuRole.Data == null)
                        {
                            db.ExecuteNoneQuery("insert into SystemMenuRole(SystemMenuId,SystemRoleId) values(" + item + "," + request.Id + ")", System.Data.CommandType.Text);
                        }
                    }
                }
                /*Menu listesi güncellenir*/

                return Json(new ReturnObject<SystemRole>
                {
                    Data = request,
                    ResultType = MessageType.Success
                });
            }
        }
    }
}