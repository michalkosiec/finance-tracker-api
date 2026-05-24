using FinanceTracker.Api.Dtos.Users;

namespace FinanceTracker.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(UserLoginDto userLogin);
        Task<bool> RegisterAsync(UserCreateDto userCreate);
    }
}