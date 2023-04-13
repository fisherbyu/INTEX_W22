using System;
using static System.Reflection.Metadata.BlobBuilder;

namespace BYU_EGYPT_INTEX.Models.ViewModels
{
	public class BurialmainsViewModel
	{
		public PageInfo? PageInfo { get; set; }
		public List<Masterfilter>? ListBurials { get; set; }
        public IQueryable<Masterfilter>? masterfilters { get; set; }
    }
}

