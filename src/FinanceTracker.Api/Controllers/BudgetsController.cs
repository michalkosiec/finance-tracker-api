using FinanceTracker.Api.Models;
using FinanceTracker.Api.Dtos.Budgets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.Api.Services;

namespace FinanceTracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BudgetsController(IBudgetService budgetService) : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBudgets()
        {
            var budgetsRead = await budgetService.GetAllBudgetsAsync(UserId!.Value);

            return Ok(budgetsRead);
        }

        [HttpGet("{id}", Name = "GetBudgetById")]
        public async Task<IActionResult> GetBudgetById(Guid id)
        {
            var budgetRead = await budgetService.GetBudgetByIdAsync(UserId!.Value, id);

            return budgetRead == null ? NotFound() : Ok(budgetRead);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] BudgetCreateDto budgetCreate)
        {
            var budgetRead = await budgetService.CreateBudgetAsync(UserId!.Value, budgetCreate);

            return CreatedAtAction(nameof(GetBudgetById), new { id = budgetRead.Id }, budgetRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(Guid id, [FromBody] BudgetUpdateDto budgetUpdate)
        {
            var result = await budgetService.UpdateBudgetAsync(UserId!.Value, id, budgetUpdate);

            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(Guid id)
        {
            var result = await budgetService.DeleteBudgetAsync(UserId!.Value, id);

            return result ? NoContent() : NotFound();
        }
    }
}