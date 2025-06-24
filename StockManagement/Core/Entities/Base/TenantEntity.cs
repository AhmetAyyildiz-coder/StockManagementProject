namespace Core.Entities.Base;

/// <summary>
/// Abstract base class for all tenant-aware entities in the multi-tenant system.
/// Provides common properties for tenant isolation, audit tracking, and soft delete functionality.
/// </summary>
public abstract class TenantEntity
{
    /// <summary>
    /// Gets or sets the tenant identifier for data isolation in multi-tenant architecture.
    /// This property is used to segregate data between different tenants.
    /// </summary>
    public string TenantId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// Defaults to UTC now when the entity is instantiated.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// Null indicates the entity has never been updated since creation.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is active.
    /// Used for soft delete functionality - false indicates the entity is logically deleted.
    /// Defaults to true for new entities.
    /// </summary>
    public bool IsActive { get; set; } = true;
}