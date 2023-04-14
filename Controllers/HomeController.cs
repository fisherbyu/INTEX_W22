using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BYU_EGYPT_INTEX.Models;
using System.Xml;
using BYU_EGYPT_INTEX.Models.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using BYU_EGYPT_INTEX.Component;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BYU_EGYPT_INTEX.Areas.Identity.Data;

namespace BYU_EGYPT_INTEX.Controllers;

public class HomeController : Controller
{
    //Test GIT
    private readonly ILogger<HomeController> _logger;
    private egyptbyuContext DbContext { get; set; }
    private ApplicationDbContext AuthLinkContext { get; set; }
    private IFilterRepository repo;
    private List<BurialmainsFilteredData> filteredData;

    public HomeController(ILogger<HomeController> logger, egyptbyuContext temp_context, ApplicationDbContext tempLink, IFilterRepository temp)
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


    public IActionResult BurialData(string? burialid, string? depth, string? ageatdeath, string? sex, string? haircolor,
        int? estimatestature, string? headdirection, string? textilecolor, string? textilestructure, string? textilefunction, int pageNum=1)
    {
        int resultLength = 50;

        // Define the search criteria
        var searchCriteria = new
        {
            Column1 = burialid,
            Column2 = depth,
            Column3 = ageatdeath,
            Column4 = sex,
            Column5 = haircolor,
            Column6 = estimatestature,
            Column7 = headdirection,
            Column8 = textilecolor,
            Column9 = textilestructure,
            Column10 = textilefunction
        };

        // Count the number of records that match the search criteria
        var matchingRecords = repo.masterfilters.Where(data =>
            (searchCriteria.Column1 == null || data.Burialid == searchCriteria.Column1) &&
            (searchCriteria.Column2 == null || data.Depth == searchCriteria.Column2) &&
            (searchCriteria.Column3 == null || data.Ageatdeath == searchCriteria.Column3) &&
            (searchCriteria.Column4 == null || data.Sex == searchCriteria.Column4) &&
            (searchCriteria.Column5 == null || data.Haircolor == searchCriteria.Column5) &&
            (searchCriteria.Column6 == null || data.Estimatestature == searchCriteria.Column6) &&
            (searchCriteria.Column7 == null || data.Headdirection == searchCriteria.Column7) &&
            (searchCriteria.Column8 == null || data.Color == searchCriteria.Column8) &&
            (searchCriteria.Column9 == null || data.TextileStructure == searchCriteria.Column9) &&
            (searchCriteria.Column10 == null || data.Textilefunction == searchCriteria.Column10)
        );
        var totalNumBurials = matchingRecords.Count();

        var masterfilters = repo.masterfilters
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
            .Take(resultLength)
            .Select(x => new BurialmainsFilteredData
                {
                    BurialId = x.Burialid,
                    AgeAtDeath = x.Ageatdeath,
                    Depth = x.Depth,
                    Sex = x.Sex,
                    HairColor = x.Haircolor,
                    EstimateStature = x.Estimatestature,
                    HeadDirection = x.Headdirection,
                    TextileColor = x.Color,
                    TextileStructure = x.TextileStructure,
                    TextileFunction = x.Textilefunction
                });

        var data = new BurialmainsViewModel
        {
            FilteredData = filteredData,
            PageInfo = new PageInfo
            {
                TotalNumBurials = totalNumBurials,
                BurialsPerPage = resultLength,
                CurrentPage = pageNum
            }
        };

        return View(data);

    }

    public IActionResult DisplayBurial(long ID)
    {
        Burialmain burialmain = DbContext.Burialmains.Single(x => x.Id == ID);

        
        //Assemble Textiles
        List<Textile> textiles = (from b in DbContext.Burialmains
                                    join bt in DbContext.BurialmainTextiles on b.Id equals bt.MainBurialmainid
                                    join t in DbContext.Textiles on bt.MainTextileid equals t.Id
                                    where b.Id == ID
                                    select t).ToList();

        //Iterate and grab data for each Textile, add to empty List
        List<TextileComposite> textileComposites = new List<TextileComposite>();
        foreach (Textile Index in textiles)
        {
            
            List<Models.Color> colors = (from c in DbContext.Colors
                                        join ct in DbContext.ColorTextiles on c.Id equals ct.MainColorid
                                        where ct.MainTextileid == Index.Id
                                        select c).ToList();
            //Grab Functions 
            List<Textilefunction> functions = (from tf in DbContext.Textilefunctions
                                   join tft in DbContext.TextilefunctionTextiles on tf.Id equals tft.MainTextilefunctionid
                                   where tft.MainTextileid == Index.Id
                                   select tf).ToList();

            //Grab Structures
            List<Structure> structures = (from s in DbContext.Structures
                                               join st in DbContext.StructureTextiles on s.Id equals st.MainStructureid
                                               where st.MainTextileid == Index.Id
                                               select s).ToList();

            List<Analysis> analyses = (from a in DbContext.Analyses
                                       join at in DbContext.AnalysisTextiles on a.Id equals at.MainAnalysisid
                                       where at.MainTextileid == Index.Id
                                       select a).ToList();

            List<Decoration> decorations = (from d in DbContext.Decorations
                                            join dt in DbContext.DecorationTextiles on d.Id equals dt.MainDecorationid
                                            where dt.MainTextileid == Index.Id
                                            select d).ToList();

            List<Dimension> dimensions = (from d in DbContext.Dimensions
                                          join dt in DbContext.DimensionTextiles on d.Id equals dt.MainDimensionid
                                          where dt.MainTextileid == Index.Id
                                          select d).ToList();

            List<Yarnmanipulation> yarnmanipulations = (from y in DbContext.Yarnmanipulations
                                                        join yt in DbContext.YarnmanipulationTextiles on y.Id equals yt.MainYarnmanipulationid
                                                        where yt.MainTextileid == Index.Id
                                                        select y).ToList();


            List<Photodatum> photodata = (from p in DbContext.Photodata
                                          join pt in DbContext.PhotodataTextiles on p.Id equals pt.MainPhotodataid
                                          where pt.MainTextileid == Index.Id
                                          select p).ToList();






            TextileComposite composite = new TextileComposite(Index, colors, functions, structures, analyses, decorations, dimensions, yarnmanipulations, photodata);

            textileComposites.Add(composite);
        }

        //Artifacts
        List<Artifactkomaushimregister> artifacts = (from b in DbContext.Burialmains
                                                     join ba in DbContext.ArtifactkomaushimregisterBurialmains on b.Id equals ba.MainArtifactkomaushimregisterid
                                                     join a in DbContext.Artifactkomaushimregisters on ba.MainBurialmainid equals a.Id
                                                     where b.Id == ID

                                                     select a).ToList();


        DisplayBurialViewModel data = new DisplayBurialViewModel(burialmain)
        {
            burial = burialmain,
            textiles = textileComposites,
            artifacts = artifacts
        };

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

    public IActionResult Unsupervised()
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

