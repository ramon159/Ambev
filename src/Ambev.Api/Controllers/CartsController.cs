using Ambev.Application.Features.Carts.Commands.CreateCart;
using Ambev.Application.Features.Carts.Commands.DeleteCart;
using Ambev.Application.Features.Carts.Commands.UpdateCart;
using Ambev.Application.Features.Carts.Queries.GetCart;
using Ambev.Application.Features.Carts.Queries.GetCartWithPagination;
using Ambev.Application.Models.Http;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ambev.Api.Controllers
{
    /// <summary>
    /// Carts controller
    /// </summary>
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// constructor of CartsController
        /// </summary>
        /// <param name="mediator"></param>
        public CartsController(IMediator mediator)
        {
            _mediator=mediator;
        }

        /// <summary>
        /// Retrieve a list of carts
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCarts([FromQuery] GetAllCartsQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithPagination<IReadOnlyCollection<GetCartResponse>>
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
        /// Retrieve a specific cart by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetCartQuery() { Id = id };

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<GetCartResponse>
            {
                Data = response,
                Message = "Cart retrived sucessfully"
            });
        }

        /// <summary>
        /// Add a new cart
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCartCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(new ApiResponseWithData<CreateCartResponse>
            {
                Data = response,
                Message = "Cart created successfully"
            });
        }

        /// <summary>
        /// Update a specific cart
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCartCommand request, CancellationToken cancellationToken)
        {
            request.SetId(id);
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<UpdateCartResponse>
            {
                Data = response,
                Message = "Cart updated successfully"
            });
        }

        /// <summary>
        /// Delete a specific cart
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteCartCommand() { Id = id };
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponse
            {
                Message = "Cart deleted successfully"
            });
        }
    }
}
