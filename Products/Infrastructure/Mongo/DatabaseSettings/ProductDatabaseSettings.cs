namespace Products.Infrastructure.Mongo.DatabaseSettings
{
    public class ProductDatabaseSettings
    {
        public string DBConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ProductsCollectionName { get; set; } = null!;
    }
}
