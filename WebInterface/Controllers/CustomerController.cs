using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebInterface.Models;
using BusinessLogic;
using Models;
using System.Linq;

namespace WebInterface.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private IBusiness _BL;

        public CustomerController(IBusiness p_BL, ILogger<CustomerController> p_logger)
        {
            _logger = p_logger;
            _BL = p_BL;
        }

        public IActionResult Index()
        {
            return View(_BL.GetAll(new Customer()) //Map to view model
            .Select(x => new CustomerVM(x))
            .ToList()
            );
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(CustomerVM p_customerVM)
        {
            if (ModelState.IsValid){
                _BL.Add(p_customerVM.MapToModel());
                return RedirectToAction("Index");
            }
            return View(p_customerVM);
        }
        public IActionResult Edit(int p_Id)
        {
            return View(new CustomerVM(_BL.Get(new Customer(p_Id))));
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