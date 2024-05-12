using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
    [Table("SystemMenuRole")]
    public class SystemMenuRole
    {
        public SystemMenuRole()
        {
        }

        public int Id { get; set; }
        public int SystemRoleId { get; set; }
        public int SystemMenuId { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
