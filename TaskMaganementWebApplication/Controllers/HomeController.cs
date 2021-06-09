using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskMaganementWebApplication.Data;
using TaskMaganementWebApplication.Models;


namespace TaskMaganementWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ApplicationTaskDbContext _context;
        
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationTaskDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            
            HttpContext.Session.Clear();
            
            return RedirectToAction("Login"); ;
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(ApplicationUser model)
        {
            var user = await _userManager.FindByNameAsync(model.User);
            if (user != null)
            {
                var SignIn = await _signInManager.PasswordSignInAsync(user, user.Password, false, false);
                if (SignIn.Succeeded)
                {
                    HttpContext.Session.SetString("UserName", user.UserName.ToString());
                    HttpContext.Session.SetString("UserType", user.UserType.ToString());                                      
                    return RedirectToAction("TaskManagement");
                }               
            }           
            return RedirectToAction("Register"); 
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(ApplicationUser model)
        {
            var user = new ApplicationUser
            {                
                UserName = model.User,
                Password = model.Password,
                UserType = model.UserType
            };
            var result = await _userManager.CreateAsync(user, user.Password);
            if (result.Succeeded)
            {
                var SignIn = await _signInManager.PasswordSignInAsync(user, user.Password, false, false);
                if (SignIn.Succeeded)
                {
                    
                    return RedirectToAction("Login");
                }               
            }
            return new JsonResult(result.Errors);
        }
        [HttpGet]
        public IActionResult TaskManagement()
        {
            ViewBag.user = HttpContext.Session.GetString("UserType");
            ViewBag.userName = HttpContext.Session.GetString("UserName");
            var Tasks = _context.ApplicationTasks.ToList();
            return View(Tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ApplicationTasks tasks)
        {
            var result = _context.ApplicationTasks.Select(x => x.Id).FirstOrDefault();
            if (result !=0)
            {
                var newID = _context.ApplicationTasks.Select(x => x.Id).Max() + 1;
                tasks.Id = newID;
               tasks.Status = StatusType.New;
            }
            else {
                tasks.Id = 1;
            }
            _context.ApplicationTasks.Add(tasks);
            _context.SaveChanges();
            return RedirectToAction("TaskManagement");
        }
        public IActionResult Edit(int id)
        {
            var tasks = _context.ApplicationTasks.Find(id);
            return View(tasks);
        }

        [HttpPost]
        public IActionResult Edit(ApplicationTasks tasks)
        {
            _context.ApplicationTasks.Update(tasks);
            _context.SaveChanges();

            return RedirectToAction("TaskManagement");
        }
        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
