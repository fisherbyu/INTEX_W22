using System;
using BYU_EGYPT_INTEX.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BYU_EGYPT_INTEX.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		public IUserRepo User { get; }

		public UnitOfWork(IUserRepo user)
		{
			User = user;
		}
    }
}

