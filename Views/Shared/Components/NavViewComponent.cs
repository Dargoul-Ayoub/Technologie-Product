using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechProduct.Models;

namespace TechProduct.Views.Shared.Components
{
    public class NavViewComponent:ViewComponent
    {
        private readonly DBaseTechProducts _dBasTechProduct;
        public NavViewComponent(DBaseTechProducts dBaseTechProduct)
        {
            _dBasTechProduct = dBaseTechProduct;
        }
        public IViewComponentResult Invoke(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _dBasTechProduct.dbProducts.AsNoTracking().Select(x => x.Name).Distinct().OrderBy(x => x);
            return View("Menu", categories);
        }

    }
}
