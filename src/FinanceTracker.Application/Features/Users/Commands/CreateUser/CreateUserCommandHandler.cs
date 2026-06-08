using AutoMapper;
using FinanceTracker.Application.Common.DTOs.Users;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IAppDbContext context, IMapper mapper)
        : IRequestHandler<CreateUserCommand, UserResponse>
    {
        public async Task<UserResponse> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var user = User.Create(request.IdentityUserId, request.Name, request.Email);

            await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<UserResponse>(user);
        }
    }
}
