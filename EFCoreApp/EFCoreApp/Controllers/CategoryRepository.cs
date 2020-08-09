using EFCore.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreApp.Controllers
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext context;

        public CategoryRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<Category> Categories => this.context.Categories;

        public Category GetCategory(long id)
        {
            var category = this.context.Categories.Find(id);
            return category;
        }

        public void AddCategory(Category category)
        {
            this.context.Categories.Add(category);
            this.context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            this.context.Categories.Update(category);
            this.context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            this.context.Categories.Remove(category);
            this.context.SaveChanges();
        }
    }
}
