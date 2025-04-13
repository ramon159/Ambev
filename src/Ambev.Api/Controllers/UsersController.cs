using Ambev.Application.Features.Users.Commands.CreateUser;
using Ambev.Application.Features.Users.Commands.DeleteUser;
using Ambev.Application.Features.Users.Commands.UpdateUser;
using Ambev.Application.Features.Users.Queries.GetUser;
using Ambev.Application.Features.Users.Queries.GetUsersWithPagination;
using Ambev.Application.Models.Http;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ambev.Api.Controllers
{
    /// <summary>
    /// Users controller
    /// </summary>
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Users controller constructor
        /// </summary>
        /// <param name="mediator"></param>
        public UsersController(IMediator mediator)
        {
            _mediator=mediator;
        }

        /// <summary>
        /// Retrieve a list of users
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithPagination<IReadOnlyCollection<GetUserResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(new ApiResponseWithPagination<IReadOnlyCollection<GetUserResponse>>
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
        /// Retrieve a specific user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseWithData<GetUserResponse>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetUserCommand() { Id = id };

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<GetUserResponse>
            {
                Data = response,
                Message = "User retrived sucessfully"
            });
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(new ApiResponseWithData<CreateUserResponse>
            {
                Data = response,
                Message = "User created successfully"
            });
        }

        /// <summary>
        /// Update a specific user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserCommand request, CancellationToken cancellationToken)
        {
            request.SetId(id);
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<UpdateUserResponse>
            {
                Data = response,
                Message = "User updated successfully"
            });
        }
        /// <summary>
        /// Delete a specific user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteUserCommand() { Id = id };

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponse
            {
                Message = "User deleted successfully"
            });
        }
    }
}
