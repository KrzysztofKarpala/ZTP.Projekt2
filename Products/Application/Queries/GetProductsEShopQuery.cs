using Mapster;
using MediatR;
using Products.Application.Dto;
using Products.Core.Repositories;

namespace Products.Application.Queries
{
    public record GetProductsEShopQuery() : IRequest<List<ProductEShopResponseDto>>
    {
    }
    public class GetProductsEShopQueryHandler : IRequestHandler<GetProductsEShopQuery, List<ProductEShopResponseDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductsEShopQueryHandler> _logger;
        public GetProductsEShopQueryHandler(ILogger<GetProductsEShopQueryHandler> logger, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<List<ProductEShopResponseDto>> Handle(GetProductsEShopQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepository.GetProducts();
                var productsDto = products.Adapt<List<ProductEShopResponseDto>>();
                Random rand = new Random();
                foreach (var product in productsDto )
                {
                    product.UnitPrice = rand.Next(0, 100);
                }
                return productsDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(500, ex, "GetProductsQueryHandler failed");
                throw ex;
            }
        }
    }
}
