using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IAppDbContext context)
        : IRequestHandler<UpdateUserCommand, Unit>
    {
        public async Task<Unit> Handle(
            UpdateUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var user =
                await context.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(User), new { request.Id });

            user.UpdateName(request.Name);
            user.UpdateEmail(request.Email);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
