using System;
using BYU_EGYPT_INTEX.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BYU_EGYPT_INTEX.Models.ViewModels
{
	public class DeleteUserViewModel
	{
        public ApplicationUser User { get; set; }

        public IList<SelectListItem> Roles { get; set; }
    }
}

