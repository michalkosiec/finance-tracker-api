using FinanceTracker.Api.Dtos.Users;
using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Services
{
    public interface IUserService
    {
        public Task<UserReadDto?> GetUserByIdAsync(Guid id);
        public Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        public Task<UserReadDto> CreateUserAsync(UserCreateDto userCreate);
        public Task<bool> UpdateUserAsync(Guid id, UserUpdateDto userUpdate);
        public Task<bool> DeleteUserAsync(Guid id);
    }
}