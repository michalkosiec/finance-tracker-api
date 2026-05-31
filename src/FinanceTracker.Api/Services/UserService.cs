using AutoMapper;
using FinanceTracker.Api.Dtos.Users;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories;

namespace FinanceTracker.Api.Services
{
    public class UserService(IUserRepo userRepo, IMapper mapper) : IUserService
    {
        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userCreate)
        {
            var user = mapper.Map<User>(userCreate);
            await userRepo.CreateAsync(user);
            return mapper.Map<UserReadDto>(user);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            await userRepo.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var users = await userRepo.GetAllAsync();
            return mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto?> GetUserByIdAsync(Guid id)
        {
            var user = await userRepo.GetByIdAsync(id);
            return mapper.Map<UserReadDto>(user);
        }

        public async Task<bool> UpdateUserAsync(Guid id, UserUpdateDto userUpdate)
        {
            var user = await userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            mapper.Map(userUpdate, user);
            user.UpdatedAt = DateTimeOffset.UtcNow;
            await userRepo.UpdateAsync(user);
            return true;
        }

        public Task<bool> UpdateUserAsync(Guid id, User user)
        {
            throw new NotImplementedException();
        }
    }
}