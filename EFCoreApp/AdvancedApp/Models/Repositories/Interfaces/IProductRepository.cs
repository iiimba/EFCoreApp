using AdvancedApp.DTOs;
using System.Threading.Tasks;

namespace AdvancedApp.Models.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDTO[]> GetProductsAsync();

        Task<ProductDTO> GetProductAsync(long id);

        Task<bool> CreateProductAsync(Product product);

        Task<bool> UpdateProductAsync(Product product);

        Task<bool> DeleteProductAsync(Product product);
    }
}
