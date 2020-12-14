using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechProduct.Models;

namespace TechProduct.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBaseTechProducts _dBaseTechProducts;
       
     
                 
        public HomeController(ILogger<HomeController> logger, DBaseTechProducts dBaseTechProduct)
        {
            _logger = logger;
            _dBaseTechProducts = dBaseTechProduct;
        }
       
        public async Task<IActionResult> Index(int? pageNumber,string category="All")
        {

            ViewBag.CurrentCategory = category;
            ViewBag.CurrentPage = pageNumber;

            int pageSize = 3;
            if (category == "All")
            {

                return View(await PaginatedList<Product>.CreateAsync(_dBaseTechProducts.dbProducts.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            else
            {
                return View(await PaginatedList<Product>.CreateAsync(_dBaseTechProducts.dbProducts.AsNoTracking().Where(p => p.Name == category || category == null), pageNumber ?? 1, pageSize));

            }


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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Name, string Password)
        {
            ClaimsIdentity identity = null;
             bool isAuthenticated = false;
            if(Name=="admin" && Password == "admin")
            {
                identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name,"admin"),
                new Claim(ClaimTypes.Role,"Admin")
                 }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticated = true;
            }
            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                this.HttpContext.Response.Cookies.Append("Email","admin");
                return RedirectToAction("Index", "Admin");
            }
            else if(Name!="" || Password!="")
            {
                ViewBag.Alert = "Try again if you're Admin , go away if you're not ";
            }
            return View();
        }

        public string Forbidden()
        {
            return "you don't have access";
        }
    }
}
