using FinanceTracker.Api.Models;
using FinanceTracker.Api.Dtos.Categories;
using AutoMapper;

namespace FinanceTracker.Api.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>().ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<CategoryUpdateDto, Category>().ForMember(dest => dest.UserId, opt => opt.Ignore());
        }
    }
}