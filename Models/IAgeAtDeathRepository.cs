using System;
namespace BYU_EGYPT_INTEX.Models
{
    public interface IAgeAtDeathRepository
    {
        IQueryable<Masterfilter> masterfilters { get; }
    }
}

