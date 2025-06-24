using Core.Common;
using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces.Services;

/// <summary>
/// Service interface for managing stock movements in the inventory system.
/// Provides operations for creating, retrieving, and validating stock movements.
/// </summary>
public interface IStockMovementService
{
    #region Movement Operations

    /// <summary>
    /// Creates a new stock movement based on the provided request data.
    /// </summary>
    /// <param name="request">The request containing stock movement creation data.</param>
    /// <returns>A task that represents the asynchronous operation containing the created stock movement.</returns>
    Task<StockMovement> CreateStockMovementAsync(CreateStockMovementRequest request);

    /// <summary>
    /// Creates multiple stock movements in a single transaction for bulk operations.
    /// </summary>
    /// <param name="requests">The list of requests containing stock movement creation data.</param>
    /// <returns>A task that represents the asynchronous operation containing the list of created stock movements.</returns>
    Task<List<StockMovement>> CreateBulkStockMovementAsync(List<CreateStockMovementRequest> requests);

    /// <summary>
    /// Retrieves a specific stock movement by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the stock movement.</param>
    /// <returns>A task that represents the asynchronous operation containing the stock movement, or null if not found.</returns>
    Task<StockMovement?> GetStockMovementAsync(int id);

    /// <summary>
    /// Retrieves stock movements based on the specified search criteria with pagination support.
    /// </summary>
    /// <param name="criteria">The search criteria to filter stock movements.</param>
    /// <returns>A task that represents the asynchronous operation containing the paged result of stock movements.</returns>
    Task<PagedResult<StockMovement>> GetStockMovementsAsync(StockMovementSearchCriteria criteria);

    #endregion

    #region Quick Stock Operations

    /// <summary>
    /// Performs a quick stock-in operation for receiving products into inventory.
    /// </summary>
    /// <param name="productId">The identifier of the product being received.</param>
    /// <param name="quantity">The quantity being received.</param>
    /// <param name="reference">Optional reference document (e.g., purchase order number, invoice number).</param>
    /// <param name="unitPrice">Optional unit price of the received products.</param>
    /// <param name="supplierId">Optional identifier of the supplier providing the products.</param>
    /// <returns>A task that represents the asynchronous operation containing the created stock movement.</returns>
    Task<StockMovement> StockInAsync(int productId, int quantity, string? reference = null, decimal? unitPrice = null, int? supplierId = null);

    /// <summary>
    /// Performs a quick stock-out operation for removing products from inventory.
    /// </summary>
    /// <param name="productId">The identifier of the product being removed.</param>
    /// <param name="quantity">The quantity being removed.</param>
    /// <param name="reference">Optional reference document (e.g., sales order number, delivery note).</param>
    /// <param name="notes">Optional notes explaining the reason for stock removal.</param>
    /// <returns>A task that represents the asynchronous operation containing the created stock movement.</returns>
    Task<StockMovement> StockOutAsync(int productId, int quantity, string? reference = null, string? notes = null);

    /// <summary>
    /// Performs a stock adjustment to set the stock level to a specific quantity.
    /// This creates the necessary movements to reach the target quantity.
    /// </summary>
    /// <param name="productId">The identifier of the product being adjusted.</param>
    /// <param name="newQuantity">The target quantity after adjustment.</param>
    /// <param name="reason">The reason for the stock adjustment.</param>
    /// <returns>A task that represents the asynchronous operation containing the created stock movement.</returns>
    Task<StockMovement> AdjustStockAsync(int productId, int newQuantity, string reason);

    #endregion

    #region Validation & Business Rules

    /// <summary>
    /// Validates a stock movement request to ensure it meets all business rules and constraints.
    /// Throws appropriate exceptions if validation fails.
    /// </summary>
    /// <param name="request">The stock movement request to validate.</param>
    /// <returns>A task that represents the asynchronous validation operation.</returns>
    Task ValidateStockMovementAsync(CreateStockMovementRequest request);

    /// <summary>
    /// Determines whether a stock movement can be created based on current stock levels and business rules.
    /// </summary>
    /// <param name="productId">The identifier of the product.</param>
    /// <param name="movementTypeId">The identifier of the movement type.</param>
    /// <param name="quantity">The quantity to be moved.</param>
    /// <returns>A task that represents the asynchronous operation containing true if the movement can be created, false otherwise.</returns>
    Task<bool> CanCreateMovementAsync(int productId, int movementTypeId, int quantity);

    #endregion
}