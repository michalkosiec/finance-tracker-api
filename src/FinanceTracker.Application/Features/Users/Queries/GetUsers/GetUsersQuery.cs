using FinanceTracker.Application.Common.DTOs.Users;
using MediatR;

namespace FinanceTracker.Application.Features.Users.Queries.GetUsers
{
    public record GetUsersQuery : IRequest<IReadOnlyCollection<UserResponse>> { }
}
