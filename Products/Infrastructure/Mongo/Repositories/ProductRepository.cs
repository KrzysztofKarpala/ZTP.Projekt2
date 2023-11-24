using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Products.Core.Entities;
using Products.Core.Repositories;
using Products.Infrastructure.Mongo.DatabaseSettings;

namespace Products.Infrastructure.Mongo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductRepository(IOptions<ProductDatabaseSettings> productsDatabaseSettings)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(productsDatabaseSettings.Value.DBConnectionString);
            var mongoClient = new MongoClient(settings);
            var mongoDatabase = mongoClient.GetDatabase(productsDatabaseSettings.Value.DatabaseName);
            _productsCollection = mongoDatabase.GetCollection<Product>(productsDatabaseSettings.Value.ProductsCollectionName);
        }
        public async Task AddOrReplaceProduct(Product product)
        {
            await _productsCollection.ReplaceOneAsync(x => x.ProductId == product.ProductId, product, new ReplaceOptions()
            {
                IsUpsert = true
            });
        }

        public async Task DeleteProductById(Guid productId)
        {
            await _productsCollection.DeleteOneAsync(x => x.ProductId == productId);
        }

        public async Task<Product> GetProductByProductId(Guid productId)
        {
            return await _productsCollection.Find(x => x.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByProductName(string productName)
        {
            return await _productsCollection.Find(x => x.ProductName == productName).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _productsCollection.Find(x => true).ToListAsync();

        }
    }
}
