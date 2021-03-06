﻿using EFCore.Models;
using EFCoreApp.Models.Pages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFCoreApp.Models
{
    public class OrdersRepository : IOrdersRepository
    {
        private DataContext context;

        public OrdersRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<Order> Orders => this.context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);

        public PagedList<Order> GetOrders(QueryOptions options)
        {
            return new PagedList<Order>(this.context.Orders.Include(p => p.Lines), options);
        }

        public PagedList<Order> GetProducts(QueryOptions options)
        {
            return new PagedList<Order>(this.context.Orders.Include(p => p.Lines), options);
        }

        public void AddOrder(Order order)
        {
            this.context.Orders.Add(order);
            this.context.SaveChanges();
        }

        public Order GetOrder(long id)
        {
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);
            return order;
        }

        public void UpdateOrder(Order order)
        {
            this.context.Orders.Update(order);
            this.context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            this.context.Orders.Remove(order);
            this.context.SaveChanges();
        }
    }
}
