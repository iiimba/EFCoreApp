using AdvancedApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace AdvancedApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MultiController : ControllerBase
    {
        private readonly AdvancedContext context;

        public MultiController(AdvancedContext context)
        {
            this.context = context;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertRangeAsync(Employee employee)
        {
            context.Add(employee);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateRangeAsync(Employee[] employees)
        {
            await context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            context.UpdateRange(employees);
            await context.SaveChangesAsync();

            if (await context.Employees.SumAsync(e => e.Salary) > 10000)
            {
                await context.Database.RollbackTransactionAsync();
            }
            else
            {
                await context.Database.CommitTransactionAsync();
            }

            return Ok();
        }
    }
}
