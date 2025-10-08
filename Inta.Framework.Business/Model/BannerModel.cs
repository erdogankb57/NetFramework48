using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Business.Model
{
    public class BannerModel
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
