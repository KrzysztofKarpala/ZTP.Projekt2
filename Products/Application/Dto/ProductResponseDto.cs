namespace Products.Application.Dto
{
    public class ProductResponseDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductQuantity { get; set; }
    }
}
