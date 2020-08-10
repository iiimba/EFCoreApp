using EFCoreApp.Models;
using EFCoreApp.Models.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCore.Models
{
    public class DataRepository : IRepository
    {
        private DataContext context;

        public DataRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<Product> Products => this.context.Products.Include(p => p.Category);

        public PagedList<Product> GetProducts(QueryOptions options)
        {
            return new PagedList<Product>(this.context.Products.Include(p => p.Category), options);
        }

        public Product GetProduct(long id)
        {
            var product = this.context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
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
                itemexistingProduct.CategoryId = currentProduct.CategoryId;
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

        public void CreateSeedData(int count)
        {
            this.ClearData();
            if (count > 0)
            {
                this.context.Database.SetCommandTimeout(TimeSpan.FromMilliseconds(10));
                this.context.Database.ExecuteSqlRaw("DROP PROCEDURE IF EXISTS CreateSeedData");
                this.context.Database.ExecuteSqlRaw($@"CREATE PROCEDURE CreateSeedData
                    @RowCount decimal
                    AS
                    BEGIN
                        SET NOCOUNT ON
                        DECLARE @i INT = 1;
                        DECLARE @catId BIGINT;
                        DECLARE @CatCount INT = @RowCount / 10;
                        DECLARE @pprice DECIMAL(5, 2);
                        DECLARE @rprice DECIMAL(5, 2);
                        BEGIN TRANSACTION
                            WHILE @i <= @CatCount
                            BEGIN
                                INSERT INTO Categories (Name, Description)
                                VALUES (CONCAT('Category-', @i), 'Test Data Category');

                                SET @catId = SCOPE_IDENTITY();
                                DECLARE @j INT = 1;
                                WHILE @j <= 10
                                BEGIN
                                    SET @pprice = RAND() * (500 - 5 + 1);
                                    SET @rprice = (RAND() * @pprice) + @pprice;

                                    INSERT INTO Products (Name, Categoryid,  PurchasePrice, RetailPrice)
                                    VALUES (CONCAT('Product', @i, '-', @j), @catId, @pprice, @rprice)

                                    SET @j = @j + 1
                                    SET @i = @i + 1
                                END
                            END
                        COMMIT
                    END");
                this.context.Database.BeginTransaction();
                this.context.Database.ExecuteSqlRaw($"EXEC CreateSeedData @RowCount = {count}");
                this.context.Database.CommitTransaction();
            }
        }

        private void ClearData()
        {
            this.context.Database.SetCommandTimeout(TimeSpan.FromMilliseconds(10));
            this.context.Database.BeginTransaction();
            this.context.Database.ExecuteSqlRaw("DELETE FROM Orders");
            this.context.Database.ExecuteSqlRaw("DELETE FROM Categories");
            this.context.Database.CommitTransaction();
        }
    }
}
