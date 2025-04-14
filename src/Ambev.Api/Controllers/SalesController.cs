using Ambev.Application.Features.Sales.Commands.CreateSale;
using Ambev.Application.Features.Sales.Commands.DeleteSale;
using Ambev.Application.Features.Sales.Commands.UpdateSale;
using Ambev.Application.Features.Sales.Queries.GetSale;
using Ambev.Application.Features.Sales.Queries.GetSaleWithPagination;
using Ambev.Application.Models.Http;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ambev.Api.Controllers
{
    /// <summary>
    /// Sales controller
    /// </summary>
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Sales controller constructor
        /// </summary>
        /// <param name="mediator"></param>
        public SalesController(IMediator mediator)
        {
            _mediator=mediator;
        }

        /// <summary>
        /// Retrieve a list of sales
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithPagination<IReadOnlyCollection<GetSaleResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSales([FromQuery] GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(new ApiResponseWithPagination<IReadOnlyCollection<GetSaleResponse>>
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
        /// Retrieve a specific sale by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetSaleQuery() { Id = id };

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<GetSaleResponse>
            {
                Data = response,
                Message = "Sale retrived sucessfully"
            });
        }

        /// <summary>
        /// Add a new sale
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(new ApiResponseWithData<CreateSaleResponse>
            {
                Data = response,
                Message = "Sale created successfully"
            });
        }

        /// <summary>
        /// Update a specific sale
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            request.SetId(id);
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<UpdateSaleResponse>
            {
                Data = response,
                Message = "Sale updated successfully"
            });
        }
        /// <summary>
        /// Delete a specific sale
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteSaleCommand() { Id = id };

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponse
            {
                Message = "Sale deleted successfully"
            });
        }
    }
}
