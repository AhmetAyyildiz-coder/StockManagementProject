using Core.Entities.Base;

namespace Core.Entities;

/// <summary>
/// Represents a parametric stock movement type configuration in the multi-tenant stock management system.
/// Allows tenants to define custom movement types while maintaining system-defined types.
/// Defines how different types of stock movements should be processed and categorized.
/// Inherits from TenantEntity to ensure proper tenant isolation.
/// </summary>
public class MovementType : TenantEntity, IEntity<int>
{
    /// <summary>
    /// Gets or sets the unique identifier for the movement type.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the display name of the movement type.
    /// Examples: "Mal Alımı", "Satış", "Fire", "Sayım Düzeltmesi"
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique code identifier for the movement type within the tenant.
    /// Examples: "PURCHASE", "SALE", "LOSS", "ADJUSTMENT"
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the direction of stock change for this movement type.
    /// +1 indicates stock increase (incoming), -1 indicates stock decrease (outgoing).
    /// </summary>
    public int Direction { get; set; }

    /// <summary>
    /// Gets or sets an optional description explaining when this movement type should be used.
    /// Provides additional context for the movement type usage.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this movement type is system-defined.
    /// System-defined movement types cannot be modified or deleted by tenant users.
    /// </summary>
    public bool IsSystemDefined { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether movements of this type require manager approval.
    /// Used for high-value or sensitive stock movements.
    /// </summary>
    public bool RequiresManagerApproval { get; set; } = false;

    /// <summary>
    /// Gets or sets the identifier of the user who created this movement type.
    /// Null for system-defined movement types.
    /// </summary>
    public int? CreatedByUserId { get; set; }

    // Navigation Properties

    /// <summary>
    /// Gets or sets the collection of stock movements using this movement type.
    /// </summary>
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    /// <summary>
    /// Gets or sets the user who created this movement type.
    /// </summary>
    public User? CreatedByUser { get; set; }
}