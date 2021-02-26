using System.Threading.Tasks;

namespace AdvancedApp.Models.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product[]> GetProductsAsync();

        Task<Product> GetProductAsync(long id);

        Task<bool> CreateProductAsync(Product product);

        Task<bool> UpdateProductAsync(Product product);

        Task<bool> DeleteProductAsync(Product product);
    }
}
