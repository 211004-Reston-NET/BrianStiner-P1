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
            
        //     DbContextOptionsBuilder<revaturedatabaseContext> options = new DbContextOptionsBuilder<revaturedatabaseContext>()
        //         .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        public CustomerController(IBusiness p_BL, ILogger<CustomerController> p_logger)
        {
            _logger = p_logger;
            _BL = p_BL;
        }

        public IActionResult Index()
        {
            return View(_BL.GetAll(new Customer()));
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CustomerVM p_customerVM)
        {
            if (ModelState.IsValid){
            if(_BL.IsValidCustomer(p_customerVM.MapToModel())){
                _BL.Add(p_customerVM.MapToModel());
                return RedirectToAction("Index");
            }}
            ModelState.AddModelError("", "Entered Values are invalid");
            return Create();
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int? Id)
        {
            if (Id == null){return NotFound();}

            _BL.Delete(new Customer((int)Id));

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Select(int? Id)
        {
            if (Id == null){return NotFound();}
            return View( _BL.Get( new Customer((int)Id) ).ToArrayList(null) );
        }

        public IActionResult Test(int? Id)
        {
            if (Id == null){return NotFound();}
            var test =_BL.GetAll( new Customer((int)Id) ).Where(x => x.Id == Id).FirstOrDefault();
            test.Orders = _BL.GetAll(new Order());
            return View(  test.ToArrayList(null) );
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int? Id)
        {
            if (Id == null){return NotFound();}

            var customer = _BL.Get(new Customer((int)Id));
            if (customer == null){return NotFound();}

            return View(new CustomerVM(customer));
        }
        [HttpPost("Edit")]
        public IActionResult Edit(CustomerVM p_customerVM)
        {
            if (ModelState.IsValid){
            if(_BL.IsValidCustomer(p_customerVM.MapToModel())){
                _BL.Update(p_customerVM.MapToModel());
                return RedirectToAction("Index");
            }}
            ModelState.AddModelError("", "Entered Values are invalid");
            return Edit(p_customerVM.Id);
        }
            




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}