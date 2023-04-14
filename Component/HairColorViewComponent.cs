using System;
using BYU_EGYPT_INTEX.Models;
using Microsoft.AspNetCore.Mvc;

namespace BYU_EGYPT_INTEX.Component
{
    public class HairColorViewComponent : ViewComponent
    {
        private IFilterRepository repo { get; set; }

        public HairColorViewComponent(IFilterRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {

            var haircolor = repo.masterfilters
                .Select(x => x.Haircolor)
                .Distinct()
                .OrderBy(x => x);

            return View(haircolor);
        }
    }
}

