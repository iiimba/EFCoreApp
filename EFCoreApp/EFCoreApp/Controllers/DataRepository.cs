using System.Collections.Generic;
using System.Linq;

namespace EFCore.Controllers
{
    public class DataRepository : IRepository
    {
        private DataContext context;

        public DataRepository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> Products => this.context.Products.ToList();

        public void AddProduct(Product product)
        {
            this.context.Add(product);
            this.context.SaveChanges();
        }
    }
}
