﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
    [Table("SystemAction")]
    public class SystemAction
    {
        public SystemAction()
        {

        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Bu alanı doldurmanız gerekmektedir.")]
        public int SystemMenuId { get; set; }
        [Required(ErrorMessage = "Bu alanı doldurmanız gerekmektedir.")]
        public string ControllerName { get; set; }
        [Required(ErrorMessage = "Bu alanı doldurmanız gerekmektedir.")]
        public string ActionName { get; set; }
        [Required(ErrorMessage = "Bu alanı doldurmanız gerekmektedir.")]
        public string Description { get; set; }
    }
}
