using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inta.Framework.Web.Model
{
    public class CategoryModel
    {
        public string CategoryFullUrl { get; set; }
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryFullRouting { get; set; }
        public string Title { get; set; }
        public string MetaDecription { get; set; }
        public string MetaKeywords { get; set; }
        public string ShortExplanation { get; set; }
        public string Image { get; set; }
        public string ImageTag { get; set; }
        public string ImageTitle { get; set; }
        public string Explanation { get; set; }
        public string OrderNumber { get; set; }
        public string IsActive { get; set; }
        public string CategoryLink { get; set; }
        public List<CategoryModel> Items { get; set; }
    }
}