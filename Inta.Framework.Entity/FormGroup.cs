using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
    [Table("FormGroup")]
    public class FormGroup
    {
        public FormGroup()
        {
        }

        public int Id { get; set; }
        public int SystemUserId { get; set; }
        public int LanguageId { get; set; }

        [Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
        public string Explanation { get; set; }
        public int OrderNumber { get; set; }
        public DateTime RecordDate { get; set; }
        public bool IsActive { get; set; }
    }
}
