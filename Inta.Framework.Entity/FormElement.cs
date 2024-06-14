using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
    [Table("FormElement")]
    public class FormElement
    {
        public FormElement()
        {
        }

        public int Id { get; set; }
        public int SystemUserId { get; set; }
        public int LanguageId { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public int FormGroupId { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public int ElementTypeId { get; set; }
        public bool AllowNulls { get; set; }
        public int OrderNumber { get; set; }
        public DateTime RecordDate { get; set; }
        public bool IsActive { get; set; }
    }
}
