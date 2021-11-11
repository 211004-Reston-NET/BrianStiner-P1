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
            return View(_BL.GetAll(new Store()) //Map to view model
            .Select(x => new StoreVM(x))
            .ToList()
            );
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null){return NotFound();}

            _BL.Delete(new Store((int)Id));

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Select(int? Id)
        {
            if (Id == null){return NotFound();}

            _BL.Update(new Store((int)Id));

            return RedirectToAction(nameof(Index));
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
            return View(p_StoreVM);
        }



        public IActionResult Edit(int p_Id)
        {
            return View(new StoreVM(_BL.Get(new Store(p_Id))));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StoreVM p_StoreVM)
        {
            if (ModelState.IsValid){
                _BL.Update(p_StoreVM.MapToModel());
                return RedirectToAction("Index");
            }
            return View(p_StoreVM);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}