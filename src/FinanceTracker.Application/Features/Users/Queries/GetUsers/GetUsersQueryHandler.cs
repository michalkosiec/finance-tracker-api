using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinanceTracker.Application.Common.DTOs.Users;
using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler(IAppDbContext context, IMapper mapper)
        : IRequestHandler<GetUsersQuery, IReadOnlyCollection<UserResponse>>
    {
        public async Task<IReadOnlyCollection<UserResponse>> Handle(
            GetUsersQuery request,
            CancellationToken cancellationToken
        )
        {
            return await context
                .Users.ProjectTo<UserResponse>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
