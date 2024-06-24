using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
	[Table("PageType")]
	public class PageType
	{
		public PageType()
		{
		}

		public int Id { get; set; }
		public int SystemUserId { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string Code { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string ControllerName { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string ActionName { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string ViewName { get; set; }
		public bool IsExplanationEnabled { get; set; }
		public bool IsShortExplanationEnabled { get; set; }
		public bool IsMenuFirstRecord { get; set; }
		public bool IsMenuFirstCategory { get; set; }
		public bool IsPagingEnabled { get; set; }
		public DateTime RecordDate { get; set; }
		public bool IsActive { get; set; }
	}
}
