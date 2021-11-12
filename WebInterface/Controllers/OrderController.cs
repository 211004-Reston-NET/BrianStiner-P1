using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebInterface.Models;
using BusinessLogic;
using Models;
using System.Linq;

namespace WebInterface.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private IBusiness _BL; 
            
        //     DbContextOptionsBuilder<revaturedatabaseContext> options = new DbContextOptionsBuilder<revaturedatabaseContext>()
        //         .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        public OrderController(IBusiness p_BL, ILogger<OrderController> p_logger)
        {
            _logger = p_logger;
            _BL = p_BL;
        }

        public IActionResult Index()
        {
            return View(_BL.GetAll(new Order()) //Map to view model
            .Select(x => new OrderVM(x))
            .ToList()
            );
        }




        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(OrderVM p_OrderVM)
        {
            if (ModelState.IsValid){
                _BL.Add(p_OrderVM.MapToModel());
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Entered Values are invalid");
            return Create();
        }


        public IActionResult Delete(int? Id)
        {
            if (Id == null){return NotFound();}

            _BL.Delete(new Order((int)Id));

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null){return NotFound();}

            var Order = _BL.Get(new Order((int)Id));
            if (Order == null){return NotFound();}

            return View(new OrderVM(Order));
        }
        [HttpPost]
        public IActionResult Edit(OrderVM p_OrderVM)
        {
            if (ModelState.IsValid){  
                _BL.Update(p_OrderVM.MapToModel());
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Entered Values are invalid");
            return Edit(p_OrderVM.Id);
        }
            




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}