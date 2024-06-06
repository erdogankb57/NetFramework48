using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
using Inta.Framework.Web.Base.Authorization;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Controllers
{
    public class AccountController : Controller
    {
        [AuthorizationCheck]
        public ActionResult Index()
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            AuthenticationData authenticationData = new AuthenticationData();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "UserName", Value = authenticationData.UserName });
            var user = db.Get<SystemUser>("Select * from SystemUser where UserName=@UserName", System.Data.CommandType.Text, parameters);

            return View(user.Data);
        }

        public ActionResult Save(SystemUser user)
        {
            ReturnObject<SystemUser> result = new ReturnObject<SystemUser>();

            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            AuthenticationData authenticationData = new AuthenticationData();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Name", Value = user.Name });
            parameters.Add(new SqlParameter { ParameterName = "SurName", Value = user.SurName });
            parameters.Add(new SqlParameter { ParameterName = "UserName", Value = user.UserName });
            parameters.Add(new SqlParameter { ParameterName = "Password", Value = user.Password });
            parameters.Add(new SqlParameter { ParameterName = "Email", Value = user.Email });
            parameters.Add(new SqlParameter { ParameterName = "Phone", Value = user.Phone });
            parameters.Add(new SqlParameter { ParameterName = "Address", Value = user.Address });
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = user.Id });


            db.ExecuteNoneQuery(@"
            Update SystemUser set
            Name=@Name,
            SurName=@SurName,    
            UserName=@UserName,
            Password=@Password,
            Email=@Email,
            Phone=@Phone,
            Address=@Address
            where Id=@Id
            ", System.Data.CommandType.Text, parameters);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}