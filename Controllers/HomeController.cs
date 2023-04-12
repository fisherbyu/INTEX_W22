using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BYU_EGYPT_INTEX.Models;
using System.Xml;
using BYU_EGYPT_INTEX.Models.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;
using BYU_EGYPT_INTEX.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BYU_EGYPT_INTEX.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<HomeController> _logger;
    private egyptbyuContext DbContext { get; set; }
    private ApplicationDbContext AuthLinkContext { get; set; }

    public HomeController(ILogger<HomeController> logger, egyptbyuContext temp_context, ApplicationDbContext tempLink, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        DbContext = temp_context;
        AuthLinkContext = tempLink;
        _userManager = userManager;
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

        //Declare var Burial to make data manipulation simpler
        var Burials = DbContext.Burialmains
            .Skip((pageNum - 1) * resultLength)
            .Take(resultLength);

        var data = new BurialmainsViewModel
        {
            Burialmains = Burials,

            PageInfo = new PageInfo
            {
                TotalNumBurials = (
                    DbContext.Burialmains.Count()
                ),
                BurialsPerPage = resultLength,
                CurrentPage = pageNum
            }
        };

        //var data = new BurialmainsViewModel
        //{
        //    Burialmains = Burials;

        //    PageInfo = new PageInfo;
        //}
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
    //[Authorize(Roles = "Admin")]
    //public async Task<IActionResult> Users()
    //{
        //var users = await _userManager.Users.ToListAsync();
        //return View(users);
    //}

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

