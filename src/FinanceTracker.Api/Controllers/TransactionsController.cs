using FinanceTracker.Api.Dtos.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.Api.Services.Interfaces;

namespace FinanceTracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController(ITransactionService transactionService) : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] TransactionQueryDto query)
        {
            var transactionsRead = await transactionService.GetAllTransactionsAsync(UserId!.Value, query);

            return Ok(transactionsRead);
        }

        [HttpGet("{id}", Name = "GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var transactionRead = await transactionService.GetTransactionByIdAsync(UserId!.Value, id);

            return transactionRead == null ? NotFound() : Ok(transactionRead);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionCreateDto transactionCreate)
        {
            var transactionRead = await transactionService.CreateTransactionAsync(UserId!.Value, transactionCreate);

            return CreatedAtAction(nameof(GetTransactions), new { id = transactionRead.Id }, transactionRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] TransactionUpdateDto transactionUpdate)
        {
            var result = await transactionService.UpdateTransactionAsync(UserId!.Value, id, transactionUpdate);

            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var result = await transactionService.DeleteTransactionAsync(UserId!.Value, id);
            return result ? NoContent() : NotFound();
        }
    }
}