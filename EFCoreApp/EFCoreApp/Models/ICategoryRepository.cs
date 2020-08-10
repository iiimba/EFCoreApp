using EFCoreApp.Models.Pages;
using System.Linq;

namespace EFCoreApp.Models
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        PagedList<Category> GetCategories(QueryOptions options);

        Category GetCategory(long id);

        void AddCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);
    }
}
