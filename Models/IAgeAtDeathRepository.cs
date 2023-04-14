using System;
namespace BYU_EGYPT_INTEX.Models
{
    public interface IFilterRepository
    {
        IQueryable<Masterfilter> masterfilters { get; }
    }
}

