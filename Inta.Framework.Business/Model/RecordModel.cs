using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Business.Model
{
    public class RecordModel
    {
        public string Name { get; set; }
        public string RecordUrl { get; set; }
        public string Title { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string Url { get; set; }
        public string ShortContent { get; set; }
        public string Link { get; set; }
        public string TargetType { get; set; }
        public string ShortExplanation { get; set; }
        public string Explanation { get; set; }
        public string Image { get; set; }
        public int OrderNumber { get; set; }
        public bool IsActive { get; set; }

    }
}
