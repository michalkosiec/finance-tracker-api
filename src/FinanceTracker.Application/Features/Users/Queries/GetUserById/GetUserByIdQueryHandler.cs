using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinanceTracker.Application.Common.DTOs.Users;
using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(IAppDbContext context, IMapper mapper)
        : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        public async Task<UserResponse> Handle(
            GetUserByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var userResponse =
                await context
                    .Users.Where(u => u.Id == request.UserId)
                    .ProjectTo<UserResponse>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken)
                ?? throw new KeyNotFoundException("User not found for the given user ID.");

            return userResponse;
        }
    }
}
