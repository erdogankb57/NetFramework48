using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
    public class Record
    {
        public Record()
        {
        }

        public int Id { get; set; }
        public int SystemUserId { get; set; }
        public int LanguageId { get; set; }
        public int BannerTypeId { get; set; }
        public int CategoryId { get; set; }

        [Range(1, 100, ErrorMessage = "Lütfen açılış tipini seçiniz")]
        public int TargetId { get; set; }

        [Required(ErrorMessage = "Bu alanı doldurmanız gerekmektedir.")]
        public string Name { get; set; }
        public string RecordUrl { get; set; }
        public string Title { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string Url { get; set; }
        public string ShortContent { get; set; }
        public string Link { get; set; }
        public int TargetType { get; set; }
        public string ShortExplanation { get; set; }
        public string Explanation { get; set; }
        public string Image { get; set; }
        public int OrderNumber { get; set; }
        public DateTime RecordDate { get; set; }
        public bool IsActive { get; set; }
    }

}
