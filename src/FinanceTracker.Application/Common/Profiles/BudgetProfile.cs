using AutoMapper;
using FinanceTracker.Application.Common.DTOs.Budgets;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Common.Profiles
{
    public class BudgetProfile : Profile
    {
        public BudgetProfile()
        {
            CreateMap<Budget, BudgetResponse>()
                .ForCtorParam("LimitAmount", opt => opt.MapFrom(src => src.LimitAmount.Amount))
                .ForCtorParam("Currency", opt => opt.MapFrom(src => src.LimitAmount.Currency))
                .ForCtorParam("CreatedAt", opt => opt.MapFrom(src => src.CreatedAt.UtcDateTime))
                .ForCtorParam("UpdatedAt", opt => opt.MapFrom(src => src.UpdatedAt.UtcDateTime));
            ;
        }
    }
}
