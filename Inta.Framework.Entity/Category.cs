using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
        }
        public int Id { get; set; }
        public int SystemUserId { get; set; }
        public int LanguageId { get; set; }

        [Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
        public int PageTypeId { get; set; }
        public int CategoryId { get; set; }
        public string Code { get; set; }

        [Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
        public string Name { get; set; }
        public string Title { get; set; }
        public string CategoryUrl { get; set; }
        public string CategoryLink { get; set; }
        public string MetaDecription { get; set; }
        public string MetaKeywords { get; set; }
        public int FormGroupId { get; set; }
        public string ShortExplanation { get; set; }
        public bool CanBeDeleted { get; set; }
        public bool CanSubCategoryBeAdded { get; set; }
        public string Image { get; set; }
        public string ImageTag { get; set; }
        public string ImageTitle { get; set; }
        public int OrderNumber { get; set; }
        public DateTime RecordDate { get; set; }
        public bool IsActive { get; set; }
        public string Explanation { get; set; }
    }
}
