using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EF.RepositoryPattern.NET.Interfaces;

/// <summary>
/// Base repository class providing common CRUD operations for entities.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
public interface IBaseRepository<TEntity> where TEntity : IBaseEntity
{
    /// <summary>
    /// The UseLazyLoadingProxies property in Entity Framework is used to enable lazy loading of related entities.
    /// When this property is enabled, related entities are not automatically loaded 
    /// when the main entity is loaded.
    /// </summary>
    /// <param name="useAsLazyLoadingProxies">Used to enable or disable lazy loading.</param>
    public void UseAsLazyLoadingProxies(bool useAsLazyLoadingProxies);
    
    /// <summary>
    /// Retrieves an IQueryable representing the given entity.
    /// </summary>
    /// <returns>An IQueryable of TEntity.</returns>
    IQueryable<TEntity> AsQueryable();

    /// <summary>
    /// Retrieves an IQueryable as no tracking representing the given entity.
    /// </summary>
    /// <returns>An IQueryable of TEntity.</returns>
    public IQueryable<TEntity> AsNoTracking();

    /// <summary>
    /// Retrieves all entities synchronously.
    /// </summary>
    /// <returns>An IEnumerable of TEntity containing all entities.</returns>
    IEnumerable<TEntity> GetAll();
    
    /// <summary>
    /// Retrieves all entities asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An IEnumerable of TEntity containing all entities.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Adds a new entity synchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    void Add(TEntity entity);
    
    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Adds a range of entities synchronously.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    void AddRange(IEnumerable<TEntity> entities);
    
    /// <summary>
    /// Adds a range of entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates an existing entity synchronously.
    /// </summary>
    /// <param name="oldEntity">The old entity.</param>
    /// <param name="newEntity">The new entity with updated values.</param>
    void Update(TEntity oldEntity, TEntity newEntity);
    
    /// <summary>
    /// Updates an existing entity synchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(TEntity entity);
    
    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="oldEntity">The old entity.</param>
    /// <param name="newEntity">The new entity with updated values.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task UpdateAsync(TEntity oldEntity, TEntity newEntity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates a range of entities synchronously.
    /// </summary>
    /// <param name="entities">The entities to update.</param>
    void UpdateRange(IEnumerable<TEntity> entities);
    
    /// <summary>
    /// Updates a range of entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes an entity synchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Delete(TEntity entity);
    
    /// <summary>
    /// Deletes an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes a range of entities synchronously.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    void DeleteRange(IEnumerable<TEntity> entities);
    
    /// <summary>
    /// Deletes a range of entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Truncates the entity synchronously.
    /// </summary>
    void Truncate();
    
    /// <summary>
    /// Truncates the entity asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task TruncateAsync(CancellationToken cancellationToken = default);
}