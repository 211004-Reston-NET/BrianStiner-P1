using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebInterface.Models;
using BusinessLogic;
using Models;
using System.Linq;

namespace WebInterface.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private IBusiness _BL; 
            
        //     DbContextOptionsBuilder<revaturedatabaseContext> options = new DbContextOptionsBuilder<revaturedatabaseContext>()
        //         .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        public ProductController(IBusiness p_BL, ILogger<ProductController> p_logger)
        {
            _logger = p_logger;
            _BL = p_BL;
        }

        public IActionResult Index()
        {
            return View(_BL.GetAll(new Product()) //Map to view model
            .Select(x => new ProductVM(x))
            .ToList()
            );
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductVM p_productVM)
        {
            if (ModelState.IsValid){
                _BL.Add(p_productVM.MapToModel());
                return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int? Id)
        {
            if (Id == null){return NotFound();}

            _BL.Delete(new Product((int)Id));

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null){return NotFound();}

            var product = _BL.Get(new Product((int)Id));
            if (product == null){return NotFound();}

            return View(new ProductVM(product));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM p_productVM)
        {
            if (ModelState.IsValid){
                _BL.Update(p_productVM.MapToModel());
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