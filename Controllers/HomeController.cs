using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BYU_EGYPT_INTEX.Models;
using System.Xml;
using BYU_EGYPT_INTEX.Models.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using ASP.NETCoreIdentityCustom.Areas.Identity.Data;

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

        //var burialRecords = from b in DbContext.Burialmains
        //           join l in DbContext.BurialmainTextiles on b.Id equals l.MainBurialmainid
        //           join t in DbContext.Textiles on l.MainTextileid equals t.Id
        //           join ctx in DbContext.ColorTextiles on t.Id equals ctx.MainTextileid
        //           join c in DbContext.Colors on ctx.MainColorid equals c.Id
        //           select new BurialCombined { burialmain = b, textile = t, color = c };

        //Query DB
        var records = from b in DbContext.Burialmains
                      //Link burialmain-textile table
                      join bt_join in DbContext.BurialmainTextiles on b.Id equals bt_join.MainBurialmainid into bt_temp
                      from bt in bt_temp.DefaultIfEmpty()
                      //Link textile table
                      join t_join in DbContext.Textiles on bt.MainTextileid equals t_join.Id into t_temp
                      from t in t_temp.DefaultIfEmpty()
                      //Link color-textile Table
                      join ct_join in DbContext.ColorTextiles on t.Id equals ct_join.MainTextileid into ct_temp
                      from ct in ct_temp.DefaultIfEmpty()
                      //Link color table
                      join c_join in DbContext.Colors on ct.MainColorid equals c_join.Id into c_temp
                      from c in c_temp.DefaultIfEmpty()
                      //link burialmain-bodyanalysischart table
                      join bacb_join in DbContext.BurialmainBodyanalysischarts on b.Id equals bacb_join.MainBurialmainid into bacb_temp
                      from bacb in bacb_temp.DefaultIfEmpty()
                      //Link Bodyanalysischart Table
                      join bac_join in DbContext.Bodyanalysischarts on bacb.MainBodyanalysischartid equals bac_join.Id into bac_temp
                      from bac in bac_temp.DefaultIfEmpty()
                      //Link textilefunction-textile table
                      join tft_join in DbContext.TextilefunctionTextiles on t.Id equals tft_join.MainTextileid into tft_temp
                      from tft in tft_temp.DefaultIfEmpty()
                      //Link textilefunction table
                      join tf_join in DbContext.Textilefunctions on tft.MainTextilefunctionid equals tf_join.Id into tf_temp
                      from tf in tf_temp.DefaultIfEmpty()

                      select new BurialCombined
                      {
                          burialmain = b,
                          textile = t ?? new Textile(),
                          color = c ?? new Models.Color(),
                          textileFunction = tf ?? new Textilefunction(),
                          bodyAnalysisChart = bac ?? new Bodyanalysischart()
                      };


        //var result = from b in DbContext.Burialmains
        //             join bt in DbContext.BurialmainTextiles on b.Id equals bt.MainBurialmainid into temp1
        //    from t1 in temp1.DefaultIfEmpty()
        //    join t in DbContext.Textiles on t1.MainTextileid equals t.Id into temp2
        //    from t2 in temp2.DefaultIfEmpty()
        //    join ct in DbContext.ColorTextiles on t2.Id equals ct.MainTextileid into temp3
        //    from t3 in temp3.DefaultIfEmpty()
        //    join c in DbContext.Colors on t3.MainColorid equals c.Id into temp4
        //    from t4 in temp4.DefaultIfEmpty()
        //    select new BurialCombined { burialmain = b, textile = t2, color = t4 };

        var data = new BurialmainsViewModel
        {
            burials = records
        };

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

