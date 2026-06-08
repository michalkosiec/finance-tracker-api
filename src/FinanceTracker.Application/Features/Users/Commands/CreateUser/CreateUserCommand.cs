using FinanceTracker.Application.Common.DTOs.Users;
using MediatR;

namespace FinanceTracker.Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand(string IdentityUserId, string Name, string Email)
        : IRequest<UserResponse> { }
}
