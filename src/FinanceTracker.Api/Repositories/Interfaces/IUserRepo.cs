using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories.Interfaces;

namespace FinanceTracker.Api.Repositories
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User?> GetUserByEmail(string email);
    }
}