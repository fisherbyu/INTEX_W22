using System;
using Microsoft.AspNetCore.Identity;

namespace BYU_EGYPT_INTEX.Core.Repositories
{
	public interface IUserRepo
	{
		ICollection<IdentityUser> GetUsers();
	}
}

