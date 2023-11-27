namespace Products.Core.Entities
{
    public class ProductHistory
    {
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public int ProductQuantity { get; private set; }
        public DateTime ProductHistoryCreationDate { get; private set; }
        private ProductHistory(string productName, string productDescription, int productQuantity, DateTime dateTime)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            ProductQuantity = productQuantity;
            ProductHistoryCreationDate = dateTime;
        }
        public static ProductHistory Create(string productName, string productDescription, int productQuantity)
        {
            var productHistory = new ProductHistory(productName, productDescription, productQuantity, DateTime.UtcNow);
            return productHistory;
        }
    }
}
