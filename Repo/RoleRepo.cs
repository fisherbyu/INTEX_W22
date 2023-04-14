using System;
using BYU_EGYPT_INTEX.Areas.Identity.Data;
using BYU_EGYPT_INTEX.Core.Repo;
using Microsoft.AspNetCore.Identity;

namespace BYU_EGYPT_INTEX.Repo
{
	public class RoleRepo : IRoleRepo
    { 
        private readonly ApplicationDbContext _context;

		public RoleRepo(ApplicationDbContext context)
		{
            _context = context;
		}

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}

