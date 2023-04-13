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

namespace BYU_EGYPT_INTEX.Controllers;

public class HomeController : Controller
{
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
    }


    
    public IActionResult BurialData(int pageNum = 1)
    {
        //Set Pagination Params
        int resultLength = 10;
        int pageNumber = pageNum;



        List<Burialmain> allBurialIds = (from b in DbContext.Burialmains
                                  select b).ToList();
        allBurialIds = allBurialIds
            .Skip((pageNum - 1) * resultLength)
            .Take(resultLength)
            .ToList();

        List<BurialComposite> records = new List<BurialComposite>();
        foreach (Burialmain mainBurial in allBurialIds)
        {
            List<Textile> textiles = (from bt in DbContext.BurialmainTextiles
                                      join t_join in DbContext.Textiles on bt.MainTextileid equals t_join.Id into t_temp
                                      from t in t_temp.DefaultIfEmpty()
                                      where bt.MainBurialmainid == mainBurial.Id
                                      select t ?? new Textile()).ToList();
            List<Models.Color> colors = (from c in DbContext.Colors
                                         join ct in DbContext.ColorTextiles on c.Id equals ct.MainColorid
                                         join t in DbContext.Textiles on ct.MainTextileid equals t.Id
                                         join bt in DbContext.BurialmainTextiles on t.Id equals bt.MainTextileid into bt_temp
                                         from bt in bt_temp.DefaultIfEmpty()
                                         join b in DbContext.Burialmains on bt.MainBurialmainid equals b.Id into b_temp
                                         from b in b_temp.DefaultIfEmpty()
                                         where b.Id == mainBurial.Id
                                         select c ?? new Models.Color()).Distinct().ToList();


            BurialComposite composite = new BurialComposite(mainBurial, textiles, colors);
            records.Add(composite);

        }
        //Change REcords to IQueryable

        //IQueryable<BurialComposite> BurialQuery = records.AsQueryable<BurialComposite>();
        


        List<BurialComposite> BurialList = records
            .Skip((pageNum - 1) * resultLength)
            .Take(resultLength)
            .ToList();









        var data = new BurialmainsViewModel
        {
            ListBurials = BurialList
        };

        return View(data);
    }


    public IActionResult xBurialData(int pageNum = 1)
    {


        return View();
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

    //public IActionResult xIndex(string bookCategory, int pageNum = 1)
    //{
    //    //Set Pagination Params
    //    int resultLength = 10;
    //    int pageNumber = pageNum;


    //    //Declare var Books to make data manipulation simpler
    //    var Books = BookRepo.Books
    //        .Where(x => x.Category == bookCategory || bookCategory == null)
    //        .OrderBy(b => b.Title)
    //        .Skip((pageNum - 1) * resultLength)
    //        .Take(resultLength);

    //    //Pass Data to backend
    //    var data = new BooksViewModel
    //    {
    //        //Books to view on this page
    //        Books = Books,

    //        PageInfo = new PageInfo
    //        {

    //            TotalNumBooks = (
    //                bookCategory == null
    //                ? BookRepo.Books.Count()
    //                : BookRepo.Books
    //                .Where(x => x.Category == bookCategory)
    //                .Count()),
    //            BooksPerPage = resultLength,
    //            CurrentPage = pageNum
    //        }
    //    };

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

