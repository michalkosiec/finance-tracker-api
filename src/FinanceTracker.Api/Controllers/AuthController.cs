using FinanceTracker.Application.Features.Auth.Commands.Login;
using FinanceTracker.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ISender mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand command,
            CancellationToken cancellationToken
        )
        {
            return Ok(new { AccessToken = await mediator.Send(command, cancellationToken) });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterCommand command,
            CancellationToken cancellationToken
        )
        {
            var newUserId = await mediator.Send(command, cancellationToken);

            return StatusCode(
                StatusCodes.Status201Created,
                new { Message = "User registered successfully.", UserId = newUserId }
            );
        }
    }
}
