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
            return View(_BL.GetAll(new Order()));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(OrderVM p_OrderVM)
        {
            if (ModelState.IsValid){
            if (_BL.IsValidAddress(p_OrderVM.Address)){
                _BL.Add(p_OrderVM.MapToModel());
                return RedirectToAction("Index");
            }}
            ModelState.AddModelError("", "Entered Values are invalid");
            return Create();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null){return NotFound();}

            _BL.Delete(new Order((int)Id));

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null){return NotFound();}
            var order = _BL.Get(new Order((int)Id));
            if (order == null){return NotFound();}

            return Edit(order);
        }
        [HttpPost]
        public IActionResult Edit(Order p_Order)
        {
            if (ModelState.IsValid){  
                _BL.Update(p_Order);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Entered Values are invalid");
            return Edit(p_Order.Id);
        }
            




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}