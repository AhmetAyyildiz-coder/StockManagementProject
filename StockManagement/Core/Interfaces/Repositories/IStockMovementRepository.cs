using Core.Common;
using Core.Entities;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for stock movement specific operations.
/// Extends the generic repository with stock movement-specific business logic and reporting capabilities.
/// </summary>
public interface IStockMovementRepository : IGenericRepository<StockMovement, int>
{
    /// <summary>
    /// Gets all stock movements for a specific product.
    /// </summary>
    /// <param name="productId">The product identifier</param>
    /// <returns>Collection of stock movements for the product</returns>
    Task<IEnumerable<StockMovement>> GetByProductIdAsync(int productId);
    
    /// <summary>
    /// Gets stock movements for a specific product within a date range.
    /// </summary>
    /// <param name="productId">The product identifier</param>
    /// <param name="from">Start date (inclusive)</param>
    /// <param name="to">End date (inclusive)</param>
    /// <returns>Collection of stock movements for the product within the date range</returns>
    Task<IEnumerable<StockMovement>> GetByProductIdAsync(int productId, DateTime from, DateTime to);
    
    /// <summary>
    /// Gets all stock movements for a specific movement type.
    /// </summary>
    /// <param name="movementTypeId">The movement type identifier</param>
    /// <returns>Collection of stock movements for the movement type</returns>
    Task<IEnumerable<StockMovement>> GetByMovementTypeAsync(int movementTypeId);
    
    /// <summary>
    /// Gets paginated stock movement history with optional filtering.
    /// Supports comprehensive filtering and pagination for reporting and audit purposes.
    /// </summary>
    /// <param name="productId">Optional product filter</param>
    /// <param name="movementTypeId">Optional movement type filter</param>
    /// <param name="from">Optional start date filter</param>
    /// <param name="to">Optional end date filter</param>
    /// <param name="page">Page number (1-based)</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <returns>Paged result containing matching stock movements</returns>
    Task<PagedResult<StockMovement>> GetMovementHistoryAsync(
        int? productId = null,
        int? movementTypeId = null,
        DateTime? from = null,
        DateTime? to = null,
        int page = 1,
        int pageSize = 50);
    
    /// <summary>
    /// Calculates the current stock level for a specific product.
    /// Sums all stock movements considering their directions.
    /// </summary>
    /// <param name="productId">The product identifier</param>
    /// <returns>Current stock quantity for the product</returns>
    Task<int> GetCurrentStockAsync(int productId);
    
    /// <summary>
    /// Calculates the total stock value for a specific product.
    /// Considers unit prices and quantities of all movements.
    /// </summary>
    /// <param name="productId">The product identifier</param>
    /// <returns>Total stock value for the product</returns>
    Task<decimal> GetStockValueAsync(int productId);
}