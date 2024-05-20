using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
	[Table("FormElementOptions")]
	public class FormElementOptions
	{
		public FormElementOptions()
		{
		}

		public int Id { get; set; }
		public int SystemUserId { get; set; }
		public int LanguageId { get; set; }
		public int FormElementId { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public bool IsSelected { get; set; }
		public int OrderNumber { get; set; }
		public DateTime RecordDate { get; set; }
		public bool IsActive { get; set; }
	}
}
