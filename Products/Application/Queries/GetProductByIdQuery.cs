using Mapster;
using MediatR;
using Products.Application.Dto;
using Products.Core.Entities;
using Products.Core.Repositories;

namespace Products.Application.Queries
{
    public record GetProductByIdQuery(Guid productId) : IRequest<ProductResponseDto>
    {
    }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;
        public GetProductByIdQueryHandler(ILogger<GetProductByIdQueryHandler> logger, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetProductByProductId(request.productId);
                if (product == null)
                {
                    throw new FileNotFoundException($"Product: {request.productId} does not exist.");
                }
                return product.Adapt<ProductResponseDto>();
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogWarning(404, ex, $"Product: {request.productId} does not exist.");
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(500, ex, "GetProductByIdQueryHandler failed");
                throw ex;
            }
        }
    }
}
