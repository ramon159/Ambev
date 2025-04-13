using Ambev.Application.Features.Authentication.Commands.AuthenticateUser;
using Ambev.Application.Models.Http;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ambev.Api.Controllers
{
    /// <summary>
    /// Authorization Controller
    /// </summary>
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Authorization Controller constructor
        /// </summary>
        /// <param name="mediator"></param>
        public AuthController(IMediator mediator)
        {
            this._mediator=mediator;
        }

        /// <summary>
        /// For User authentication
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<AuthUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] AuthUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(new ApiResponseWithData<AuthUserResponse>
            {
                Data = response,
                Message = "User authenticated"
            });
        }

    }
}
