using System.Collections.Generic;
using System.Linq;
using OrderApi.Models;
using System;

namespace OrderApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(OrderApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            BEOrder order = new BEOrder();
            order.Date = DateTime.Now;
            order.ProductId = 4;
            order.Quantity = 3;
            context.Add(order);

            context.SaveChanges();
        }
    }
}
