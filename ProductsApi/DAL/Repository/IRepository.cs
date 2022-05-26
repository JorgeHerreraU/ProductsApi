using System.Linq.Expressions;
using ProductsApi.Models;

namespace ProductsApi.DAL.Repository;

public interface IRepository<T> where T : BaseEntity
{
    ValueTask<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    ValueTask<T?> GetByIdAsync(int id);
    ValueTask<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteAsync(IEnumerable<T> entities);
}
