using System;
using BYU_EGYPT_INTEX.Areas.Identity.Data;
using BYU_EGYPT_INTEX.Core.Repo;

namespace BYU_EGYPT_INTEX.Repo
{
	public class UserRepo : IUserRepo
	{
		private readonly ApplicationDbContext _context;

		public UserRepo(ApplicationDbContext context)
		{
			_context = context;
		}

        public ApplicationUser GetUser(string id)
        {
			return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ICollection<ApplicationUser> GetUsers()
		{
			return _context.Users.ToList();
		}

        public ApplicationUser RemoveUser(ApplicationUser user)
        {
            _context.Remove(user);
            _context.SaveChanges();

            return user;
        }

        public ApplicationUser UpdateUser(ApplicationUser user)
        {
            _context.Update(user);
			_context.SaveChanges();

			return user;
        }
    }
}

