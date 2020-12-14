using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechProduct.Models;

namespace TechProduct.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly DBaseTechProducts _dBaseTechProduct;
        public AdminController(DBaseTechProducts dBaseTechProduct)
        {
            _dBaseTechProduct = dBaseTechProduct;
        }
        public IActionResult Index()
        {
            return View(_dBaseTechProduct.dbProducts.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _dBaseTechProduct.dbProducts.Add(product);
            _dBaseTechProduct.SaveChanges();
            TempData["message"] = product.Name + " has been saved successfly";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int productID)
        {
            Product product = _dBaseTechProduct.dbProducts.FirstOrDefault(p => p.ProductID == productID);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product,IFormCollection form,IFormFile file)
        {
            
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (file.Length > 0)
                    {
                        product.ImageMimType = file.ContentType;

                        using (var memory = new MemoryStream())
                        {
                            file.CopyToAsync(memory);
                            product.ImageData = memory.ToArray();
                            TempData["message"] = product.Name + " Changes has been saved successfly";
                        }
                    }
                    _dBaseTechProduct.Entry(product).State = EntityState.Modified;
                    _dBaseTechProduct.SaveChanges();
                }
                // productID proprety is always null when its input as hidden , i don't know why i have to show in razor page to get his value

                else
                {
                    // i did this because if i was putting the picture of the product and i wanted to edite some informations without change the image ,
                    // it will take the file as null so the picture will change to No Image picture 

                    var prdct = _dBaseTechProduct.dbProducts.Where(s => s.ProductID == product.ProductID).FirstOrDefault();
                    prdct.Name = product.Name;
                    prdct.Price = product.Price;
                    prdct.Description = product.Description;
                    prdct.Category = product.Category;
                    _dBaseTechProduct.Entry(prdct).State = EntityState.Modified;
                    _dBaseTechProduct.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            else
                return View();
           
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _dBaseTechProduct.dbProducts.Remove(product);
            _dBaseTechProduct.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public FileResult getImage(int productID)
        {
            Product product = _dBaseTechProduct.dbProducts.FirstOrDefault(p => p.ProductID == productID);
                return File(product.ImageData, product.ImageMimType);
        }

        public IActionResult Logout()
        {
            this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "home");
        }
    }
}
