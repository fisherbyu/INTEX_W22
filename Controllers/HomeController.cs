using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BYU_EGYPT_INTEX.Models;
using System.Xml;
using BYU_EGYPT_INTEX.Models.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;
using BYU_EGYPT_INTEX.Data;
using Microsoft.EntityFrameworkCore;

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
        ////Set Pagination Params
        //int resultLength = 10;
        //int pageNumber = pageNum;

        ////Declare var Burial to make data manipulation simpler
        //var Burials = DbContext.Burialmains
        //    .Skip((pageNum - 1) * resultLength)
        //    .Take(resultLength);

        //var data = new BurialmainsViewModel
        //{
        //    Burialmains = Burials,

        //    PageInfo = new PageInfo
        //    {
        //        TotalNumBurials = (
        //            DbContext.Burialmains.Count()
        //        ),
        //        BurialsPerPage = resultLength,
        //        CurrentPage = pageNum
        //    }
        //};

        ////var data = new BurialmainsViewModel
        ////{
        ////    Burialmains = Burials;

        ////    PageInfo = new PageInfo;
        ////}
        ///

        var data = from b in DbContext.Burialmains
                   join l in DbContext.BurialmainTextiles on b.Id equals l.MainBurialmainid
                   join t in DbContext.Textiles on l.MainTextileid equals t.Id
                   select new { Burial = b, Textile = t };
        return View(data);
    }

    public IActionResult DisplayBurial(long ID)
    {
        return View();
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

