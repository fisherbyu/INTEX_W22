using System;
using BYU_EGYPT_INTEX.Core.Repositories;
using BYU_EGYPT_INTEX.Data;
using Microsoft.AspNetCore.Identity;

namespace BYU_EGYPT_INTEX.Repositories
{
	public class UserRepo : IUserRepo
	{
		private readonly ApplicationDbContext _context;

		public UserRepo(ApplicationDbContext context)
		{
			_context = context;
		}

		public ICollection<IdentityUser> GetUsers()
		{
			return _context.Users.ToList();
		}
	}
}

