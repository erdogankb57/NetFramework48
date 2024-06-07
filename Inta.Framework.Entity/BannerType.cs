using Inta.Framework.Ado.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inta.Framework.Entity
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
		[Required(ErrorMessage = "Bu alanı doldurmanız gerekmektedir.")]
		public string Name { get; set; }
		
		[DatabaseColumn(Name = "Description")]
		public string Description { get; set; }
		
		[DatabaseColumn(Name = "SmallImageWidth")]
		[Required(ErrorMessage = "Bu alanı doldurmanız gerekmektedir.")]
		[Range(100, 2000, ErrorMessage = "Lütfen 100 ile 2000 arasında bir değer giriniz.")]
		public int SmallImageWidth { get; set; }
		
		[DatabaseColumn(Name = "BigImageWidth")]
		[Required(ErrorMessage = "Bu alanı doldurmanız gerekmektedir.")]
		[Range(100, 2000, ErrorMessage = "Lütfen 100 ile 2000 arasında bir değer giriniz.")]
		public int BigImageWidth { get; set; }
		
		[DatabaseColumn(Name = "RecordDate")]
		public DateTime? RecordDate { get; set; }
		
		[DatabaseColumn(Name = "IsActive")]
		public bool IsActive { get; set; }
	}
}