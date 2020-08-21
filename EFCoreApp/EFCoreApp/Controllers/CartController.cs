using EFCoreApp.Infrastructure;
using EFCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EFCoreApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        IRepository productRepository;
        IOrdersRepository ordersRepository;

        public CartController(IRepository productRepository, IOrdersRepository ordersRepository)
        {
            this.productRepository = productRepository;
            this.ordersRepository = ordersRepository;
        }

        [HttpPost("Add")]
        public IActionResult AddToCart(Product product)
        {
            SaveCart(GetCart().AddItem(product, 1));
            return Ok();
        }

        [HttpPost("Remove")]
        public IActionResult RemoveFromCart(long productId)
        {
            SaveCart(GetCart().Removeitem(productId));
            return Ok();
        }

        [HttpPost("Create")]
        public IActionResult CreateOrde(Order order)
        {
            order.Lines = GetCart().Selections
                .Select(s => new OrderLine
                {
                    ProductId = s.ProductId,
                    Quantity = s.Quantity
                }).ToArray();

            ordersRepository.AddOrder(order);
            SaveCart(new Cart());
            return Ok();
        }

        private Cart GetCart() => HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();

        private void SaveCart(Cart cart) => HttpContext.Session.SetJson("Cart", cart);
    }
}
