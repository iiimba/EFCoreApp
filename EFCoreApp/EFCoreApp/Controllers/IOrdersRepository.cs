using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreApp.Controllers
{
    public interface IOrdersRepository
    {
        IQueryable<Order> Orders { get; }

        Order GetOrder(long id);

        void AddOrder(Order order);

        void UpdateOrder(Order order);

        void DeleteOrder(Order order);
    }
}
