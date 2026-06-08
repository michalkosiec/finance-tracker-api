using AutoMapper;
using FinanceTracker.Application.Common.DTOs.Users;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Common.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>();
        }
    }
}
