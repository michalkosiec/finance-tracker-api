using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinanceTracker.Application.Common.DTOs.Users;
using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
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
                ?? throw new NotFoundException(nameof(User), new { request.UserId });

            return userResponse;
        }
    }
}
