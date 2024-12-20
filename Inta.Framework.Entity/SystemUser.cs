﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public int SystemRoleId { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string SurName { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")] 
		public string UserName { get; set; }


		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Bu alan boş geçilemez.")]
		public string Address { get; set; }
		public string Image { get; set; }

		public DateTime RecordDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsAdmin { get; set; }
	}
}
