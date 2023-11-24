using MediatR;
using Products.Core.Entities;
using Products.Core.Repositories;

namespace Products.Application.Commands
{
    public record DeleteProductCommand(Guid productId) : IRequest
    {
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        public DeleteProductCommandHandler(ILogger<DeleteProductCommandHandler> logger, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetProductByProductId(request.productId);
                if (product == null)
                {
                    throw new FileNotFoundException($"Product: {request.productId} does not exist.");
                }
                await _productRepository.DeleteProductById(request.productId);
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogWarning(404, ex, $"Product: {request.productId} does not exist.");
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(500, ex, "DeleteProductCommandHandler failed");
                throw ex;
            }
        }
    }
}
