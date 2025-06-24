using Core.Entities.Base;

namespace Core.Entities;

/// <summary>
/// Represents a stock movement transaction in the inventory management system.
/// Tracks all inventory changes with audit trail and reference information.
/// Inherits from TenantEntity to ensure proper tenant isolation.
/// </summary>
public class StockMovement : TenantEntity, IEntity<int>
{
    /// <summary>
    /// Gets or sets the unique identifier for the stock movement.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the product identifier for this movement.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the movement.
    /// This value is always positive - the direction is determined by MovementType.Direction.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the movement type identifier.
    /// References the MovementType that defines the nature of this movement.
    /// </summary>
    public int MovementTypeId { get; set; }

    /// <summary>
    /// Gets or sets the external reference for this movement.
    /// Examples: invoice number, order number, transfer reference, etc.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Gets or sets additional notes about this movement.
    /// Optional field for providing context or additional information.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the unit price at the time of movement.
    /// Used for calculating stock value and cost tracking.
    /// </summary>
    public decimal? UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the supplier identifier for incoming movements.
    /// Used to track which supplier provided the stock for purchase movements.
    /// </summary>
    public int? SupplierId { get; set; }

    /// <summary>
    /// Gets or sets the user identifier who created this movement.
    /// Required for audit trail and accountability.
    /// </summary>
    public int CreatedByUserId { get; set; }

    // Navigation Properties

    /// <summary>
    /// Gets or sets the product associated with this movement.
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Gets or sets the movement type that defines this movement's behavior.
    /// </summary>
    public MovementType MovementType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the supplier associated with this movement (for incoming movements).
    /// </summary>
    public Supplier? Supplier { get; set; }

    /// <summary>
    /// Gets or sets the user who created this movement.
    /// </summary>
    public User CreatedByUser { get; set; } = null!;

    // Computed Properties

    /// <summary>
    /// Gets the calculated quantity considering the movement direction.
    /// Positive values increase stock, negative values decrease stock.
    /// </summary>
    public int CalculatedQuantity => Quantity * MovementType.Direction;
}