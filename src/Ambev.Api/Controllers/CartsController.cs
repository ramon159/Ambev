using Ambev.Domain.Features.Carts.Commands.CreateCart;
using Ambev.Domain.Features.Carts.Commands.DeleteCart;
using Ambev.Domain.Features.Carts.Commands.UpdateCart;
using Ambev.Domain.Features.Carts.Queries.GetCart;
using Ambev.Domain.Features.Carts.Queries.GetCartWithPagination;
using Ambev.Shared.Common.Http;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ambev.Api.Controllers
{
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartsController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarts([FromQuery] GetAllCartsCommand request, CancellationToken cancellationToken)
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

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetCartCommand() { Id = id };

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<GetCartResponse>
            {
                Data = response,
                Message = "Cart retrived sucessfully"
            });
        }

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
