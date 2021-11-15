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


        public IActionResult Select(int? Id)                            //View the whole customer
        {
            if (Id == null){return NotFound();}
            return View( _BL.Get( new Customer((int)Id) ).ToArrayList(null) );
        }


        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int? Id)                              //Edit the customer
        {
            if (Id == null){return NotFound();}
            var customer = _BL.Get(new Customer((int)Id));
            if (customer == null){return NotFound();}

            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(Customer p_customer)
        {
            if (ModelState.IsValid){
                _BL.Update(p_customer);
                return Select(p_customer.Id);
            }
            ModelState.AddModelError("", "Entered Values are invalid");
            return Edit(p_customer.Id);
        }
            

        public IActionResult AddOrder(int? Id){                         //add order to customer
            if (Id == null){return NotFound();}
            var customer = _BL.Get(new Customer((int)Id));
            if (customer == null){return NotFound();}
            return View(customer);
        }
        [HttpPost]
        public IActionResult AddOrder(Customer p_customer)
        {
            if (ModelState.IsValid){
                _BL.Update(p_customer);
                return Select(p_customer.Id);
            }
            ModelState.AddModelError("", "Entered Values are invalid");
            return AddOrder(p_customer.Id);
        }

        public IActionResult Purchase(int? Id){                         //add order to customer
            if (Id == null){return NotFound();}
            var customer = _BL.Get(new Customer((int)Id));
            _BL.TransactOrders(customer);
            return Select(customer.Id);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}