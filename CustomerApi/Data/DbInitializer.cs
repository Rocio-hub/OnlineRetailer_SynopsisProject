
using CustomerApi.Models;
using System.Collections.Generic;

namespace CustomerApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(CustomerApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            BECustomer cust = new BECustomer();
            cust.Name = "TestName";
            cust.Phone = "123456789";
            cust.ShippingAddress = "Test Shipping Address";
            cust.BillingAddress = "Test Billing Address";
            cust.Email = "Test@Email.com";
            cust.CreditStanding = 5000.0;

            context.Add(cust);
            context.SaveChanges();
        }
    }
}
