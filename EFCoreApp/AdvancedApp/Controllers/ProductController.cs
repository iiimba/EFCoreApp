using AdvancedApp.Models;
using AdvancedApp.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvancedApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var products = await repository.GetProductsAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var product = await repository.GetProductAsync(id);

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(Product product)
        {
            var updated = await repository.CreateProductAsync(product);

            return Ok(updated);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync(Product product)
        {
            var updated = await repository.UpdateProductAsync(product);

            return Ok(updated);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductAsync(Product product)
        {
            var deleted = await repository.DeleteProductAsync(product);

            return Ok(deleted);
        }
    }
}
