using Core.Entities.Base;

namespace Core.Entities;

/// <summary>
/// Represents a tenant entity in the multi-tenant Stock Management system.
/// Placeholder entity to support interface contracts - full implementation in Issue #002.
/// </summary>
public class Tenant : TenantEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the tenant.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the tenant's display name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tenant's unique identifier used for data isolation.
    /// This should match the TenantId from the base class.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the tenant is currently active.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
}