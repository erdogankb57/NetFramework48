using System;
using System.Collections.Generic;
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
        public int SystemMenuId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Description { get; set; }
    }
}
