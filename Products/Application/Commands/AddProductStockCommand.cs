using MediatR;
using Products.Core.Entities;
using Products.Core.Repositories;

namespace Products.Application.Commands
{
    public record AddProductQuantityCommand(Guid productId, int productQuantity) : IRequest
    {
    }
    public class AddProductQuantityCommandHandler : IRequestHandler<AddProductQuantityCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<AddProductQuantityCommandHandler> _logger;
        public AddProductQuantityCommandHandler(ILogger<AddProductQuantityCommandHandler> logger, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task Handle(AddProductQuantityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetProductByProductId(request.productId);
                if (product == null)
                {
                    throw new FileNotFoundException($"Product: {request.productId} does not exist.");
                }
                product.AddQuantity(request.productQuantity);
                await _productRepository.AddOrReplaceProduct(product);
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogWarning(404, ex, $"Product: {request.productId} does not exist.");
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(500, ex, "AddProductQuantityCommandHandler failed");
                throw ex;
            }
        }
    }
}
