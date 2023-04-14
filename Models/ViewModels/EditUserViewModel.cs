using System;
using BYU_EGYPT_INTEX.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BYU_EGYPT_INTEX.Models.ViewModels
{
	public class EditUserViewModel
	{
		public ApplicationUser User { get; set; }

		public IList<SelectListItem> Roles { get; set;}
	}
}

