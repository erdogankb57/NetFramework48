using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inta.Framework.Entity
{
    [Table("SEOIndex")]
    public class SEOIndex
    {
        public SEOIndex()
        {
        }


        public int Id { get; set; }
        public int SystemUserId { get; set; }
        public int LanguageId { get; set; }

        [Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
        public string RedirectUrl { get; set; }
        public DateTime RecordDate { get; set; }
        public bool IsActive { get; set; }
    }
}