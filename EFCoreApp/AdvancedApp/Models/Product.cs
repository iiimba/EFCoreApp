namespace AdvancedApp.Models
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public long CategoryId { get; set; }

        public Category Category { get; set; }

        public bool SoftDeleted { get; set; }
    }
}
