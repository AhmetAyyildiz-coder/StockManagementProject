using Core.Entities.Base;

namespace Core.Repositories;

/// <summary>
/// Generic repository interface for basic CRUD operations.
/// Supports the repository pattern for data access layer abstraction.
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
/// <typeparam name="TKey">The primary key type</typeparam>
public interface IRepository<TEntity, TKey> 
    where TEntity : class, IEntity<TKey>
{
    /// <summary>
    /// Gets an entity by its identifier.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of all entities</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The added entity</returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated entity</returns>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the entity was deleted, false if not found</returns>
    Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Checks if an entity exists with the specified identifier.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the entity exists, false otherwise</returns>
    Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default);
}

/// <summary>
/// Generic repository interface for tenant-aware entities.
/// Automatically applies tenant filtering to all operations.
/// </summary>
/// <typeparam name="TEntity">The tenant entity type</typeparam>
/// <typeparam name="TKey">The primary key type</typeparam>
public interface ITenantRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : TenantEntity, IEntity<TKey>
{
    /// <summary>
    /// Gets all active entities for the current tenant.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of active entities</returns>
    Task<IEnumerable<TEntity>> GetActiveAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Performs a soft delete on an entity.
    /// Sets IsActive to false instead of physically removing the entity.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the entity was soft deleted, false if not found</returns>
    Task<bool> SoftDeleteAsync(TKey id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Restores a soft-deleted entity.
    /// Sets IsActive to true.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the entity was restored, false if not found</returns>
    Task<bool> RestoreAsync(TKey id, CancellationToken cancellationToken = default);
}