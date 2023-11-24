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
        private Product(Guid productId, string productName, string productDescription, int productQuantity)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductQuantity = productQuantity;
        }
        public static Product Create(string productName, string productDescription, int productQuantity)
        {
            if (productQuantity < 0)
            {
                throw new ApplicationException($"Quantity cannot be less than 0.");
            }
            var productId = Guid.NewGuid();
            var product = new Product(productId, productName, productDescription, productQuantity);
            product.SetCreationDate();
            product.SetModyficationDate();
            product.IncrementVersion();
            return product;
        }
        public void AddQuantity(int productQuantity)
        {
            ProductQuantity = ProductQuantity + productQuantity;
        }
        public void SubtractQuantity(int productQuantity)
        {
            ProductQuantity = ProductQuantity - productQuantity;
            if(ProductQuantity < 0)
            {
                throw new ApplicationException($"Product: {ProductId}, quantity cannot be less than 0.");
            }
        }
    }
}
