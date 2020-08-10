using System.Collections.Generic;
using System.Linq;

namespace EFCoreApp.Models
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        Category GetCategory(long id);

        void AddCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);
    }
}
