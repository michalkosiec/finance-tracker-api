using System.Linq.Expressions;
using FinanceTracker.Api.Data;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Api.Repositories
{
    public abstract class UserOwnedRepo<T>(AppDbContext context) : GenericRepo<T>(context), IUserOwnedRepo<T> where T: class, IUserOwned
    {
         public async Task<IEnumerable<T>> GetAllAsync(Guid userID)
        {
            return await _context.Set<T>().AsNoTracking().Where(e => e.UserId == userID).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.UserId == userId && e.Id == id);
        }

        public async Task<T?> GetFirstOrDefaultByAsync(Expression<Func<T, bool>> predicate, Guid userId)
        {
            return await _context.Set<T>().Where(e => e.UserId == userId).FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, Guid userId)
        {
            return await _context.Set<T>().Where(e => e.UserId == userId).AnyAsync(predicate);
        }

        public async Task UpdateAsync(T entity, Guid userId)
        {
            if (entity.UserId != userId) throw new UnauthorizedAccessException("You are not authorized to edit this data.");

            _context.Set<T>().Update(entity);
            await SaveChanges();
        }

        public async Task DeleteAsync(Guid id, Guid userId)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await SaveChanges();
            }
        }

        public new async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveChanges();
        }
    }
}