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
            var shoes = context.Shoe
                .Include(s => s.Stl)
                .Include(s => s.Width).Include(s => s.Categories)
                .ThenInclude(j => j.Category)
                .Select(s => new
                { 
                    s.Name,
                    s.Price,
                    s.Stl.StyleName,
                    s.Width.WidthName,
                    Categories = s.Categories.Select(c => new
                    { 
                        s.Name,
                        s.Price
                    })
                })
                .ToList();


            return Ok(shoes);
        }
    }
}
