using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebInterface.Models;
using BusinessLogic;
using Models;
using System.Linq;

namespace WebInterface.Controllers
{
    public class LineItemController : Controller
    {
        private readonly ILogger<LineItemController> _logger;
        private IBusiness _BL; 
            
        //     DbContextOptionsBuilder<revaturedatabaseContext> options = new DbContextOptionsBuilder<revaturedatabaseContext>()
        //         .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        public LineItemController(IBusiness p_BL, ILogger<LineItemController> p_logger)
        {
            _logger = p_logger;
            _BL = p_BL;
        }

        public IActionResult Index()
        {
            return View(_BL.GetAll(new LineItem()));
        }




        public IActionResult Create()
        {
            return View(_BL.GetAll(new Product()));
        }
        [HttpPost]
        public IActionResult Create(LineItem p_LineItem)
        {
            if (ModelState.IsValid){
            if(_BL.IsValidQuantity(p_LineItem.Quantity)){
                _BL.Add(p_LineItem);
                return RedirectToAction("Index");
            }}
            ModelState.AddModelError("", "Entered Values are invalid");
            return Create();
        }


        public IActionResult Delete(int? Id)
        {
            if (Id == null){return NotFound();}

            _BL.Delete(new LineItem((int)Id));

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null){return NotFound();}

            var LineItem = _BL.Get(new LineItem((int)Id));
            if (LineItem == null){return NotFound();}

            return View(new LineItemVM(LineItem));
        }
        [HttpPost]
        public IActionResult Edit(LineItemVM p_LineItemVM)
        {
            if (ModelState.IsValid){
                _BL.Update(p_LineItemVM.MapToModel());
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Entered Values are invalid");
            return Edit(p_LineItemVM.Id);
        }
            




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}