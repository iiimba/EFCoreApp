using EFCore.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreApp.Controllers
{
    public class OrdersRepository : IOrdersRepository
    {
        private DataContext context;

        public OrdersRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<Order> Orders => this.context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);

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
