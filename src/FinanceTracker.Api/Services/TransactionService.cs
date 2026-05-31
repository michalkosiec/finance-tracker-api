using AutoMapper;
using FinanceTracker.Api.Dtos.Transactions;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories.Interfaces;
using FinanceTracker.Api.Services.Interfaces;

namespace FinanceTracker.Api.Services
{
    public class TransactionService(ITransactionRepo transactionRepo, IMapper mapper, IValidationService validationService) : ITransactionService
    {
        public async Task<TransactionReadDto> CreateTransactionAsync(Guid userId, TransactionCreateDto transactionCreate)
        {
            var transaction = mapper.Map<Transaction>(transactionCreate);

            transaction.UserId = userId;

            await validationService.AllowTransaction(transaction, userId);
            
            await transactionRepo.CreateAsync(transaction);

            return mapper.Map<TransactionReadDto>(transaction);
        }

        public async Task<bool> DeleteTransactionAsync(Guid userId, Guid transactionId)
        {
            var transaction = await transactionRepo.GetByIdAsync(transactionId, userId);
            if (transaction == null)
                return false;

            await transactionRepo.DeleteAsync(transactionId, userId);
            return true;
        }

        public async Task<IEnumerable<TransactionReadDto>> GetAllTransactionsAsync(Guid userId, TransactionQueryDto query)
        {
            var parameters = mapper.Map<TransactionParameters>(query);
            var transactions = await transactionRepo.GetAllByUserIdAsync(userId, parameters);
            var transactionsRead = mapper.Map<IEnumerable<TransactionReadDto>>(transactions);

            return transactionsRead;
        }

        public async Task<TransactionReadDto?> GetTransactionByIdAsync(Guid userId, Guid transactionId)
        {
            var transaction = await transactionRepo.GetByIdAsync(transactionId, userId);
            var transactionRead = mapper.Map<TransactionReadDto>(transaction);

            return transactionRead;
        }

        public async Task<bool> UpdateTransactionAsync(Guid userId, Guid transactionId, TransactionUpdateDto transactionUpdate)
        {
            var transaction = await transactionRepo.GetByIdAsync(transactionId, userId);
            if (transaction == null)
                return false;

            mapper.Map(transactionUpdate, transaction);
            await validationService.AllowTransaction(transaction, userId);
            await transactionRepo.UpdateAsync(transaction, userId);

            return true;
        }
    }
}