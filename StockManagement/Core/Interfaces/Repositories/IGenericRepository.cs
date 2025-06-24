using System.Linq.Expressions;
using Core.Common;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Generic repository interface for comprehensive data access operations.
/// Supports the repository pattern with advanced querying, pagination, and bulk operations.
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
/// <typeparam name="TKey">The primary key type</typeparam>
public interface IGenericRepository<TEntity, TKey> where TEntity : class
{
    // Query Operations
    
    /// <summary>
    /// Gets an entity by its identifier.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, null otherwise</returns>
    Task<TEntity?> GetByIdAsync(TKey id);
    
    /// <summary>
    /// Finds a single entity that matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to match</param>
    /// <returns>The first entity that matches the predicate, null if none found</returns>
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
    
    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>Collection of all entities</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    /// <summary>
    /// Finds all entities that match the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to match</param>
    /// <returns>Collection of entities that match the predicate</returns>
    Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
    
    /// <summary>
    /// Gets entities with pagination support and optional filtering.
    /// </summary>
    /// <param name="page">The page number (1-based)</param>
    /// <param name="pageSize">The number of items per page</param>
    /// <param name="filter">Optional filter predicate</param>
    /// <returns>Paged result containing entities and pagination metadata</returns>
    Task<PagedResult<TEntity>> GetPagedAsync(int page, int pageSize, Expression<Func<TEntity, bool>>? filter = null);
    
    // Command Operations
    
    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The added entity</returns>
    Task<TEntity> AddAsync(TEntity entity);
    
    /// <summary>
    /// Adds a collection of entities in a single operation.
    /// </summary>
    /// <param name="entities">The entities to add</param>
    /// <returns>The added entities</returns>
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
    
    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <returns>Task representing the update operation</returns>
    Task UpdateAsync(TEntity entity);
    
    /// <summary>
    /// Deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>Task representing the delete operation</returns>
    Task DeleteAsync(TKey id);
    
    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <param name="entity">The entity to delete</param>
    /// <returns>Task representing the delete operation</returns>
    Task DeleteAsync(TEntity entity);
    
    /// <summary>
    /// Checks if an entity exists with the specified identifier.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>True if the entity exists, false otherwise</returns>
    Task<bool> ExistsAsync(TKey id);
    
    /// <summary>
    /// Checks if any entity exists that matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to match</param>
    /// <returns>True if any entity matches the predicate, false otherwise</returns>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    
    // Count Operations
    
    /// <summary>
    /// Gets the total count of entities.
    /// </summary>
    /// <returns>The total number of entities</returns>
    Task<int> CountAsync();
    
    /// <summary>
    /// Gets the count of entities that match the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to match</param>
    /// <returns>The number of entities that match the predicate</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
}