using System.Linq.Expressions;
using BL.Domain;
using BL.Domain.Questions;
using DAL.Interfaces;

namespace DAL;

public class InMemoryRepository : IRepository
{
    private readonly Dictionary<Type, object> _store = new Dictionary<Type, object>();

    public Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);
        return _store.TryGetValue(type, out object value) ? Task.FromResult((IEnumerable<TEntity>)value) : Task.FromResult(Enumerable.Empty<TEntity>());
    }

    public Task<TEntity> GetAsync<TEntity>(int id) where TEntity : class
    {
        var entities = GetAllAsync<TEntity>().Result;
        var entity = entities.FirstOrDefault(e => ((dynamic)e).Id == id);
        return Task.FromResult(entity);
    }

    public Task AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        var type = typeof(TEntity);
        if (!_store.ContainsKey(type))
        {
            _store[type] = new List<TEntity>();
        }
        ((List<TEntity>)_store[type]).Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync<TEntity>(TEntity entityToUpdate, TEntity entity) where TEntity : class
    {
        throw new NotImplementedException("This method is not implemented for the InMemoryRepository.");
    }

    public Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
    {
        var type = typeof(TEntity);
        if (_store.ContainsKey(type))
        {
            ((List<TEntity>)_store[type]).Remove(entity);
        }
        return Task.CompletedTask;
    }

    public Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        throw new NotImplementedException("This method is not implemented for the InMemoryRepository.");
    }

    public Task UpdateAllAsync<TEntity>(IEnumerable<TEntity> entitiesToUpdate) where TEntity : class
    {
        throw new NotImplementedException();
    }
}
