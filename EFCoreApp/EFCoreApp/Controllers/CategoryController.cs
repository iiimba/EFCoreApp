using EFCoreApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository repository;

        public CategoryController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.repository.Categories);
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Id != default)
            {
                return BadRequest();
            }

            this.repository.AddCategory(category);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Category category)
        {
            var existingCategory = this.repository.GetCategory(category.Id);
            if (existingCategory == null)
            {
                return BadRequest();
            }

            // TODO: Temporary decision
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            this.repository.UpdateCategory(existingCategory);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var category = this.repository.GetCategory(id);
            if (category == null)
            {
                return BadRequest();
            }

            this.repository.DeleteCategory(category);
            return Ok();
        }
    }
}
