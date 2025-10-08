using Inta.Framework.Ado.Net;
using Inta.Framework.Web.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Inta.Framework.Web.Manager
{
    public class ContactInformationManager
    {

        public List<ContactInformationModel> Find()
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            List<ContactInformationModel> list = new List<ContactInformationModel>();
            var data = db.Find("Select * from ContactInformation", System.Data.CommandType.Text);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new ContactInformationModel
                {
                    Name = data.Data.Rows[i]["Name"].ToString(),
                    Email = data.Data.Rows[i]["Email"].ToString(),
                    Phone = data.Data.Rows[i]["Phone"].ToString(),
                    Gsm = data.Data.Rows[i]["Gsm"].ToString(),
                    Fax = data.Data.Rows[i]["Fax"].ToString(),
                    Adress = data.Data.Rows[i]["Adress"].ToString(),
                    Explanation = data.Data.Rows[i]["Explanation"].ToString(),
                    GoogleMapFrame = data.Data.Rows[i]["GoogleMapFrame"].ToString(),
                    GoogleMapLink = data.Data.Rows[i]["GoogleMapLink"].ToString(),
                    GoogleMapX = data.Data.Rows[i]["GoogleMapX"].ToString(),
                    GoogleMapY = data.Data.Rows[i]["GoogleMapY"].ToString(),
                    Image = data.Data.Rows[i]["Image"].ToString(),
                    QrCode = data.Data.Rows[i]["QrCode"].ToString(),
                    FormSendEmail = data.Data.Rows[i]["FormSendEmail"].ToString(),
                    OrderNumber = Convert.ToInt32(data.Data.Rows[i]["OrderNumber"]),
                    IsActive = Convert.ToBoolean(data.Data.Rows[i]["IsActive"])
                });
            }

            return list;
        }

        public List<ContactInformationModel> Get(int Id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "ContactInformationId", Value = Id });


            List<ContactInformationModel> list = new List<ContactInformationModel>();
            var data = db.Find("select * from ContactInformation where Id=@ContactInformationId", System.Data.CommandType.Text, parameters);
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                list.Add(new ContactInformationModel
                {
                    Name = data.Data.Rows[i]["Name"].ToString(),
                    Email = data.Data.Rows[i]["Email"].ToString(),
                    Phone = data.Data.Rows[i]["Phone"].ToString(),
                    Gsm = data.Data.Rows[i]["Gsm"].ToString(),
                    Fax = data.Data.Rows[i]["Fax"].ToString(),
                    Adress = data.Data.Rows[i]["Adress"].ToString(),
                    Explanation = data.Data.Rows[i]["Explanation"].ToString(),
                    GoogleMapFrame = data.Data.Rows[i]["GoogleMapFrame"].ToString(),
                    GoogleMapLink = data.Data.Rows[i]["GoogleMapLink"].ToString(),
                    GoogleMapX = data.Data.Rows[i]["GoogleMapX"].ToString(),
                    GoogleMapY = data.Data.Rows[i]["GoogleMapY"].ToString(),
                    Image = data.Data.Rows[i]["Image"].ToString(),
                    QrCode = data.Data.Rows[i]["QrCode"].ToString(),
                    FormSendEmail = data.Data.Rows[i]["FormSendEmail"].ToString(),
                    OrderNumber = Convert.ToInt32(data.Data.Rows[i]["OrderNumber"]),
                    IsActive = Convert.ToBoolean(data.Data.Rows[i]["IsActive"])
                });
            }

            return list;
        }
    }
}