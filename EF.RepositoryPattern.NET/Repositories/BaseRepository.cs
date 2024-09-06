#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EF.RepositoryPattern.NET.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

#endregion

namespace EF.RepositoryPattern.NET.Repositories;

/// <summary>
/// Base repository class providing common CRUD operations for entities.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
/// <typeparam name="TContext">The type of the DbContext used by the repository.</typeparam>
public abstract class BaseRepository<TEntity, TContext>
    where TEntity : class, IBaseEntity
    where TContext : DbContext
{
    private readonly TContext _dbContext;
    private bool? _useAsLazyLoadingProxies;
    
    /// <summary>
    /// Constructs an instance of the base repository with the provided DbContext.
    /// </summary>
    /// <param name="dbContext">The DbContext instance.</param>
    protected BaseRepository(TContext dbContext)
    {
        _dbContext = dbContext;
        if (_useAsLazyLoadingProxies.HasValue)
            _dbContext.ChangeTracker.LazyLoadingEnabled = _useAsLazyLoadingProxies.Value;
    }

    /// <summary>
    /// The UseLazyLoadingProxies property in Entity Framework is used to enable lazy loading of related entities.
    /// When this property is enabled, related entities are not automatically loaded 
    /// when the main entity is loaded.
    /// </summary>
    /// <param name="useAsLazyLoadingProxies">Used to enable or disable lazy loading.</param>
    public void UseAsLazyLoadingProxies(bool useAsLazyLoadingProxies)
    {
        _useAsLazyLoadingProxies = useAsLazyLoadingProxies;
    }
    
    /// <summary>
    /// Retrieves an IQueryable representing the given entity.
    /// </summary>
    /// <returns>An IQueryable of TEntity.</returns>
    public IQueryable<TEntity> AsQueryable()
    {
        Log.Logger.Information("Retrieving queryable for '{TEntity}'.", typeof(TEntity).Name);
        return _dbContext.Set<TEntity>();
    }
    
    /// <summary>
    /// Retrieves an IQueryable as no tracking representing the given entity.
    /// </summary>
    /// <returns>An IQueryable of TEntity.</returns>
    public IQueryable<TEntity> AsNoTracking()
    {
        Log.Logger.Information("Retrieving queryable for '{TEntity}'.", typeof(TEntity).Name);
        return _dbContext.Set<TEntity>().AsNoTracking();
    }

    /// <summary>
    /// Retrieves all entities synchronously.
    /// </summary>
    /// <returns>An IEnumerable of TEntity containing all entities.</returns>
    public virtual IEnumerable<TEntity> GetAll()
    {
        Log.Logger.Information("Retrieving all records from '{TEntity}'.", typeof(TEntity).Name);
        return _dbContext.Set<TEntity>();
    }

    /// <summary>
    /// Retrieves all entities asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An IEnumerable of TEntity containing all entities.</returns>
    public virtual Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        Log.Logger.Information("Retrieving all records from '{TEntity}'.", typeof(TEntity).Name);
        return Task.FromResult<IEnumerable<TEntity>>(_dbContext.Set<TEntity>());
    }

    /// <summary>
    /// Adds a new entity synchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public virtual void Add(TEntity entity)
    {
        Log.Logger.Information("Adding record to '{TEntity}'.", typeof(TEntity).Name);
        _dbContext.Set<TEntity>().Add(entity);
        _dbContext.SaveChanges();
    }
    
    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Log.Logger.Information("Adding record to '{TEntity}'.", typeof(TEntity).Name);
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Adds a range of entities synchronously.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        Log.Logger.Information("Adding range records to '{TEntity}'.", typeof(TEntity).Name);
        _dbContext.Set<TEntity>().AddRange(entities);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Adds a range of entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Log.Logger.Information("Adding range records to '{TEntity}'.", typeof(TEntity).Name);
        await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Updates an existing entity synchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public virtual void Update(TEntity entity)
    {
        Log.Logger.Information("Updating record from '{TEntity}'.", typeof(TEntity).Name);
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Updates an existing entity synchronously.
    /// </summary>
    /// <param name="oldEntity">The old entity.</param>
    /// <param name="newEntity">The new entity with updated values.</param>
    public virtual void Update(TEntity oldEntity, TEntity newEntity)
    {
        Log.Logger.Information("Updating old record from '{TEntity}'.", typeof(TEntity).Name);
        _dbContext.Entry(oldEntity).State = EntityState.Detached;
        _dbContext.Update(newEntity);
        _dbContext.SaveChanges();
    }
    
    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Log.Logger.Information("Updating record from '{TEntity}'.", typeof(TEntity).Name);
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="oldEntity">The old entity.</param>
    /// <param name="newEntity">The new entity with updated values.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public virtual async Task UpdateAsync(TEntity oldEntity, TEntity newEntity, CancellationToken cancellationToken = default)
    {
        Log.Logger.Information("Updating old record from '{TEntity}'.", typeof(TEntity).Name);
        _dbContext.Entry(oldEntity).State = EntityState.Detached;
        _dbContext.Update(newEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates a range of entities synchronously.
    /// </summary>
    /// <param name="entities">The entities to update.</param>
    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {   
        Log.Logger.Information("Updating range records from '{TEntity}'.", typeof(TEntity).Name);
        _dbContext.Set<TEntity>().UpdateRange(entities);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Updates a range of entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {   
        Log.Logger.Information("Updating range records from '{TEntity}'.", typeof(TEntity).Name);
        _dbContext.Set<TEntity>().UpdateRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Deletes an entity synchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    public virtual void Delete(TEntity entity)
    {
        Log.Logger.Information("Deleting record from '{TEntity}'.", typeof(TEntity).Name);
        _dbContext.Set<TEntity>().Remove(entity);
        _dbContext.SaveChanges();
    }
    
    /// <summary>
    /// Deletes an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Log.Logger.Information("Deleting record from '{TEntity}'.", typeof(TEntity).Name);
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Deletes a range of entities synchronously.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        Log.Logger.Information("Deleting range records from '{TEntity}'.", typeof(TEntity).Name);
        _dbContext.Set<TEntity>().RemoveRange(entities);
        _dbContext.SaveChanges();
    }
    
    /// <summary>
    /// Deletes a range of entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Log.Logger.Information("Deleting range records from '{TEntity}'.", typeof(TEntity).Name);
        _dbContext.Set<TEntity>().RemoveRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Truncates the entity synchronously.
    /// </summary>
    public virtual void Truncate()
    {
        Log.Logger.Information("Deleting range records from: {TEntity}", typeof(TEntity).Name);
        _dbContext.Set<TEntity>().RemoveRange(_dbContext.Set<TEntity>());
        _dbContext.SaveChanges();
    }
    
    /// <summary>
    /// Truncates the entity asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    public virtual async Task TruncateAsync(CancellationToken cancellationToken = default)
    {
        Log.Logger.Information("Deleting range records from: {TEntity}", typeof(TEntity).Name);
        _dbContext.Set<TEntity>().RemoveRange(_dbContext.Set<TEntity>());
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}