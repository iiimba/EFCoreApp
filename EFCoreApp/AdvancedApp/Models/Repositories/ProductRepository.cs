using AdvancedApp.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdvancedApp.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AdvancedContext context;

        public ProductRepository(AdvancedContext context)
        {
            this.context = context;
        }

        public async Task<Product[]> GetProductsAsync()
        {
            var products = await context.Products.Include(p => p.Category).ToArrayAsync();

            return products;
        }

        public async Task<Product> GetProductAsync(long id)
        {
            var product = await context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            if (product.Id != default(long))
            {
                return false;
            }

            context.Products.Add(product);

            var affectedRows = await context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            if(product.Id == default(long))
            {
                return false;
            }

            context.Products.Update(product);

            var affectedRows = await context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            if (product.Id == default(long))
            {
                return false;
            }

            context.Products.Remove(product);

            var affectedRows = await context.SaveChangesAsync();

            return affectedRows > 0;
        }
    }
}
