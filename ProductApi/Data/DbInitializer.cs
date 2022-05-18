using System.Collections.Generic;
using System.Linq;
using ProductApi.Models;

namespace ProductApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(ProductApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            List<BEProduct> products = new List<BEProduct>
            {
                new BEProduct { Name = "Hammer", Price = 100, ItemsInStock = 10, ItemsReserved = 0 },
                new BEProduct { Name = "Screwdriver", Price = 70, ItemsInStock = 20, ItemsReserved = 0 },
                new BEProduct { Name = "Drill", Price = 500, ItemsInStock = 2, ItemsReserved = 0 }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
