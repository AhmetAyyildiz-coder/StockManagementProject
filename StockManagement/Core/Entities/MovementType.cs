using Core.Entities.Base;

namespace Core.Entities;

/// <summary>
/// Represents a stock movement type configuration in the multi-tenant stock management system.
/// Defines how different types of stock movements should be processed and categorized.
/// </summary>
public class MovementType : TenantEntity, IEntity<int>
{
    /// <summary>
    /// Gets or sets the unique identifier of the movement type.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the display name of the movement type.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique code identifier for the movement type.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the direction of stock change for this movement type.
    /// Use +1 for stock increases (incoming) and -1 for stock decreases (outgoing).
    /// </summary>
    public int Direction { get; set; }

    /// <summary>
    /// Gets or sets an optional description explaining when this movement type should be used.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this movement type is system-defined.
    /// System-defined movement types cannot be modified or deleted by tenants.
    /// </summary>
    public bool IsSystemDefined { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether movements of this type require manager approval.
    /// </summary>
    public bool RequiresManagerApproval { get; set; } = false;

    /// <summary>
    /// Gets or sets the identifier of the user who created this movement type.
    /// </summary>
    public int? CreatedByUserId { get; set; }

    #region Navigation Properties

    /// <summary>
    /// Gets or sets the collection of stock movements that use this movement type.
    /// </summary>
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    /// <summary>
    /// Gets or sets the user who created this movement type.
    /// </summary>
    public User? CreatedByUser { get; set; }

    #endregion
}