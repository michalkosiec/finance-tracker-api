using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IAppDbContext context)
        : IRequestHandler<DeleteUserCommand, Unit>
    {
        public async Task<Unit> Handle(
            DeleteUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var user =
                await context.Users.FirstOrDefaultAsync(
                    u => u.Id == request.UserId,
                    cancellationToken
                ) ?? throw new NotFoundException(nameof(User), new { request.UserId });

            context.Users.Remove(user);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
