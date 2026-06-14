using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler(IAppDbContext context, IIdentityService identityService)
        : IRequestHandler<RegisterCommand, Guid>
    {
        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            {
                throw new ConflictException("Email is already in use.");
            }

            var keycloakUserId =
                await identityService.RegisterUserAsync(request.Email, request.Password)
                ?? throw new ConflictException("Failed to register user in Identity Provider.");

            var newUser = User.Create(keycloakUserId, request.Name, request.Email);

            context.Users.Add(newUser);
            await context.SaveChangesAsync(cancellationToken);

            return newUser.Id;
        }
    }
}
