using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BYU_EGYPT_INTEX.Models;
using System.Xml;
using BYU_EGYPT_INTEX.Models.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;
using BYU_EGYPT_INTEX.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using BYU_EGYPT_INTEX.Component;
using System.Drawing.Printing;
using System.Linq; 

namespace BYU_EGYPT_INTEX.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private egyptbyuContext DbContext { get; set; }
    private ApplicationDbContext AuthLinkContext { get; set; }
    private IAgeAtDeathRepository repo;

    public HomeController(ILogger<HomeController> logger, egyptbyuContext temp_context, ApplicationDbContext tempLink, IAgeAtDeathRepository temp)
    {
        //Log Errors
        _logger = logger;
        //Conect to Egypt DB
        DbContext = temp_context;
        //Connect Auth Users
        AuthLinkContext = tempLink;
        //get filters
        repo = temp;
    }

    public IActionResult Index()
    {
        return View();
        //Test Git again
    }

    [HttpGet]
    public IActionResult BurialData(int pageNum = 1)
    {
        int resultLength = 15;
        int pageNumber = pageNum;

        var data = new BurialmainsViewModel
        {
            masterfilters = repo.masterfilters
                .OrderBy(x => x.Burialid)
                .Skip((pageNum - 1) * resultLength)
                .Take(resultLength),

            PageInfo = new PageInfo
            {
                TotalNumBurials = repo.masterfilters.Count(),
                BurialsPerPage = resultLength,
                CurrentPage = pageNum
            }
        };
        return View(data);
     }

    [HttpPost]
    public IActionResult BurialData(string? burialid, string? depth, string? ageatdeath, string? sex, string? haircolor,
        int? estimatestature, string? headdirection, string? textilecolor, string? textilestructure, string? textilefunction,
        int pageNum = 1)
    {
        int resultLength = 15;
        int pageNumber = pageNum;
       
        var data = new BurialmainsViewModel
        {
            masterfilters = repo.masterfilters
                .Where(a => a.Ageatdeath == ageatdeath || ageatdeath == null)
                .Where(b => b.Burialid == burialid || burialid == null)
                .Where(c => c.Depth == depth || depth == null)
                .Where(d => d.Sex == sex || sex == null)
                .Where(e => e.Haircolor == haircolor || haircolor == null)
                .Where(f => f.Estimatestature == estimatestature || estimatestature == null)
                .Where(g => g.Headdirection == headdirection || headdirection == null)
                .Where(h => h.Color == textilecolor || textilecolor == null)
                .Where(i => i.TextileStructure == textilestructure || textilestructure == null)
                .Where(j => j.Textilefunction == textilefunction || textilefunction == null)
                .OrderBy(x => x.Burialid)
                .Skip((pageNum - 1) * resultLength)
                .Take(resultLength),

            PageInfo = new PageInfo
            {
                TotalNumBurials =
                (ageatdeath == null
                ? repo.masterfilters.Count()
                : repo.masterfilters.Where(data => data.Ageatdeath == ageatdeath).Count()),
                BurialsPerPage = resultLength,
                CurrentPage = pageNumber
            }
        };
        return View(data);

    }

    public IActionResult DisplayBurial(long ID)
    {
        Burialmain burialmain = DbContext.Burialmains.Single(x => x.Id == ID);
        //Burialmain burialmain = (from b in DbContext.Burialmains
        //                         where b.Id == ID
        //                         select b).SingleOrDefault() ?? new Burialmain();


        List<Models.Color> colors = (from c in DbContext.Colors
                                     join ct in DbContext.ColorTextiles on c.Id equals ct.MainColorid
                                     join t in DbContext.Textiles on ct.MainTextileid equals t.Id
                                     join bt in DbContext.BurialmainTextiles on t.Id equals bt.MainTextileid into bt_temp
                                     from bt in bt_temp.DefaultIfEmpty()
                                     join b in DbContext.Burialmains on bt.MainBurialmainid equals b.Id into b_temp
                                     from b in b_temp.DefaultIfEmpty()
                                     where b.Id == ID
                                     select c ?? new Models.Color()).Distinct().ToList();

        List<Textile> textiles = (from bt in DbContext.BurialmainTextiles
                                  join t_join in DbContext.Textiles on bt.MainTextileid equals t_join.Id into t_temp
                                  from t in t_temp.DefaultIfEmpty()
                                  where bt.MainBurialmainid == ID
                                  select t ?? new Textile()).ToList();
        BurialComposite data = new BurialComposite(burialmain, textiles, colors);
        return View(data);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

