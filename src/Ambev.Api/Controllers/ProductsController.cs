using Ambev.Domain.Features.Products.Commands.CreateProduct;
using Ambev.Domain.Features.Products.Commands.DeleteProduct;
using Ambev.Domain.Features.Products.Commands.UpdateProduct;
using Ambev.Domain.Features.Products.Queries.GetProduct;
using Ambev.Domain.Features.Products.Queries.GetProductWithPagination;
using Ambev.Shared.Common.Http;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.Api.Controllers
{
    /// <summary>
    /// Controller for Product
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// constructor of ProductsController
        /// </summary>
        /// <param name="mediator"></param>
        public ProductsController(IMediator mediator)
        {
            _mediator=mediator;
        }

        /// <summary>
        /// Retrieve a list of all products
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithPagination<IReadOnlyCollection<GetProductResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(new ApiResponseWithPagination<IReadOnlyCollection<GetProductResponse>>
            {
                Data = response.Items,
                TotalItems=response.TotalItems,
                CurrentPage=response.CurrentPage,
                TotalPages=response.TotalPages,
                HasNextPage=response.HasNextPage,
                HasPreviousPage=response.HasPreviousPage
            });
        }

        /// <summary>
        /// Retrieve a specific product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetProductCommand();
            request.Id = id;
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<GetProductResponse> 
            { 
                Data = response,
                Message = "Product retrived sucessfully"
            });
        }

        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status200OK)]

        public async Task<IActionResult> CreateProduc([FromBody] CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(new ApiResponseWithData<CreateProductResponse>
            {
                Data = response,
                Message = "Product created successfully"
            });
        }

        /// <summary>
        /// Update a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]

        public async Task<IActionResult> UpdateProduc([FromRoute] Guid id, [FromBody] UpdateProductCommand request, CancellationToken cancellationToken)
        {
            request.SetId(id);
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<UpdateProductResponse>
            {
                Data = response,
                Message = "Product updated successfully"
            });
        }
        /// <summary>
        /// Delete a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteProductCommand();
            request.Id = id;
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponse
            {
                Message = "Product deleted successfully"
            });
        }
    }
}
