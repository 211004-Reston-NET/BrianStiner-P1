using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebInterface.Models;
using BusinessLogic;
using Models;
using System.Linq;

namespace WebInterface.Controllers
{
    public class StoreController : Controller
    {
        private readonly ILogger<StoreController> _logger;
        private IBusiness _BL; 
            
        //     DbContextOptionsBuilder<revaturedatabaseContext> options = new DbContextOptionsBuilder<revaturedatabaseContext>()
        //         .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        public StoreController(IBusiness p_BL, ILogger<StoreController> p_logger)
        {
            _logger = p_logger;
            _BL = p_BL;
        }

        public IActionResult Index()
        {
            return View(_BL.GetAll(new Store()));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StoreVM p_StoreVM)
        {
            if (ModelState.IsValid){
                _BL.Add(p_StoreVM.MapToModel());
                return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int? Id)
        {
            if (Id == null){return NotFound();}

            _BL.Delete(new Store((int)Id));

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Select(int? Id)                            //View the whole customer
        {
            if (Id == null){return NotFound();}
            return View( _BL.Get( new Store((int)Id) ).ToArrayList() );
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null){return NotFound();}

            var Store = _BL.Get(new Store((int)Id));
            if (Store == null){return NotFound();}

            return View(new StoreVM(Store));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StoreVM p_StoreVM)
        {
            if (ModelState.IsValid){
                _BL.Update(p_StoreVM.MapToModel());
                return RedirectToAction("Index");
            }
            return Index();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}