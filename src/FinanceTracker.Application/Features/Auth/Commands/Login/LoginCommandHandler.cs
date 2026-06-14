using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using MediatR;

namespace FinanceTracker.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler(IIdentityService identityService)
        : IRequestHandler<LoginCommand, string>
    {
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var token = await identityService.LoginAsync(
                request.Email,
                request.Password,
                cancellationToken
            );

            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedException("Invalid email or password.");

            return token;
        }
    }
}
