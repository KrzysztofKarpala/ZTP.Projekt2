using Mapster;
using MediatR;
using Products.Application.Dto;
using Products.Core.Repositories;

namespace Products.Application.Queries
{
    public record GetProductEShopByIdQuery(Guid productId) : IRequest<ProductEShopResponseDto>
    {
    }
    public class GetProductEShopByIdQueryHandler : IRequestHandler<GetProductEShopByIdQuery, ProductEShopResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductEShopByIdQueryHandler> _logger;
        public GetProductEShopByIdQueryHandler(ILogger<GetProductEShopByIdQueryHandler> logger, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ProductEShopResponseDto> Handle(GetProductEShopByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetProductByProductId(request.productId);
                var productDto = product.Adapt<ProductEShopResponseDto>();
                Random rand = new Random();
                productDto.UnitPrice = rand.Next(0, 100);
                return productDto;
            }
            catch(FileNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(500, ex, "GetProductEShopByIdQueryHandler failed");
                throw ex;
            }
        }
    }
}
