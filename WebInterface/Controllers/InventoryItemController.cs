using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebInterface.Models;
using BusinessLogic;
using Models;
using System.Linq;
using System.Collections.Generic;

namespace WebInterface.Controllers
{
    public class InventoryItemController : Controller
    {
        private readonly ILogger<InventoryItemController> _logger;
        private IBusiness _BL; 
            
        //     DbContextOptionsBuilder<revaturedatabaseContext> options = new DbContextOptionsBuilder<revaturedatabaseContext>()
        //         .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        public InventoryItemController(IBusiness p_BL, ILogger<InventoryItemController> p_logger)
        {
            _logger = p_logger;
            _BL = p_BL;
        }

        public IActionResult Index()
        {
            return View(_BL.GetAll(new InventoryItem()));
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(InventoryItem p_InventoryItem)
        {
            if (ModelState.IsValid){
                _BL.Add(p_InventoryItem);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Invalid Entrys");
            return Create();

        }

        [HttpGet("InventoryItem/Delete/{id}")]
        public IActionResult Delete(int? Id)
        {
            if (Id == null){return NotFound();}
            var p_InventoryItem = _BL.Get(new InventoryItem() { Id = (int)Id });
            if (p_InventoryItem == null){return NotFound();}

            _BL.Delete(new InventoryItem((int)Id));

            return RedirectToAction(nameof(Select), nameof(Store), new { id = p_InventoryItem.StoreId });
        }


        public IActionResult Select(int? Id)                            //View the whole InventoryItem
        {
            if (Id == null){return NotFound();}
            return View( _BL.Get( new InventoryItem((int)Id) ).ToArrayList() );
        }

        [HttpGet("InventoryItem/Edit/{id}")]
        public IActionResult Edit(int? Id)                              //Edit the InventoryItem
        {
            if (Id == null){return NotFound();}
            var InventoryItem = _BL.Get(new InventoryItem((int)Id));
            if (InventoryItem == null){return NotFound();}

            return View(InventoryItem);
        }
        [HttpPost("InventoryItem/Edit/{id}")]
        public IActionResult Edit(InventoryItem p_InventoryItem)
        {
            if (ModelState.IsValid){
                _BL.Update(p_InventoryItem);
                return RedirectToAction(nameof(Select), new { Id = p_InventoryItem.Id });
            }
            ModelState.AddModelError("", "Entered Values are invalid");
            return Edit(p_InventoryItem.Id);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}