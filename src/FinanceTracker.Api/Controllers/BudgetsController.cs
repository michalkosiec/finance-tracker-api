using FinanceTracker.Api.Models;
using FinanceTracker.Api.Dtos.Budgets;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FinanceTracker.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.Api.Services.Interfaces;

namespace FinanceTracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BudgetsController(IBudgetRepo repo, IMapper mapper, IValidationService validationService) : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBudgets()
        {
            var budgets = await repo.GetAllAsync(UserId!.Value);
            var budgetsRead = mapper.Map<IEnumerable<BudgetReadDto>>(budgets);

            return Ok(budgetsRead);
        }

        [HttpGet("{id}", Name = "GetBudgetById")]
        public async Task<IActionResult> GetBudgetById(Guid id)
        {
            var budget = await repo.GetByIdAsync(id, UserId!.Value);
            if (budget == null)
            {
                return NotFound();
            } else
            {
                var budgetRead = mapper.Map<BudgetReadDto>(budget);

                return Ok(budgetRead);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] BudgetCreateDto budgetCreate)
        {
            var budget = mapper.Map<Budget>(budgetCreate);
            budget.UserId = UserId!.Value;

            await validationService.AllowBudget(budget, UserId!.Value);

            await repo.CreateAsync(budget);
            var budgetRead = mapper.Map<BudgetReadDto>(budget);

            return CreatedAtAction(nameof(GetBudgetById), new { id = budget.Id }, budgetRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(Guid id, [FromBody] BudgetUpdateDto budgetUpdate)
        {
            var budget = await repo.GetByIdAsync(id, UserId!.Value);
            if (budget == null)
            {
                return NotFound();
            } else
            {
                mapper.Map(budgetUpdate, budget);
                budget.UserId = UserId!.Value;

                budget.UpdatedAt = DateTimeOffset.UtcNow;

                await validationService.AllowBudget(budget, UserId!.Value);
                
                await repo.UpdateAsync(budget, UserId!.Value);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(Guid id)
        {
            var budget = await repo.GetByIdAsync(id, UserId!.Value);
            if (budget == null)
            {
                return NotFound();
            } else
            {
                await repo.DeleteAsync(id, UserId!.Value);
                
                return NoContent();
            }
        }
    }
}