using EFCore.Models;
using EFCoreApp.Models;
using EFCoreApp.Models.Pages;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EFCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var products = this.repository.Products.Select(p => new { p.Id, p.Name, p.PurchasePrice, p.RetailPrice, CategoryName = p.Category.Name }).Take(20);
            return Ok(products);
        }

        [HttpGet("ByPage")]
        public IActionResult GetProductsByPage([FromQuery]QueryOptions options)
        {
            var products = this.repository.GetProducts(options);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(long id)
        {
            if (id == default)
            {
                return BadRequest();
            }

            var product = this.repository.GetProduct(id);
            return Ok(new { product.Id, product.Name, product.PurchasePrice, product.RetailPrice, CategoryName = product.Category.Name });
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

        [HttpPost("Seed")]
        public IActionResult CreateSeedData(int count = 50000)
        {
            this.repository.CreateSeedData(count);
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
            //existingProduct.Category = product.Category;
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
