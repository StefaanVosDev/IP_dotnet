using DAL;

namespace BL;

//TODO: Be sure the Manager is set up correctly for the project.
/// <summary>
/// The Manager class that implements the IManager interface.
/// </summary>
/// <typeparam name="TEntity">The type of the entity to be managed. This must be a class.</typeparam>
/// <author>TCS</author>
/// <version>1.0.0</version>
public class Manager<TEntity> : IManager<TEntity> where TEntity : class
{
    private readonly IRepository _repo;

    public Manager(IRepository repository)
    {
        _repo = repository;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _repo.GetAllAsync<TEntity>();

    public async Task<TEntity> GetAsync(int id) => await _repo.GetAsync<TEntity>(id);

    public async Task AddAsync(TEntity entity) => await _repo.AddAsync(entity);

    public async Task UpdateAsync(TEntity entityToUpdate, TEntity entity) =>
        await _repo.UpdateAsync(entityToUpdate, entity);

    public async Task DeleteAsync(TEntity entity) => await _repo.DeleteAsync(entity);
}