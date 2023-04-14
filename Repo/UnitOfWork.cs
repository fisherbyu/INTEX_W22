using System;
using BYU_EGYPT_INTEX.Core.Repo;

namespace BYU_EGYPT_INTEX.Repo
{
	public class UnitOfWork : IUnitOfWork
	{
        public IUserRepo User { get; }

        public IRoleRepo Role { get; }

        public UnitOfWork(IUserRepo user, IRoleRepo role)
        {
            User = user;
            Role = role;
        }
    }
}

