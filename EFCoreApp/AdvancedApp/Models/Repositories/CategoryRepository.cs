using AdvancedApp.DTOs;
using AdvancedApp.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedApp.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AdvancedContext context;

        public CategoryRepository(AdvancedContext context)
        {
            this.context = context;
        }

        public async Task<CategoryDTO[]> GetCategoriesAsync()
        {
            var categories = await context.Categories.Include(c => c.Products).ToArrayAsync();

            return categories.Select(c => new CategoryDTO(c)).ToArray();
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            if (category.Id == default(long))
            {
                return false;
            }

            context.Categories.Update(category);

            var affectedRows = await context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            if (category.Id == default(long))
            {
                return false;
            }

            context.Categories.Remove(category);

            var affectedRows = await context.SaveChangesAsync();

            return affectedRows > 0;
        }
    }
}
