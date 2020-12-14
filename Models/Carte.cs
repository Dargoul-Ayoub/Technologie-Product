using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechProduct.Models
{
    public class Carte
    {
        private static List<CarteLine> lineCollection = new List<CarteLine>();
        public static string returnUrl;
        public void AddItem(Product product,int quantity)
        {
            CarteLine line = lineCollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CarteLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
                line.Quantity += quantity;
        }
        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(p => p.Product.ProductID == product.ProductID);
        }
        public static decimal CompTotalValue()
        {
            return lineCollection.Sum(p => p.Product.Price * p.Quantity);
        }
        public static void Clear()
        {
            lineCollection.Clear();
        }
        public static IEnumerable<CarteLine> Lines
        {
            get { return lineCollection; }
        }
        public class CarteLine
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}
