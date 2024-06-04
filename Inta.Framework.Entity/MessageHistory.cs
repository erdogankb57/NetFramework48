using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inta.Framework.Entity
{
    public class MessageHistory 
    {
        public MessageHistory()
        {
        }

        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int MessageTypeId { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }

        [Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
        public string Subject { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
        public string Explanation { get; set; }
        public bool IsActive { get; set; }
        public bool IsRead { get; set; }
        public DateTime ArchiveDate { get; set; }
        public string IpNumber { get; set; }
        public DateTime RecordDate { get; set; }
    }
}