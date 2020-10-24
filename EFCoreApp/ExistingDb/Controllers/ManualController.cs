using ExistingDb.Models.Manual;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var shoes = this.context.Shoes
                .Include(s => s.Style)
                .Include(s => s.Width)
                .Include(s => s.Categories)
                .ThenInclude(j => j.Category)
                .ToList();

            return Ok();
        }
    }
}
