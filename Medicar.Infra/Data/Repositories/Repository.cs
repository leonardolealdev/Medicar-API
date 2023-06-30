using Medicar.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Medicar.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MedicarDbContext _dbContext;
        protected readonly DbSet<T> _currentSet;

        public Repository(MedicarDbContext dbContext) 
        {
            _dbContext = dbContext;
            _currentSet = dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _currentSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            var entityEntry = _dbContext.Entry(entity); 
            entityEntry.State= EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _currentSet.ToListAsync();
    }
}
