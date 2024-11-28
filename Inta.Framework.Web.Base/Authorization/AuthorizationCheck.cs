using Inta.Framework.Ado.Net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Base.Authorization
{
    public class AuthorizationCheck : ActionFilterAttribute, IResultFilter
    {
        public AuthorizationCheck()
        {

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        
        {
            Controller controller = context?.Controller as Controller;
            DBLayer dbLayer = new DBLayer(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            if (AuthenticationData.HasSession)
            {
                List<SqlParameter> userParameters = new List<SqlParameter>();
                userParameters.Add(new SqlParameter { ParameterName = "UserName", Value = AuthenticationData.UserName });
                userParameters.Add(new SqlParameter { ParameterName = "Password", Value = AuthenticationData.Password });

                var user = dbLayer.Get("Select * from SystemUser where UserName=@UserName and Password=@Password and IsActive=1", System.Data.CommandType.Text, userParameters);
                if (user?.Data != null)
                {
                    if (controller != null)
                    {
                        controller.ViewBag.ActiveTopMenuId = "";
                        controller.ViewBag.UserName = user.Data["Name"] + " " + user.Data["SurName"];
                        controller.ViewBag.SystemUserId = user.Data["Id"];
                    }
                    List<SqlParameter> userRoleParameters = new List<SqlParameter>();
                    userRoleParameters.Add(new SqlParameter { ParameterName = "Id", Value = user.Data["SystemRoleId"].ToString() });
                    var userRole = dbLayer.Get("Select * from SystemRole where Id=@Id", System.Data.CommandType.Text, userRoleParameters);
                    if (controller != null && userRole?.Data != null)
                        controller.ViewBag.RoleName = userRole.Data["Name"];

                    if (!string.IsNullOrEmpty(AuthenticationData.LanguageId.ToString()))
                        controller.ViewBag.LanguageId = AuthenticationData.LanguageId.ToString();

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
                    if (controller.RouteData.Values["controller"].ToString() != "Account" && !Convert.ToBoolean(user.Data["IsAdmin"]) && context != null && context.HttpContext.Request.Headers["x-requested-with"] != "XMLHttpRequest" && activeRoleAction.Data == null)
                    {
                        context.Result = new RedirectResult("/Admin/NoAuthorization");
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
                    context.Result = new RedirectResult("/Admin/Login?ReturnUrl=" + context.HttpContext.Request.Path);
            }

            if (context != null)
                base.OnActionExecuting(context);
        }
    }
}
