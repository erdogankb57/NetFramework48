using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inta.Framework.Web.Models
{
    public class RecordImageModel
    {
        public string Name { get; set; }
        public string ShortExplanation { get; set; }
        public string Explanation { get; set; }
        public string ImageName { get; set; }
        public string ImageTagName { get; set; }
        public string ImageTitleName { get; set; }
        public int TargetId { get; set; }
        public bool HomePageStatus { get; set; }
        public int OrderNumber { get; set; }
        public bool IsActive { get; set; }
    }
}