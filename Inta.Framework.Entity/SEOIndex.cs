using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
        public string Url { get; set; }
        public string RedirectUrl { get; set; }
        public DateTime RecordDate { get; set; }
        public bool IsActive { get; set; }
    }
}