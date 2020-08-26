using ExistingDb.Models.Scaffold;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ExistingDb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoeValuesController : ControllerBase
    {
        private ScaffoldContext context;

        public ShoeValuesController(ScaffoldContext context) => this.context = context;

        [HttpGet]
        public IActionResult Get()
        {
            var shoes = this.context.Shoes
                .Include(s => s.Color)
                .Include(s => s.SalesCampaigns)
                .Include(s => s.ShoeCategoryJunction).ThenInclude(sc => sc.Category)
                .ToList();

            return Ok();
        }
    }
}
