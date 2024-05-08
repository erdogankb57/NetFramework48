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

		[Column("Id")]
		public int Id { get; set; }
		[Column("SystemUserId")]
		public int SystemUserId { get; set; }
		[Column("LanguageId")]
		public int LanguageId { get; set; }
		[Column("Name")]
		public string Name { get; set; }
		[Column("Description")]
		public string Description { get; set; }
		[Column("SmallImageWidth")]
		public int SmallImageWidth { get; set; }
		[Column("BigImageWidth")]
		public int BigImageWidth { get; set; }
		[Column("RecordDate")]
		public DateTime? RecordDate { get; set; }
		[Column("IsActive")]
		public bool IsActive { get; set; }
	}
}