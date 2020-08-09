using System.Collections.Generic;
using System.Linq;

namespace EFCore.Controllers
{
    public interface IRepository
    {
        IQueryable<Product> Products { get; }

        Product GetProduct(long id);

        void AddProduct(Product product);

        void Update(Product product);

        void UpdateAll(Product[] products);

        void Delete(Product product);
    }
}
