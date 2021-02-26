using AdvancedApp.Models;

namespace AdvancedApp.DTOs
{
    public class ProductDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public long CategoryId { get; set; }

        public CategoryDTO Category { get; set; }

        public ProductDTO()
        {

        }

        public ProductDTO(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            CategoryId = product.CategoryId;

            Category = product.Category == null
                ? null
                : new CategoryDTO
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                    Description = product.Category.Description
                };
        }
    }
}
