using AdvancedApp.Models;
using AdvancedApp.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvancedApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository repository;

        public CategoryController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var categories = await repository.GetCategoriesAsync();

            return Ok(categories);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategoryAsync(Category category)
        {
            var updated = await repository.UpdateCategoryAsync(category);

            return Ok(updated);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAsync(Category category)
        {
            var deleted = await repository.DeleteCategoryAsync(category);

            return Ok(deleted);
        }
    }
}
