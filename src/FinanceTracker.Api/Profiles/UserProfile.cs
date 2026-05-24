using FinanceTracker.Api.Dtos.Users;
using FinanceTracker.Api.Models;
using AutoMapper;

namespace FinanceTracker.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>().ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UserUpdateDto, User>();
        }
    }
}