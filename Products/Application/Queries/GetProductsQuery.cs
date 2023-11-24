using Mapster;
using MediatR;
using Products.Application.Dto;
using Products.Core.Repositories;

namespace Products.Application.Queries
{
    public record GetProductsQuery() : IRequest<List<ProductResponseDto>>
    {
    }
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductResponseDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductsQueryHandler> _logger;
        public GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<List<ProductResponseDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepository.GetProducts();
                return products.Adapt<List<ProductResponseDto>>();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(500, ex, "GetProductsQueryHandler failed");
                throw ex;
            }
        }
    }
}
