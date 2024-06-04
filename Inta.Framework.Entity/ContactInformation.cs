using Inta.Framework.Ado.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inta.Framework.Entity
{
	[Table("ContactInformation")]
	public class ContactInformation
	{
		public ContactInformation()
		{
		}


		public int Id { get; set; }
		public int SystemUserId { get; set; }
		public int LanguageId { get; set; }
		[Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
		public string Phone { get; set; }
		public string Gsm { get; set; }
		public string Fax { get; set; }
		[Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
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
		public DateTime RecordDate { get; set; }
		public bool IsActive { get; set; }
	}
}

