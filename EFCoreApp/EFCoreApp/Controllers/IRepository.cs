using System.Collections.Generic;

namespace EFCore.Controllers
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }

        Product GetProduct(long id);

        void AddProduct(Product product);

        void Update(Product product);

        void UpdateAll(Product[] products);

        void Delete(Product product);
    }
}
