using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inta.Framework.Admin.Models
{
    public class CategorySearch
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int IsActive { get; set; }
    }
}