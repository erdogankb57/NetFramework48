using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inta.Framework.Web.Models
{
    public class ContactInformationModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gsm { get; set; }
        public string Fax { get; set; }
        public string Adress { get; set; }
        public string Explanation { get; set; }
        public string GoogleMapFrame { get; set; }
        public string GoogleMapLink { get; set; }
        public string GoogleMapX { get; set; }
        public string GoogleMapY { get; set; }
        public string Image { get; set; }

        public string QrCode { get; set; }
        public string FormSendEmail { get; set; }
        public int OrderNumber { get; set; }
        public bool IsActive { get; set; }




    }
}