﻿namespace DAL;

//TODO: Be sure the IRepository is set up correctly for the project.
/// <summary>
/// Defines a generic interface for repository operations.
/// </summary>
/// <author>TCS</author>
/// <version>1.0.0</version>
public interface IRepository
{
    Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class;
    Task<TEntity> GetAsync<TEntity>(int id) where TEntity : class;
    Task AddAsync<TEntity>(TEntity entity) where TEntity : class;
    Task UpdateAsync<TEntity>(TEntity entityToUpdate, TEntity entity) where TEntity : class;
    Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class;
}