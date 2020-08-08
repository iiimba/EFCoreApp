using System.Collections.Generic;
using System.Linq;

namespace EFCore.Controllers
{
    public class DataRepository : IRepository
    {
        private DataContext context;

        public DataRepository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> Products => this.context.Products.ToList();

        public Product GetProduct(long id)
        {
            var product = this.context.Products.Find(id);
            return product;
        }

        public void AddProduct(Product product)
        {
            this.context.Add(product);
            this.context.SaveChanges();
        }

        public void Update(Product product)
        {
            this.context.Products.Update(product);
            this.context.SaveChanges();
        }

        public void UpdateAll(Product[] products)
        {
            var productIds = products.Select(p => p.Id);
            var existingProducts = this.context.Products.Where(p => productIds.Contains(p.Id)).ToList();

            foreach (var itemexistingProduct in existingProducts)
            {
                var currentProduct = products.Single(p => p.Id == itemexistingProduct.Id);
                itemexistingProduct.Name = currentProduct.Name;
                itemexistingProduct.Category = currentProduct.Category;
                itemexistingProduct.PurchasePrice = currentProduct.PurchasePrice;
                itemexistingProduct.RetailPrice = currentProduct.RetailPrice;
            }

            this.context.SaveChanges();
        }

        public void Delete(Product product)
        {
            this.context.Products.Remove(product);
            this.context.SaveChanges();
        }
    }
}
