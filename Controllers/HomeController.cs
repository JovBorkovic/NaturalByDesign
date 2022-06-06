using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NbdAplication.Data;
using NbdAplication.Models;
using NbdAplication.ViewModels;

namespace NbdAplication.Controllers
{
    public class HomeController : Controller
    {
        //private readonly NbdContext _context;

        private readonly ILogger<HomeController> _logger;
        //public HomeController(NbdContext context)
        //{
        //    _context = context;
        //}


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult ApptSummary()
        //{
        //    var sumQ = _context.Bids.Include(a => a.Projects)
        //        .GroupBy(a => new { a.ProjectsID, a.Projects.PName})
        //        .Select(grp => new AppoinmentSummary
        //        {

        //            ID = grp.Key.ProjectsID,
        //            PName = grp.Key.PName,
        //            TotalAddedInventoryAmount = grp.Sum(a => a.BidInventories.)
        //        });

        //    return View(sumQ.AsNoTracking().ToList());
        //}

        public IActionResult Index()
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
}
