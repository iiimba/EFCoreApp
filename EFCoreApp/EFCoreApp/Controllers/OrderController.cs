using Microsoft.AspNetCore.Mvc;

namespace EFCoreApp.Controllers
{
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
            return Ok(this.repository.Orders);
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
