using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inta.Framework.Web.Model
{
    public class RecordFileModel
    {
        public string Name { get; set; }
        public string ShortExplanation { get; set; }
        public string Explanation { get; set; }
        public string FileName { get; set; }
        public string FileTagName { get; set; }
        public string FileTitleName { get; set; }
        public int TargetId { get; set; }
        public bool HomePageStatus { get; set; }
        public int OrderNumber { get; set; }
        public bool IsActive { get; set; }


    }
}