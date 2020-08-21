using System.Collections.Generic;
using System.Linq;

namespace EFCoreApp.Models
{
    public class Cart
    {
        private List<OrderLine> selections = new List<OrderLine>();

        public Cart AddItem(Product p, int quantity)
        {
            var line = selections.Where(l => l.ProductId == p.Id).FirstOrDefault();
            if (line != null)
            {
                line.Quantity += quantity;
            }
            else
            {
                selections.Add(new OrderLine
                {
                    ProductId = p.Id,
                    Product = p,
                    Quantity = quantity
                });
            }
            return this;
        }

        public Cart Removeitem(long productId)
        {
            selections.RemoveAll(l => l.ProductId == productId);
            return this;
        }
        public void Clear() => selections.Clear();

        public IEnumerable<OrderLine> Selections { get => selections; }
    }
}
