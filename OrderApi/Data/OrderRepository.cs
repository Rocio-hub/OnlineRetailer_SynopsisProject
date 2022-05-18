using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using OrderApi.Models;
using System;

namespace OrderApi.Data
{
    public class OrderRepository : IRepository<BEOrder>
    {
        private readonly OrderApiContext db;

        public OrderRepository(OrderApiContext context)
        {
            db = context;
        }

        BEOrder IRepository<BEOrder>.Add(BEOrder entity)
        {
            if (entity.Date == null)
                entity.Date = DateTime.Now;
            
            var newOrder = db.Orders.Add(entity).Entity;
            db.SaveChanges();
            return newOrder;
        }

        void IRepository<BEOrder>.Edit(BEOrder entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        BEOrder IRepository<BEOrder>.Get(int id)
        {
            return db.Orders.FirstOrDefault(o => o.Id == id);
        }

        IEnumerable<BEOrder> IRepository<BEOrder>.GetAll()
        {
            return db.Orders.ToList();
        }

        void IRepository<BEOrder>.Remove(int id)
        {
            var order = db.Orders.FirstOrDefault(p => p.Id == id);
            db.Orders.Remove(order);
            db.SaveChanges();
        }
    }
}
