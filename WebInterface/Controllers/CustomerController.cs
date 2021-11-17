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
        public IActionResult Create(Customer p_customer)
        {
            if (ModelState.IsValid){
            if(_BL.IsValidCustomer(p_customer)){
                _BL.Add(p_customer);
                return RedirectToAction("Index");
            }}
            ModelState.AddModelError("", "Invalid Entrys");
            return Create();

        }

        [HttpGet("Customer/Delete/{id}")]
        public IActionResult Delete(int? Id)
        {
            if (Id == null){return NotFound();}

            _BL.Delete(new Customer((int)Id));

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Select(int? Id)                            //View the whole customer
        {
            if (Id == null){return NotFound();}
            return View( _BL.Get( new Customer((int)Id) ).ToArrayList() );
        }

        [HttpGet("Customer/Edit/{id}")]
        public IActionResult Edit(int? Id)                              //Edit the customer
        {
            if (Id == null){return NotFound();}
            var customer = _BL.Get(new Customer((int)Id));
            if (customer == null){return NotFound();}

            return View(customer);
        }
        [HttpPost("Customer/Edit/{id}")]
        public IActionResult Edit(Customer p_customer)
        {
            if (ModelState.IsValid){
                _BL.Update(p_customer);
                return RedirectToAction(nameof(Select), new { Id = p_customer.Id });
            }
            ModelState.AddModelError("", "Entered Values are invalid");
            return Edit(p_customer.Id);
        }
            

        public IActionResult AddOrder(int? Id)          //add new fresh order to customer
        {
            Order order = new Order();
            order.CustomerId = (int)Id;
            order.Customer = _BL.Get(new Customer((int)Id));
            order.Active = true;
            order.Address = order.Customer.Address;
            order.LineItems = new List<LineItem>();
            _BL.Add(order);
            return RedirectToAction("Select", new { id = Id });
        }


        public IActionResult Purchase(int? Id){                        
            if (Id == null){return NotFound();}
            var customer = _BL.Get(new Customer((int)Id));
            if (customer == null){return NotFound();}
            _BL.TransactOrders(customer);
            return RedirectToAction("Select", new { id = Id });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}