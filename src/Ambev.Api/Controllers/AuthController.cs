using Ambev.Domain.Features.Authentication.Commands.AuthenticateUser;
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
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            this._mediator=mediator;
        }

        [HttpPost]
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
