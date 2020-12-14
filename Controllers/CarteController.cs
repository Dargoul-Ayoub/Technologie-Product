using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechProduct.Models;

namespace TechProduct.Controllers
{
    public class CarteController : Controller
    {
        private readonly DBaseTechProducts _dBaseTechProduct;
        Carte carte = new Carte();

        public CarteController(DBaseTechProducts dBaseTechProduct)
        {
            _dBaseTechProduct = dBaseTechProduct;
        }
      
        public IActionResult AddToCarte(int productId,string returnUrl)
        {
            Product product = _dBaseTechProduct.dbProducts.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                carte.AddItem(product, 1);
                
                Carte.returnUrl = returnUrl;
            }
            
            //return View();
            return RedirectToAction(nameof(Index)) ;
        }
        public IActionResult RemoveFromCarte(int productId,string returnUrl)
        {
            Product product = _dBaseTechProduct.dbProducts.FirstOrDefault(p => p.ProductID == productId);
            if(product != null)
            {
                carte.RemoveLine(product);
            }
            return RedirectToAction(nameof(Index));
        }

      
        public IActionResult Index()
        {
            ViewBag.returnUrl = Carte.returnUrl;
            return  View();
        }
        public PartialViewResult Summary(Carte carte)
        {
            return PartialView(carte);
        }
        public IActionResult Order()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Order(ShippingDetail shippingDetail)
        {
            if (Carte.Lines.Count() == 0)
                ModelState.AddModelError("Empty", "Sorry, your carte is empty!");
            if (ModelState.IsValid)
            {
                Carte.Clear();
                return RedirectToAction(nameof(Complited));

            }
            else
                return View(shippingDetail);
        }
        public IActionResult Complited()
        {
            return View();
        }
    }
}
