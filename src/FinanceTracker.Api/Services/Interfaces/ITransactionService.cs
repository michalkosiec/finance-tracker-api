using FinanceTracker.Api.Dtos.Transactions;

namespace FinanceTracker.Api.Services.Interfaces
{
    public interface ITransactionService
    {
        public Task<IEnumerable<TransactionReadDto>> GetAllTransactionsAsync(Guid userId, TransactionQueryDto query);
        public Task<TransactionReadDto?> GetTransactionByIdAsync(Guid userId, Guid transactionId);
        public Task<TransactionReadDto> CreateTransactionAsync(Guid userId, TransactionCreateDto transactionCreate);
        public Task<bool> UpdateTransactionAsync(Guid userId, Guid transactionId, TransactionUpdateDto transactionUpdate);
        public Task<bool> DeleteTransactionAsync(Guid userId, Guid transactionId);
    }
}