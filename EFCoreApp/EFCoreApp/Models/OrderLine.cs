using EFCore.Controllers;

namespace EFCoreApp.Models
{
    public class OrderLine
    {
        public long Id { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }

        public long ProductId { get; set; }

        public Order Order { get; set; }

        public long OrderId { get; set; }
    }
}
