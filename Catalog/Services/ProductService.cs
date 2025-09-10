using MassTransit;

namespace Catalog.Services
{
    public class ProductService(ProductDbContext dbContext,IBus bus)
    {
        public async Task<IEnumerable<Product>> GetProductsAsync() => await dbContext.Products.ToListAsync();
        public async Task<Product?> GetProductsByIdAsync(int id) => await dbContext.Products.FindAsync(id);
        public async Task CreateProductAsync (Product product)
        {
            dbContext.Products.Add (product);
            await dbContext.SaveChangesAsync ();
        }

        public async Task UpdateProductAsync(Product updatedProduct, Product inputProduct)
        {
            if(updatedProduct.Price != inputProduct.Price)
            {
                // Publish event to the bus if the price has changed
                await bus.Publish(new ProductPriceChangedIntegrationEvent
                {
                    ProductId = updatedProduct.Id,
                    Name = inputProduct.Name,
                    Description = inputProduct.Description,
                    Price = inputProduct.Price,
                    ImageUrl = inputProduct.ImageUrl
                });
            }

            updatedProduct.Name = inputProduct.Name;
            updatedProduct.Description = inputProduct.Description;
            updatedProduct.Price = inputProduct.Price;
            updatedProduct.ImageUrl = inputProduct.ImageUrl;

            dbContext.Products.Update (updatedProduct);
            await dbContext.SaveChangesAsync ();
        }

        public async Task DeleteProductAsync(Product deletedProduct)
        {
            dbContext.Products.Remove(deletedProduct);
            await dbContext.SaveChangesAsync ();
        }

        public async Task<IEnumerable<Product>> ProductExists(string query) => await dbContext.Products.Where(e => e.Name.Contains(query)).ToListAsync();
    }
}
