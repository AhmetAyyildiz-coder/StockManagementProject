namespace Core.DTOs;

/// <summary>
/// Data transfer object for creating a new stock movement.
/// Contains all the required information to record a stock movement transaction.
/// </summary>
public class CreateStockMovementRequest
{
    /// <summary>
    /// Gets or sets the identifier of the product being moved.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the movement type to be applied.
    /// </summary>
    public int MovementTypeId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product being moved.
    /// Should always be a positive number regardless of movement direction.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets an optional reference document or transaction identifier.
    /// Examples include invoice numbers, purchase order numbers, or batch codes.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Gets or sets optional notes or comments about this movement.
    /// Can be used to provide additional context or justification.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product at the time of movement.
    /// Required for incoming movements, optional for outgoing movements.
    /// </summary>
    public decimal? UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the supplier for incoming movements.
    /// Optional field used to track the source of purchased products.
    /// </summary>
    public int? SupplierId { get; set; }
}