using Inta.Framework.Ado.Net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Models
{
    public class AuthorizationCheck : ActionFilterAttribute, IResultFilter
    {
        public AuthorizationCheck()
        {

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = context?.Controller as Controller;
            AuthenticationData _authenticationData = new AuthenticationData();
            DBLayer dbLayer = new DBLayer(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            if (_authenticationData?.HasSession ?? false)
            {
                var user = dbLayer.Get("Select * from SystemUser where UserName='" + _authenticationData.UserName + "' and Password='" + _authenticationData.Password + "' and IsActive=1", System.Data.CommandType.Text);
                if (user?.Data != null)
                {
                    if (controller != null)
                    {
                        controller.ViewBag.ActiveTopMenuId = "";
                        controller.ViewBag.UserName = user.Data["Name"] + " " + user.Data["SurName"];
                        controller.ViewBag.SystemUserId = user.Data["Id"];
                    }

                    var userRole = dbLayer.Get("Select * from SystemRole where Id=" + user.Data["SystemRoleId"].ToString(), System.Data.CommandType.Text);
                    if (controller != null && userRole?.Data != null)
                        controller.ViewBag.RoleName = userRole.Data["Name"];


                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter { ParameterName = "RoleId", Value = user.Data["SystemRoleId"] });
                    parameters.Add(new SqlParameter { ParameterName = "ControllerName", Value = context.RouteData.Values["controller"] });
                    parameters.Add(new SqlParameter { ParameterName = "ActionName", Value = context.RouteData.Values["action"] });


                    var activeRoleAction = dbLayer.Get(@"select 
                        SystemAction.ActionName,
                        SystemAction.ControllerName,
                        SystemAction.Description,
                        SystemAction.SystemMenuId
                        from SystemRole
                        inner join SystemActionRole on SystemRole.Id = SystemActionRole.SystemRoleId
                        inner join SystemAction on SystemActionRole.SystemActionId = SystemAction.Id
                        where
                        ControllerName=@ControllerName and ActionName=@ActionName and
                        SystemRole.Id=@RoleId", System.Data.CommandType.Text,
                        parameters);

                    //Yapılan istek ajax isteği değilse yetkilendirme kontrolü yapılır.
                    if (!Convert.ToBoolean(user.Data["IsAdmin"]) && context != null && context.HttpContext.Request.Headers["x-requested-with"] != "XMLHttpRequest" && activeRoleAction.Data == null)
                    {
                        context.Result = new RedirectResult("/NoAuthorization");
                        return;
                    }
                    else if (!Convert.ToBoolean(user.Data["IsAdmin"]) && context != null && context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest" && activeRoleAction.Data == null)
                    {
                        return;
                    }
                }
            }
            else
            {
                if (context != null)
                    context.Result = new RedirectResult("/Login?ReturnUrl=" + context.HttpContext.Request.Path + context.HttpContext.Request.QueryString);
            }

            if (context != null)
                base.OnActionExecuting(context);
        }
    }
}