using System;
namespace BYU_EGYPT_INTEX.Models.ViewModels
{
	public class BurialmainsViewModel
	{
		public IQueryable<Burialmain>? Burialmains { get; set; }
        public PageInfo? PageInfo { get; set; }
    }
}

