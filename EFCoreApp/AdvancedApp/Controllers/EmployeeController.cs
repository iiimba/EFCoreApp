using AdvancedApp.DTOs;
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

        [HttpGet("{searchTerm}")]
        public async Task<IActionResult> GetEmployeesBySearchTermAsync(string searchTerm)
        {
            var employees = await repository.GetEmployeesBySearchTermAsync(searchTerm);

            return Ok(employees);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetEmployeesIncludeDeletedAsync()
        {
            var employees = await repository.GetEmployeesIncludeDeletedAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> TestHiLoStrategyAsync()
        {
            await repository.InsertNewEmployeeAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(EmployeeDTO employeeDTO)
        {
            var updated = await repository.UpdateAsync(employeeDTO);

            return Ok(updated);
        }

        [HttpDelete("{ssn}")]
        public async Task<IActionResult> SoftDeleteByIdAsync(string ssn)
        {
            var isSoftDeleted = await repository.SoftDeleteByIdAsync(ssn);

            return Ok(isSoftDeleted);
        }
    }
}
