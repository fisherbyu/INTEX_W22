using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BYU_EGYPT_INTEX.Models;
using System.Xml;
using BYU_EGYPT_INTEX.Models.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using BYU_EGYPT_INTEX.Areas.Identity.Data;

namespace BYU_EGYPT_INTEX.Controllers;

public class HomeController : Controller
{
    //Test
    private readonly ILogger<HomeController> _logger;
    private egyptbyuContext DbContext { get; set; }
    private ApplicationDbContext AuthLinkContext { get; set; }
    
    public HomeController(ILogger<HomeController> logger, egyptbyuContext temp_context, ApplicationDbContext tempLink)
    {
        //Log Errors
        _logger = logger;
        //Conect to Egypt DB
        DbContext = temp_context;
        //Connect Auth Users
        AuthLinkContext = tempLink;
    }

    public IActionResult Index()
    {
        return View();
        //Test Git again
    }
    
    public IActionResult BurialData(string filter, int pageNum = 1)
    {
        int resultLength = 100;
        int pageNumber = pageNum;

        //Pull Data from DbView
        List<Masterfilter> burials = new List<Masterfilter>();

        int totalRecords = DbContext.Masterfilters.Count();
        int totalPages = (int)Math.Ceiling((double)totalRecords / resultLength);

        if (pageNumber <= totalPages)
        {
            burials = DbContext.Masterfilters
                .OrderBy(x => x.Burialid)
                .Skip((pageNumber - 1) * resultLength)
                .Take(resultLength)
                .ToList();
        }
        //Git?
        //Pass Data to backend
        BurialmainsViewModel data = new BurialmainsViewModel
        {
            //Books to view on this page
            ListBurials = burials,
            //Pass to ViewModel
            PageInfo = new PageInfo
            {

                TotalNumBurials = totalRecords,
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
    public IActionResult Supervised()
    {
        return View();
    }
    public IActionResult Supervised2()
    {
        return View();
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

