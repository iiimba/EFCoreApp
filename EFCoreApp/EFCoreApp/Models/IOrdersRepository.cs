using EFCoreApp.Models.Pages;
using System.Linq;

namespace EFCoreApp.Models
{
    public interface IOrdersRepository
    {
        IQueryable<Order> Orders { get; }

        PagedList<Order> GetOrders(QueryOptions options);

        Order GetOrder(long id);

        void AddOrder(Order order);

        void UpdateOrder(Order order);

        void DeleteOrder(Order order);
    }
}
