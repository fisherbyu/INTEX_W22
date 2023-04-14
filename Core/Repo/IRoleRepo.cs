using System;
using BYU_EGYPT_INTEX.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace BYU_EGYPT_INTEX.Core.Repo
{
	public interface IRoleRepo
	{
		ICollection<IdentityRole> GetRoles();
	}
}

