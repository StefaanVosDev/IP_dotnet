using Microsoft.EntityFrameworkCore;

namespace DAL;

//TODO: Be sure the Repository is set up correctly for the project.
/// <summary>
/// The Repository class that implements the IRepository interface.
/// </summary>
/// <author>TCS</author>
/// <version>1.0.0</version>
public class Repository : IRepository
{
    private readonly TpDbContext _dbContext;
    
    public Repository(TpDbContext context)
    {
        _dbContext = context;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class => await _dbContext.Set<TEntity>().ToListAsync();

    public async Task<TEntity> GetAsync<TEntity>(int id) where TEntity : class => await _dbContext.Set<TEntity>().FindAsync(id);

    public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync<TEntity>(TEntity entityToUpdate, TEntity entity) where TEntity : class
    {
        _dbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}