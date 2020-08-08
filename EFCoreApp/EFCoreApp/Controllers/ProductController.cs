using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public IActionResult CreateProducts(Product product)
        {
            this.repository.AddProduct(product);

            return Ok();
        }
    }
}
