using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebInterface.Models;
using BusinessLogic;
using Models;
using System.Linq;
using System.Collections;

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


        [HttpPost]
        public IActionResult Create(Order p_Order)
        {
            if (ModelState.IsValid){
            if (_BL.IsValidAddress(p_Order.Address)){
                _BL.Add(p_Order);
                return RedirectToAction("Select", "Customer", new { id = p_Order.CustomerId });
            }}
            ModelState.AddModelError("", "Entered Values are invalid");
            return Index();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null){return NotFound();}
            var order = _BL.Get(new Order() { Id = (int)Id });
            order.Customer = _BL.Get(new Customer() { Id = order.CustomerId });
            _BL.Delete(new Order((int)Id));

            return RedirectToAction("Select", "Customer", new { Id = order.CustomerId });
        }


        public IActionResult Select(int? Id)                            //View the whole customer
        {
            if (Id == null){return NotFound();}
            return View( _BL.Get( new Order((int)Id) ).ToArrayList() );
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


        public IActionResult AddItem(int? Id)
        {
            if (Id == null){return NotFound();}
            ViewBag.Products = _BL.GetAll(new Product());
            LineItem lineitem = new LineItem();
            lineitem.OrderId = (int)Id; lineitem.Order = _BL.Get(new Order((int)Id));
            return View(lineitem);
        }

        [HttpPost("Order/AddItem")]
        public IActionResult AddItem(LineItem p_LineItem)
        {
            p_LineItem.Product  = _BL.Get(new Product() { Id = p_LineItem.ProductId });
            p_LineItem.Order    = _BL.Get(new Order() { Id = p_LineItem.OrderId });
            if (_BL.IsValidLineItem(p_LineItem)){
                _BL.Add(p_LineItem);
                return RedirectToAction("Select", "Order", new { Id = p_LineItem.OrderId });
            }
            ModelState.AddModelError("", "Entered Values are invalid");
            return AddItem(p_LineItem.OrderId);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}