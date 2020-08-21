using EFCoreApp.Models;
using EFCoreApp.Models.Pages;
using System.Linq;

namespace EFCoreApp.Models
{
    public interface IRepository
    {
        IQueryable<Product> Products { get; }

        PagedList<Product> GetProducts(QueryOptions options);

        Product GetProduct(long id);

        void AddProduct(Product product);

        void Update(Product product);

        void UpdateAll(Product[] products);

        void Delete(Product product);

        void CreateSeedData(int count);

        void CreateSeedData2();
    }
}
