using Microsoft.AspNetCore.Mvc;

namespace AdvancedApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
