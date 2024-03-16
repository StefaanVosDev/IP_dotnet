using System.Linq.Expressions;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL;

/// <summary>
/// The Repository class that implements the IRepository interface.
/// </summary>
public class Repository(DbContext context) : IRepository
{
    public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class => await context.Set<TEntity>().ToListAsync();

    public async Task<TEntity> GetAsync<TEntity>(int id) where TEntity : class => await context.Set<TEntity>().FindAsync(id);

    public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync<TEntity>(TEntity entityToUpdate, TEntity entity) where TEntity : class
    {
        context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
    {
        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return Task.FromResult(context.Set<TEntity>().Where(predicate).AsEnumerable());
    }
}