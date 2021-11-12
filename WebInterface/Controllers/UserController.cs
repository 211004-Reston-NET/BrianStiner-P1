using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebInterface.Models;
using BusinessLogic;
using Models;
using System.Linq;

namespace WebInterface.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private IBusiness _BL; 
            
        //     DbContextOptionsBuilder<revaturedatabaseContext> options = new DbContextOptionsBuilder<revaturedatabaseContext>()
        //         .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        public UserController(IBusiness p_BL, ILogger<UserController> p_logger)
        {
            _logger = p_logger;
            _BL = p_BL;
        }

        public IActionResult Index()
        {
            return View(_BL.GetAll(new Customer()) //Map to view model
            .Select(x => new CustomerVM(x))
            .ToList()
            );
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(NewUserVM p_NewUserVM)
        {
            if (ModelState.IsValid){
                if(_BL.CreateUser(p_NewUserVM.Username, p_NewUserVM.Password1, p_NewUserVM.Password2, p_NewUserVM.Email, p_NewUserVM.Phone) ){
                    return RedirectToAction("Index");
                }
                return Create();
            }
            return Create();
        }


        public IActionResult Login1()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login1(UserVM p_UserVM)
        {
            if (ModelState.IsValid){
                if(_BL.Login(p_UserVM.Username, p_UserVM.Password)){
                    p_UserVM.LoggedIn = true;
                    return RedirectToAction("Home/Index");
                }
                return Login1();
            }
            return Login1();
        }
    }
}