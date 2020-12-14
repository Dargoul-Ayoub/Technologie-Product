using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechProduct.Models
{
    public class EFProductReposotory : IProductRepository
    {
        private DBaseTechProducts eF = new DBaseTechProducts();

        public IEnumerable<Product> Products {
            get => eF.dbProducts;
            
        }
    }
}
