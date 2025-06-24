namespace Core.Interfaces.Repositories;

/// <summary>
/// Unit of Work interface for managing repositories and transactions.
/// Provides a single point of access to all repositories and ensures data consistency.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the tenant repository.
    /// </summary>
    ITenantRepository Tenants { get; }
    
    /// <summary>
    /// Gets the user repository.
    /// </summary>
    IUserRepository Users { get; }
    
    /// <summary>
    /// Gets the product repository.
    /// </summary>
    IProductRepository Products { get; }
    
    /// <summary>
    /// Gets the category repository.
    /// </summary>
    ICategoryRepository Categories { get; }
    
    /// <summary>
    /// Saves all changes made in this unit of work to the database.
    /// </summary>
    /// <returns>The number of entities written to the database</returns>
    Task<int> SaveChangesAsync();
    
    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    /// <returns>Task representing the transaction start operation</returns>
    Task BeginTransactionAsync();
    
    /// <summary>
    /// Commits the current database transaction.
    /// </summary>
    /// <returns>Task representing the transaction commit operation</returns>
    Task CommitTransactionAsync();
    
    /// <summary>
    /// Rolls back the current database transaction.
    /// </summary>
    /// <returns>Task representing the transaction rollback operation</returns>
    Task RollbackTransactionAsync();
}