using Mapster;
using MediatR;
using Products.Application.Dto;
using Products.Core.Entities;
using Products.Core.Repositories;

namespace Products.Application.Commands
{
    public record UpdateProductCommand(Guid productId, ProductDto product) : IRequest<ProductResponseDto>
    {
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        public UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ProductResponseDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetProductByProductId(request.productId);
                if(product == null)
                {
                    throw new FileNotFoundException($"Product with id: {request.productId} does not exist");
                }
                product.UpdateProduct(request.product.ProductName, request.product.ProductDescription, request.product.ProductQuantity);
                await _productRepository.AddOrReplaceProduct(product);
                return product.Adapt<ProductResponseDto>();
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(500, ex, "UpdateProductCommandHandler failed");
                throw ex;
            }
        }
    }
}
