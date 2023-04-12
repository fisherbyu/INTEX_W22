using System;
namespace BYU_EGYPT_INTEX.Core.Repositories
{
	public interface IUnitOfWork
	{
		IUserRepo User { get; }

	}
}

