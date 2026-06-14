using FinanceTracker.Application.Features.Users.Commands.CreateUser;
using FinanceTracker.Application.Features.Users.Commands.DeleteUser;
using FinanceTracker.Application.Features.Users.Commands.UpdateUser;
using FinanceTracker.Application.Features.Users.Queries.GetUserById;
using FinanceTracker.Application.Features.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController(ISender mediator) : AppControllerBase(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUsersQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(id), cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(
            [FromBody] CreateUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateUserCommand(
                request.IdentityUserId,
                request.Name,
                request.Email
            );

            var result = await Mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(
            Guid id,
            [FromBody] UpdateUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new UpdateUserCommand(id, request.Name, request.Email);

            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteUserCommand(id), cancellationToken);

            return NoContent();
        }

        public record CreateUserRequest(string IdentityUserId, string Name, string Email);

        public record UpdateUserRequest(string Name, string Email);
    }
}
