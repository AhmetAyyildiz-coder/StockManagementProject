namespace Core.Exceptions;

/// <summary>
/// Exception thrown when a stock movement operation fails due to insufficient available stock.
/// Contains detailed information about the stock shortage for business logic and user feedback.
/// </summary>
public class InsufficientStockException : DomainException
{
    /// <summary>
    /// Gets the identifier of the product that has insufficient stock.
    /// </summary>
    public int ProductId { get; }

    /// <summary>
    /// Gets the quantity that was requested in the operation.
    /// </summary>
    public int RequestedQuantity { get; }

    /// <summary>
    /// Gets the actual quantity available in stock.
    /// </summary>
    public int AvailableQuantity { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InsufficientStockException"/> class.
    /// </summary>
    /// <param name="productId">The identifier of the product with insufficient stock.</param>
    /// <param name="requestedQuantity">The quantity that was requested.</param>
    /// <param name="availableQuantity">The actual quantity available in stock.</param>
    public InsufficientStockException(int productId, int requestedQuantity, int availableQuantity)
        : base($"Insufficient stock for product {productId}. Requested: {requestedQuantity}, Available: {availableQuantity}", "INSUFFICIENT_STOCK")
    {
        ProductId = productId;
        RequestedQuantity = requestedQuantity;
        AvailableQuantity = availableQuantity;
    }
}