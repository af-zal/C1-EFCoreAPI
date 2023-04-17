using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductsAPICore.Model;

namespace ProductsAPICore.Data
{
    public class ProductsAPICoreContext : DbContext
    {
        public ProductsAPICoreContext (DbContextOptions<ProductsAPICoreContext> options)
            : base(options)
        {
        }

        public DbSet<ProductsAPICore.Model.Product> Product { get; set; }
        public DbSet<ProductsAPICore.Model.Category> Category { get; set; }
    }
}
