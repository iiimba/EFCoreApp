using AdvancedApp.DTOs;
using System.Threading.Tasks;

namespace AdvancedApp.Models.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryDTO[]> GetCategoriesAsync();

        Task<bool> UpdateCategoryAsync(Category category);

        Task<bool> DeleteCategoryAsync(Category category);
    }
}
