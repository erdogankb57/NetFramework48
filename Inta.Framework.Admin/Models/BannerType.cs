using Inta.Framework.Ado.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inta.Framework.Admin.Models
{
	[Table("BannerType")]
	public class BannerType
	{
		public BannerType()
		{
		}

		[DatabaseColumn(Name = "Id")]
		public int Id { get; set; }
		[DatabaseColumn(Name = "SystemUserId")]
		public int SystemUserId { get; set; }
		[DatabaseColumn(Name = "LanguageId")]
		public int LanguageId { get; set; }
		[DatabaseColumn(Name = "Name")]
		public string Name { get; set; }
		[DatabaseColumn(Name = "Description")]
		public string Description { get; set; }
		[DatabaseColumn(Name = "SmallImageWidth")]
		public int SmallImageWidth { get; set; }
		[DatabaseColumn(Name = "BigImageWidth")]
		public int BigImageWidth { get; set; }
		[DatabaseColumn(Name = "RecordDate")]
		public DateTime? RecordDate { get; set; }
		[DatabaseColumn(Name = "IsActive")]
		public bool IsActive { get; set; }
	}
}