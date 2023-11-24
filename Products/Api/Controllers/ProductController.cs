using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Commands;
using Products.Application.Dto;
using Products.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    [ApiController]
    [SwaggerTag("Contains products data.")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Returns a list of all products.
        /// </summary>
        /// <response code="200">Returns a list of all products.</response>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<List<ProductResponseDto>>> GetAllProducts()
        {
            try
            {
                var query = new GetProductsQuery();
                var result = await _mediator.Send(query);

                _logger.LogInformation(200, "Fetched all products");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        /// <summary>
        /// Returns product by id.
        /// </summary>
        /// <response code="200">Returns product by id.</response>
        /// <returns></returns>
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductResponseDto>> GetProductByProductId([FromRoute] Guid productId)
        {
            try
            {
                var query = new GetProductByIdQuery(productId);
                var result = await _mediator.Send(query);

                _logger.LogInformation(200, $"Fetched product: {productId}");
                return Ok(result);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult> AddProduct([FromBody] ProductDto product)
        {
            try
            {
                var command = new AddProductCommand(product);
                await _mediator.Send(command);
                _logger.LogInformation(200, $"Added product: {product.ProductName}");
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] Guid productId)
        {
            try
            {
                var command = new DeleteProductCommand(productId);
                await _mediator.Send(command);
                _logger.LogInformation(200, $"Deleted product: {productId}");
                return Ok();
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Add product quantity
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPut("{productId}/quantityadd/{quantity}")]
        public async Task<ActionResult> AddProductQuantity([FromRoute] Guid productId, int quantity)
        {
            try
            {
                var command = new AddProductQuantityCommand(productId, quantity);
                await _mediator.Send(command);
                _logger.LogInformation(200, $"Added product quantity: {quantity} to product: {productId}");
                return Ok();
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Subtract product quantity
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPut("{productId}/quantitysubtract/{quantity}")]
        public async Task<ActionResult> SubtractProductQuantity([FromRoute] Guid productId, int quantity)
        {
            try
            {
                var command = new SubtractProductQuantityCommand(productId, quantity);
                await _mediator.Send(command);
                _logger.LogInformation(200, $"Subtracted product quantity: {quantity} to product: {productId}");
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
