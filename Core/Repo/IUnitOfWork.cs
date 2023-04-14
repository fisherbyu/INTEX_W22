using System;
namespace BYU_EGYPT_INTEX.Core.Repo
{
	public interface IUnitOfWork
	{
		IUserRepo User { get; }

		IRoleRepo Role { get; }
	}
}

