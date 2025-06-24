using Core.DTOs;
using Core.Enums;

namespace Core.Interfaces.Services;

/// <summary>
/// Service interface for stock calculation and analysis operations.
/// Provides methods for calculating current stock levels, valuations, and stock projections.
/// </summary>
public interface IStockCalculationService
{
    #region Stock Calculation

    /// <summary>
    /// Calculates the current stock quantity for a specific product.
    /// </summary>
    /// <param name="productId">The identifier of the product.</param>
    /// <returns>A task that represents the asynchronous operation containing the current stock quantity.</returns>
    Task<int> GetCurrentStockAsync(int productId);

    /// <summary>
    /// Calculates the total monetary value of current stock for a specific product.
    /// </summary>
    /// <param name="productId">The identifier of the product.</param>
    /// <returns>A task that represents the asynchronous operation containing the total stock value.</returns>
    Task<decimal> GetStockValueAsync(int productId);

    /// <summary>
    /// Calculates the average unit cost of current stock for a specific product.
    /// </summary>
    /// <param name="productId">The identifier of the product.</param>
    /// <returns>A task that represents the asynchronous operation containing the average unit cost.</returns>
    Task<decimal> GetAverageUnitCostAsync(int productId);

    #endregion

    #region Stock History & Analysis

    /// <summary>
    /// Retrieves the stock movement history for a specific product within an optional date range.
    /// </summary>
    /// <param name="productId">The identifier of the product.</param>
    /// <param name="from">The optional start date for filtering movements.</param>
    /// <param name="to">The optional end date for filtering movements.</param>
    /// <returns>A task that represents the asynchronous operation containing the list of stock movement summaries.</returns>
    Task<List<StockMovementSummary>> GetStockHistoryAsync(
        int productId, 
        DateTime? from = null, 
        DateTime? to = null);

    /// <summary>
    /// Retrieves a list of products that have stock levels below their minimum threshold.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation containing the list of low stock products.</returns>
    Task<List<ProductStockSummary>> GetLowStockProductsAsync();

    /// <summary>
    /// Retrieves a comprehensive stock summary for products, optionally filtered by category and activity status.
    /// </summary>
    /// <param name="categoryId">The optional category identifier to filter products.</param>
    /// <param name="includeInactive">Whether to include inactive products in the results.</param>
    /// <returns>A task that represents the asynchronous operation containing the list of product stock summaries.</returns>
    Task<List<ProductStockSummary>> GetProductStockSummaryAsync(
        int? categoryId = null, 
        bool includeInactive = false);

    #endregion

    #region Validation

    /// <summary>
    /// Validates whether a stock movement can be performed based on current stock levels and business rules.
    /// </summary>
    /// <param name="productId">The identifier of the product.</param>
    /// <param name="movementTypeId">The identifier of the movement type.</param>
    /// <param name="quantity">The quantity to be moved.</param>
    /// <returns>A task that represents the asynchronous operation containing true if the movement is valid, false otherwise.</returns>
    Task<bool> ValidateStockMovementAsync(int productId, int movementTypeId, int quantity);

    /// <summary>
    /// Retrieves detailed validation error messages for a stock movement.
    /// </summary>
    /// <param name="productId">The identifier of the product.</param>
    /// <param name="movementTypeId">The identifier of the movement type.</param>
    /// <param name="quantity">The quantity to be moved.</param>
    /// <returns>A task that represents the asynchronous operation containing a list of validation error messages.</returns>
    Task<List<string>> GetStockValidationErrorsAsync(int productId, int movementTypeId, int quantity);

    #endregion

    #region Stock Projections

    /// <summary>
    /// Projects the stock level for a specific product at a future date based on scheduled movements.
    /// </summary>
    /// <param name="productId">The identifier of the product.</param>
    /// <param name="projectionDate">The date for which to project stock levels.</param>
    /// <returns>A task that represents the asynchronous operation containing the projected stock quantity.</returns>
    Task<int> GetProjectedStockAsync(int productId, DateTime projectionDate);

    /// <summary>
    /// Determines whether a specific quantity will be available in stock by a certain date.
    /// </summary>
    /// <param name="productId">The identifier of the product.</param>
    /// <param name="quantity">The quantity to check availability for.</param>
    /// <param name="byDate">The optional date by which the stock should be available. If null, checks current availability.</param>
    /// <returns>A task that represents the asynchronous operation containing true if the quantity will be available, false otherwise.</returns>
    Task<bool> WillBeInStockAsync(int productId, int quantity, DateTime? byDate = null);

    #endregion
}