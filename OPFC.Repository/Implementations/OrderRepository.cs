﻿using System;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace OPFC.Repositories.Implementations
{
    public class OrderRepository : EFRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext dbContext) : base(dbContext) { }

        public Models.Order CreateOrder(Order order)
        {
            return DbSet.Add(order).Entity;
        }

        public bool DeleteOrder(Order order)
        {
            if (DbSet.SingleOrDefault() != null)
            {
                DbSet.Remove(order);
                return true;
            }
            return false;
        }

        public List<Order> GetAllOrder()
        {
            return DbSet.ToList();
        }

        public Order GetByEventId(long eventId)
        {
            return DbSet.SingleOrDefault(o => o.EventId == eventId);
        }

        public Order GetOrderById(long id)
        {
            return DbSet.SingleOrDefault(o => o.OrderId == id);

        }

        public Models.Order UpdateOrder(Order order)
        {
            return DbSet.Update(order).Entity;
        }
    }
}
