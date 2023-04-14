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

