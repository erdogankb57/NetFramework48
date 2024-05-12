using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
    [Table("SystemActionRole")]
    public class SystemActionRole
    {
        public SystemActionRole()
        {

        }

        public int Id { get; set; }
        public int SystemActionId { get; set; }
        public int SystemRoleId { get; set; }
    }
}
