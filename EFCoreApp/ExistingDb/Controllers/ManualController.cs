using ExistingDb.Models.Manual;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExistingDb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManualController : ControllerBase
    {
        private ManualContext context;

        public ManualController(ManualContext context) => this.context = context;
        [HttpGet]
        public IActionResult Get()
        {
            var shoes = this.context.Shoes.ToList();
            var colors = this.context.ShoeStyles.ToList();

            return Ok();
        }
    }
}
