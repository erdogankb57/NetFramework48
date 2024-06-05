using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inta.Framework.Admin.Models
{
    public class SystemUserSearch
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }
}