using System;
namespace BYU_EGYPT_INTEX.Models
{
    public class EfAgeAtDeathRepository : IAgeAtDeathRepository 
    {
        private egyptbyuContext context { get; set; }

        public EfAgeAtDeathRepository(egyptbyuContext temp)
        {
            context = temp;
        }

        public IQueryable<Masterfilter> masterfilters => context.Masterfilters;
    }
}

