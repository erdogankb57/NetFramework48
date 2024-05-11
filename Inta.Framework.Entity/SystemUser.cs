using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
	[Table("SystemUser")]
	public class SystemUser
	{
		public SystemUser()
		{
		}

		public int Id { get; set; }
		public int SystemUserId { get; set; }
		public int SystemRoleId { get; set; }
		public string Name { get; set; }
		public string SurName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public DateTime RecordDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsAdmin { get; set; }
	}
}
