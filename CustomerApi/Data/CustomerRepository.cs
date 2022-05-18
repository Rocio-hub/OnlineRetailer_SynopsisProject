using CustomerApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CustomerApi.Data
{
    public class CustomerRepository : IRepository<BECustomer>
    {
        private readonly CustomerApiContext db;

        public CustomerRepository(CustomerApiContext context)
        {
            db = context;
        }

        BECustomer IRepository<BECustomer>.Add(BECustomer entity)
        {
            var newCustomer = db.Customer.Add(entity).Entity;
            db.SaveChanges();
            return newCustomer;
        }

        void IRepository<BECustomer>.Edit(BECustomer entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        BECustomer IRepository<BECustomer>.Get(int id)
        {
            return db.Customer.FirstOrDefault(o => o.Id == id);
        }

        IEnumerable<BECustomer> IRepository<BECustomer>.GetAll()
        {
            return db.Customer.ToList();
        }

        void IRepository<BECustomer>.Remove(int id)
        {
            var customer = db.Customer.FirstOrDefault(p => p.Id == id);
            db.Customer.Remove(customer);
            db.SaveChanges();
        }
    }
}
