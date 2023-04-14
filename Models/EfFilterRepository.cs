using System;
namespace BYU_EGYPT_INTEX.Models
{
    public class EfFilterRepository : IFilterRepository 
    {
        private egyptbyuContext context { get; set; }

        public EfFilterRepository(egyptbyuContext temp)
        {
            context = temp;
        }

        public IQueryable<Masterfilter> masterfilters => context.Masterfilters;
    }
}

