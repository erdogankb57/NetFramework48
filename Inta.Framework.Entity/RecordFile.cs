using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
	[Table("RecordFile")]
	public class RecordFile
	{
		public RecordFile()
		{
		}


		public int Id { get; set; }
		public int SystemUserId { get; set; }
		public int LanguageId { get; set; }
		public int RecordId { get; set; }
		public string Name { get; set; }
		public string ShortExplanation { get; set; }
		public string Explanation { get; set; }
		public string FileName { get; set; }
		public string FileTagName { get; set; }
		public string FileTitleName { get; set; }
		public int TargetId { get; set; }
		public bool HomePageStatus { get; set; }
		public int OrderNumber { get; set; }
		public DateTime RecordDate { get; set; }
		public bool IsActive { get; set; }
	}
}
