using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

namespace ProductsApi.DAL.Repository;
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var query = includes.Aggregate(_context.Set<T>().AsQueryable(), (current, include) => current.Include(include));
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        var query = includes.Aggregate(_context.Set<T>().Where(predicate).AsQueryable(), (current, include) => current.Include(include));
        return await query.ToListAsync();
    }

    public async ValueTask<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async ValueTask<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        var query = includes.Aggregate(_context.Set<T>().AsQueryable(), (current, include) => current.Include(include));
        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task AddAsync(T entity)
    {
        _context.Add(entity);
        return _context.SaveChangesAsync();
    }

    public Task UpdateAsync(T entity)
    {
        _context.Update(entity);
        return _context.SaveChangesAsync();
    }

    public Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        return _context.SaveChangesAsync();
    }

    public Task DeleteAsync(IEnumerable<T> entities)
    {
        _context.RemoveRange(entities);
        return _context.SaveChangesAsync();
    }

    public async ValueTask<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }
}
