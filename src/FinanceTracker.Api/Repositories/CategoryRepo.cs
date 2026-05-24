using FinanceTracker.Api.Data;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories.Interfaces;

namespace FinanceTracker.Api.Repositories
{
    public class CategoryRepo(AppDbContext context) : UserOwnedRepo<Category>(context), ICategoryRepo {}
}