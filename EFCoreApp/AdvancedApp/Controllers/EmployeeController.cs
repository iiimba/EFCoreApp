using AdvancedApp.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvancedApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await repository.GetEmployeesAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> TestHiLoStrategy()
        {
            await repository.InsertNewEmployeeAsync();

            return Ok();
        }
    }
}
