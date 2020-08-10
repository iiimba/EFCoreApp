using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EFCoreApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrdersRepository repository;

        public OrderController(IOrdersRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = this.repository.Orders.Select(o => new 
            { 
                o.Id, 
                o.CustomerName, 
                o.Address, 
                o.State, 
                o.ZipCode, 
                o.Shipped,
                Lines = o.Lines.Select(l => new 
                {
                    LineId = l.Id,
                    l.Quantity,
                    ProductId = l.Product.Id,
                    l.Product.Name,
                    l.Product.PurchasePrice,
                    l.Product.RetailPrice
                })
            });

            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateOrUpdateOrder(Order order)
        {
            order.Lines = order.Lines.Where(l => l.Id > default(long) || (l.Id == default && l.Quantity > default(long))).ToList();

            if (order.Id == default)
            {
                this.repository.AddOrder(order);
            }
            else
            {
                this.repository.UpdateOrder(order);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var order = this.repository.GetOrder(id);
            if (order == null)
            {
                return BadRequest();
            }

            this.repository.DeleteOrder(order);
            return Ok();
        }
    }
}
