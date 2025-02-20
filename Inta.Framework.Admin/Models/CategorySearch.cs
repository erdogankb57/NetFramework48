using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inta.Framework.Web.Models
{
    public class CategorySearch
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int IsActive { get; set; }
    }

    public class CategoryPageType
    {
        public string CategoryFullUrl { get; set; }
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string CategoryName { get; set; }
    }
}