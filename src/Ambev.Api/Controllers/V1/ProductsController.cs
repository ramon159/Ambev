using Ambev.Domain.Features.Products.Commands.CreateProduct;
using Ambev.Domain.Features.Products.Queries.GetProducts;
using Ambev.Shared.Common.Http;
using Ambev.Shared.Entities;
using Ardalis.GuardClauses;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator=mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduc([FromBody] CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(new ApiResponseWithData<CreateProductResponse>
            {
                Data = response,
                Message = "Product created successfully"
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery request, CancellationToken cancellationToken)
        {
   
            Guard.Against.NotFound(request.Order, request);

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(new ApiResponseWithPagination<IReadOnlyCollection<Product>>
            {
                Data = response.Items,
                TotalItems=response.TotalItems,
                CurrentPage=response.CurrentPage,
                TotalPages=response.TotalPages,
                HasNextPage=response.HasNextPage,
                HasPreviousPage=response.HasPreviousPage
            });
        }
    }
}
