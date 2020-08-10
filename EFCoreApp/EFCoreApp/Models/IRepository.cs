using EFCoreApp.Models;
using System.Linq;

namespace EFCoreApp.Models
{
    public interface IRepository
    {
        IQueryable<Product> Products { get; }

        Product GetProduct(long id);

        void AddProduct(Product product);

        void Update(Product product);

        void UpdateAll(Product[] products);

        void Delete(Product product);

        void CreateSeedData(int count);
    }
}
