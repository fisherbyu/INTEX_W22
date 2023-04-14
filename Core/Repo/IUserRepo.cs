using System;
using BYU_EGYPT_INTEX.Areas.Identity.Data;

namespace BYU_EGYPT_INTEX.Core.Repo
{
	public interface IUserRepo
	{
		ICollection<ApplicationUser> GetUsers();

		ApplicationUser GetUser(string id);

		ApplicationUser UpdateUser(ApplicationUser user);

		ApplicationUser RemoveUser(ApplicationUser user);
	}
}

