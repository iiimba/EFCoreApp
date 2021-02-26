using AdvancedApp.Models;
using System.Linq;

namespace AdvancedApp.DTOs
{
    public class CategoryDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ProductDTO[] Products { get; set; }

        public CategoryDTO()
        {

        }

        public CategoryDTO(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Description = category.Description;

            Products = category.Products == null || !category.Products.Any()
                ? null
                : category.Products.Select(p => new ProductDTO 
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryId = p.CategoryId
                }).ToArray();
        }
    }
}
