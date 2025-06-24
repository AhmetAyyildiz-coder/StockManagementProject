using Core.Entities.Base;

namespace Core.Entities;

/// <summary>
/// Represents a stock movement transaction in the multi-tenant stock management system.
/// Records all changes to product inventory levels with full audit tracking.
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
    /// Gets or sets the identifier of the product being moved.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product being moved.
    /// This value is always positive - the direction is determined by the MovementType.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the movement type that defines the nature of this movement.
    /// </summary>
    public int MovementTypeId { get; set; }

    /// <summary>
    /// Gets or sets an optional reference document or transaction identifier.
    /// Examples include invoice numbers, purchase order numbers, or batch codes.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Gets or sets optional notes or comments about this movement.
    /// Optional field for providing context or additional information.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product at the time of movement.
    /// Required for incoming movements to calculate stock valuation.
    /// Used for calculating stock value and cost tracking.
    /// </summary>
    public decimal? UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the supplier for incoming movements.
    /// Optional field used to track the source of purchased products.
    /// </summary>
    public int? SupplierId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created this movement.
    /// Required for audit trail and accountability.
    /// </summary>
    public int? CreatedByUserId { get; set; }

    /// <summary>
    /// Gets the calculated quantity change considering the movement direction.
    /// Positive for incoming movements, negative for outgoing movements.
    /// </summary>
    public int CalculatedQuantity => MovementType?.Direction * Quantity ?? 0;

    // Navigation Properties

    /// <summary>
    /// Gets or sets the product associated with this movement.
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Gets or sets the movement type that defines the nature of this movement.
    /// </summary>
    public MovementType MovementType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the supplier associated with this movement (for incoming movements).
    /// </summary>
    public Supplier? Supplier { get; set; }

    /// <summary>
    /// Gets or sets the user who created this movement.
    /// </summary>
    public User? CreatedByUser { get; set; }
}