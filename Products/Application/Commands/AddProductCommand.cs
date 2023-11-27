using Mapster;
using MediatR;
using Products.Application.Dto;
using Products.Core.Entities;
using Products.Core.Repositories;

namespace Products.Application.Commands
{
    public record AddProductCommand(ProductDto product) : IRequest<ProductResponseDto>
    {
    }
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<AddProductCommandHandler> _logger;
        public AddProductCommandHandler(ILogger<AddProductCommandHandler> logger, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ProductResponseDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = Product.Create(request.product.ProductName, request.product.ProductDescription, request.product.ProductQuantity);
                await _productRepository.AddOrReplaceProduct(product);
                return product.Adapt<ProductResponseDto>();
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(500, ex, "AddProductCommandHandler failed");
                throw ex;
            }
        }
    }
}
