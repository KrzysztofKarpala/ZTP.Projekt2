using Products.Core.Entities;

namespace Products.Core.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductByProductId(Guid productId);
        Task<Product> GetProductByProductName(string productName);
        Task AddOrReplaceProduct(Product product);
        Task DeleteProductById(Guid productId);
    }
}
