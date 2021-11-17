using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using WebInterface.Models;
using BusinessLogic;
using System;

namespace WebInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IBusiness _BL;

        public HomeController(IBusiness p_BL, ILogger<HomeController> logger)
        {
            _logger = logger;
            _BL = p_BL;
        }

        public IActionResult Index()
        {   
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Search(string id)
        {
            ArrayList results;
            if (!String.IsNullOrEmpty(id))
            {results = _BL.SearchAll(id);}
            else
            {results = _BL.SearchAll("");}
                
            return View(results);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
