using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inta.Framework.Web.Models
{
    public class BannerSearch
    {
        public string Name { get; set; }
        public int IsActive { get; set; }
    }

    public class BannerSearchModel
    {
        public int Id { get; set; }
        public string BannerTypeName { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string ShortExplanation { get; set; }
        public int OrderNumber { get; set; }
        public int TargetId { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
    }
}