using FinanceTracker.Application.Common.Interfaces;
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
            /*
            
            */

            var user =
                await context.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                ?? throw new KeyNotFoundException("User not found for the given user ID.");

            user.UpdateName(request.Name);
            user.UpdateEmail(request.Email);

            context.Users.Update(user);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
