using AdvancedApp.Models;
using AdvancedApp.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvancedApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecondaryIdentityController : ControllerBase
    {
        private readonly ISecondaryIdentityRepository repository;

        public SecondaryIdentityController(ISecondaryIdentityRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("InsertOrUpdate")]
        public async Task<IActionResult> InsertOrUpdateAsync(SecondaryIdentity identity)
        {
            var updated = await repository.InsertOrUpdateAsync(identity);

            return Ok(updated);
        }

        [HttpPost("InsertOrUpdate/Range")]
        public async Task<IActionResult> InsertOrUpdateRangeAsync(SecondaryIdentity[] identities)
        {
            var updated = await repository.InsertOrUpdateRangeAsync(identities);

            return Ok(updated);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(SecondaryIdentity identity)
        {
            var updated = await repository.UpdateAsync(identity);

            return Ok(updated);
        }
    }
}
