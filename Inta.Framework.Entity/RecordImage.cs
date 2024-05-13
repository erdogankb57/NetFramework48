using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
    [Table("RecordImage")]
    public class RecordImage
    {
        public RecordImage()
        {
        }
        public int Id { get; set; }
        public int SystemUserId { get; set; }
        public int LanguageId { get; set; }
        public int RecordId { get; set; }
        public string Name { get; set; }
        public string ShortExplanation { get; set; }
        public string Explanation { get; set; }
        public string ImageName { get; set; }
        public string ImageTagName { get; set; }
        public string ImageTitleName { get; set; }
        public int TargetId { get; set; }
        public bool HomePageStatus { get; set; }
        public int OrderNumber { get; set; }
        public DateTime RecordDate { get; set; }
        public bool IsActive { get; set; }
    }
}

