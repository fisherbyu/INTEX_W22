using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BYU_EGYPT_INTEX.Models;
using System.Xml;

namespace BYU_EGYPT_INTEX.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private egyptbyuContext DbContext { get; set; }

    public HomeController(ILogger<HomeController> logger, egyptbyuContext temp_context)
    {
        _logger = logger;
        DbContext = temp_context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult BurialData()
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

