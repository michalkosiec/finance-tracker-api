using FinanceTracker.Application.Common.DTOs.Users;
using MediatR;

namespace FinanceTracker.Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid UserId) : IRequest<UserResponse> { }
}
