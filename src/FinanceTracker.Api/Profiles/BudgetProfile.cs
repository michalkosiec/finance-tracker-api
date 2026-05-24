using System.Globalization;
using FinanceTracker.Api.Dtos.Budgets;
using FinanceTracker.Api.Models;
using AutoMapper;

namespace FinanceTracker.Api.Profiles
{
    public class BudgetProfile : Profile
    {
        public BudgetProfile()
        {
            CreateMap<Budget, BudgetReadDto>()
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month.ToString("yyyy-MM")));

            CreateMap<BudgetCreateDto, Budget>().ForMember(dest => dest.Month, opt => opt.MapFrom(src => DateTime.SpecifyKind(
            DateTime.ParseExact(src.Month, "yyyy-MM", CultureInfo.InvariantCulture), 
            DateTimeKind.Utc
        ))).ForMember(dest => dest.UserId, opt => opt.Ignore());;
        
            CreateMap<BudgetUpdateDto, Budget>().ForMember(dest => dest.Month, opt => opt.MapFrom(src => DateTime.SpecifyKind(
            DateTime.ParseExact(src.Month, "yyyy-MM", CultureInfo.InvariantCulture), 
            DateTimeKind.Utc
        ))).ForMember(dest => dest.UserId, opt => opt.Ignore());;
        }
    }
}