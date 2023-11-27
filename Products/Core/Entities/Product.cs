using Microsoft.AspNetCore.Http.HttpResults;
using Products.Shared;

namespace Products.Core.Entities
{
    public class Product : AggregateRoot
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public int ProductQuantity { get; private set; }
        public List<ProductHistory> ProductHistories { get; private set; }
        private Product(Guid productId, string productName, string productDescription, int productQuantity, List<ProductHistory> productHistories)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductQuantity = productQuantity;
            ProductHistories = productHistories;

        }
        public static Product Create(string productName, string productDescription, int productQuantity)
        {
            if (productQuantity < 0)
            {
                throw new ApplicationException($"Quantity cannot be less than 0.");
            }
            var productId = Guid.NewGuid();
            var productHistories = new List<ProductHistory>();
            var product = new Product(productId, productName, productDescription, productQuantity, productHistories);
            product.SetCreationDate();
            product.SetModyficationDate();
            product.IncrementVersion();
            return product;
        }
        public void UpdateProduct(string productName, string productDescription, int productQuantity)
        {
            if (productQuantity < 0)
            {
                throw new ApplicationException($"Quantity cannot be less than 0.");
            }
            AddProductHistory();
            ProductName = productName;
            ProductDescription = productDescription;
            ProductQuantity = productQuantity;
            SetModyficationDate();
            IncrementVersion();
        }
        public void AddQuantity(int productQuantity)
        {
            AddProductHistory();
            ProductQuantity = ProductQuantity + productQuantity;
        }
        public void SubtractQuantity(int productQuantity)
        {
            AddProductHistory();
            ProductQuantity = ProductQuantity - productQuantity;
            if(ProductQuantity < 0)
            {
                throw new ApplicationException($"Product: {ProductId}, quantity cannot be less than 0.");
            }
        }
        public void AddProductHistory()
        {
            var productHistory = ProductHistory.Create(ProductName, ProductDescription, ProductQuantity);
            ProductHistories.Add(productHistory);
        }
    }
}
