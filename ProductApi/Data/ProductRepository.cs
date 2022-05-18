using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProductApi.Models;

namespace ProductApi.Data
{
    public class ProductRepository : IRepository<BEProduct>
    {
        private readonly ProductApiContext db;

        public ProductRepository(ProductApiContext context)
        {
            db = context;
        }

        BEProduct IRepository<BEProduct>.Add(BEProduct entity)
        {
            var newProduct = db.Products.Add(entity).Entity;
            db.SaveChanges();
            return newProduct;
        }

        void IRepository<BEProduct>.Edit(BEProduct entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        BEProduct IRepository<BEProduct>.Get(int id)
        {
            return db.Products.FirstOrDefault(p => p.Id == id);
        }

        IEnumerable<BEProduct> IRepository<BEProduct>.GetAll()
        {
            return db.Products.ToList();
        }

        void IRepository<BEProduct>.Remove(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == id);
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }
}
