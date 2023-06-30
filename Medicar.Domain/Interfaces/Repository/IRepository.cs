using System.Linq.Expressions;

namespace Medicar.Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
