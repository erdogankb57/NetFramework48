using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
	[Table("StaticText")]
	public class StaticText
	{
		public StaticText()
		{
		}


		public int Id { get; set; }
		public int SystemUserId { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string Explanation { get; set; }
		public int OrderNumber { get; set; }
		public DateTime RecordDate { get; set; }
		public bool IsActive { get; set; }
	}
}
