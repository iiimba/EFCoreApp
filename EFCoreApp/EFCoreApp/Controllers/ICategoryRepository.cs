using System.Collections.Generic;

namespace EFCoreApp.Controllers
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }

        Category GetCategory(long id);

        void AddCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);
    }
}
