using ExistingDb.Models.Manual;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ExistingDb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManualController : ControllerBase
    {
        private ManualContext context;

        public ManualController(ManualContext context) => this.context = context;

        [HttpGet("Shoes")]
        public async Task<IActionResult> GetShoes()
        {
            var shoes = await context.Shoe
                .Include(s => s.Style)
                .Include(s => s.Width).Include(s => s.Categories)
                .ThenInclude(j => j.Category)
                .Select(s => new
                { 
                    s.Name,
                    s.Price,
                    s.Style.StyleName,
                    s.Width.WidthName,
                    Categories = s.Categories.Select(c => new
                    { 
                        s.Name,
                        s.Price
                    })
                })
                .ToListAsync();

            return Ok(shoes);
        }

        [HttpGet("Styles")]
        public async Task<IActionResult> GetStyles(int id = 1)
        {
            var styles = await context.ShoeStyles
                .Include(ss => ss.Products)
                .Where(ss => ss.UniqueIdent == id)
                .Select(ss => new
                {
                    ss.UniqueIdent,
                    ss.StyleName,
                    Shoes = ss.Products.Select(p => new
                    {
                        p.Id,
                        p.Name
                    })
                })
                .ToListAsync();

            return Ok(styles);
        }
    }
}
