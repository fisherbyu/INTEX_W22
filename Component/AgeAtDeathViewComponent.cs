using System;
using BYU_EGYPT_INTEX.Models;
using Microsoft.AspNetCore.Mvc;
using static BYU_EGYPT_INTEX.Component.AgeAtDeathViewComponent;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.EntityFrameworkCore;

namespace BYU_EGYPT_INTEX.Component
{
    public class AgeAtDeathViewComponent : ViewComponent
    {
        private IAgeAtDeathRepository repo { get; set; }

        public AgeAtDeathViewComponent(IAgeAtDeathRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["ageatdeath"];

            var AgeAtDeath = repo.masterfilters
                .Select(x => x.Ageatdeath)
                .Distinct()
                .OrderBy(x => x);

            return View(AgeAtDeath);
        }
    }
}

