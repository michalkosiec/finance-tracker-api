using System.Linq.Expressions;
using FinanceTracker.Api.Data;
using FinanceTracker.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Api.Repositories
{
    public abstract class GenericRepo<T>(AppDbContext context) : IGenericRepo<T> where T : class
    {
        protected readonly AppDbContext _context = context;

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetFirstOrDefaultByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await SaveChanges();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await SaveChanges();
            }
        }
        
        protected async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}