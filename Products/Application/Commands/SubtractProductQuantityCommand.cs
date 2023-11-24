using MediatR;
using Products.Core.Repositories;

namespace Products.Application.Commands
{
    public record SubtractProductQuantityCommand(Guid productId, int productQuantity) : IRequest
    {
    }
    public class SubtractProductQuantityCommandHandler : IRequestHandler<SubtractProductQuantityCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<SubtractProductQuantityCommandHandler> _logger;
        public SubtractProductQuantityCommandHandler(ILogger<SubtractProductQuantityCommandHandler> logger, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task Handle(SubtractProductQuantityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetProductByProductId(request.productId);
                if (product == null)
                {
                    throw new FileNotFoundException($"Product: {request.productId} does not exist.");
                }
                product.SubtractQuantity(request.productQuantity);
                await _productRepository.AddOrReplaceProduct(product);
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogWarning(404, ex, $"Product: {request.productId} does not exist.");
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(500, ex, "SubtractProductQuantityCommandHandler failed");
                throw ex;
            }
        }
    }
}
