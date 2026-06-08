using AutoMapper;
using FinanceTracker.Application.Common.DTOs.Budgets;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Common.Profiles
{
    public class BudgetProfile : Profile
    {
        public BudgetProfile()
        {
            CreateMap<Budget, BudgetResponse>();
        }
    }
}
