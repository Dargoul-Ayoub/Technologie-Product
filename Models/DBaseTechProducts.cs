using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TechProduct.Models
{
    public class DBaseTechProducts:DbContext
    {
        public DBaseTechProducts()
        {
        }

        public DBaseTechProducts(DbContextOptions<DBaseTechProducts> options) : base(options)
        {

        }

        public DbSet<Product> dbProducts { get; set;  }
    }
}
