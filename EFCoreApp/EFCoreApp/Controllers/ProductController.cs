using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EFCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IRepository repository;

        public ProductController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(this.repository.Products);
        }

        [HttpGet("Find/{id}")]
        public IActionResult GetProduct(long id)
        {
            if (id == default)
            {
                return BadRequest();
            }

            var product = this.repository.GetProduct(id);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProducts(Product product)
        {
            if (product.Id != default)
            {
                return BadRequest();
            }

            this.repository.AddProduct(product);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            var existingProduct = this.repository.GetProduct(product.Id);
            if (existingProduct == null)
            {
                return BadRequest();
            }

            // TODO: Temporary decision
            existingProduct.Category = product.Category;
            existingProduct.Name = product.Name;
            existingProduct.PurchasePrice = product.PurchasePrice;
            existingProduct.RetailPrice = product.RetailPrice;

            this.repository.Update(existingProduct);
            return Ok();
        }

        [HttpPut("BulkUpdate")]
        public IActionResult UpdateAll(Product[] products)
        {
            this.repository.UpdateAll(products);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var product = this.repository.GetProduct(id);
            if (product == null)
            {
                return BadRequest();
            }

            this.repository.Delete(product);
            return Ok();
        }
    }
}
