using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inta.Framework.Web.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Lütfen kullanıcı adını giriniz.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen dil seçimi yapınız.")]
        public string LanguageId { get; set; }
        public bool? CreatePersistentCookie { get; set; }
        public string ReturnUrl { get; set; }
    }
}