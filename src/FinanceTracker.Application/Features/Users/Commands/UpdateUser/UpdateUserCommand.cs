using MediatR;

namespace FinanceTracker.Application.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(Guid Id, string Name, string Email) : IRequest<Unit> { }
}
