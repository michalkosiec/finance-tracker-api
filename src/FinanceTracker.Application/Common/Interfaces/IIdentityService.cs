namespace FinanceTracker.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> RegisterUserAsync(string email, string password);
        Task<string?> LoginAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default
        );
    }
}
