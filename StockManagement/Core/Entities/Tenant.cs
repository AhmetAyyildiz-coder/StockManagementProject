using Core.Entities.Base;

namespace Core.Entities;

/// <summary>
/// Represents a tenant in the multi-tenant system.
/// Serves as the root entity for tenant isolation and contains all tenant-specific data.
/// </summary>
public class Tenant
{
    /// <summary>
    /// Gets or sets the unique identifier for the tenant.
    /// Uses the subdomain as the primary key for easy tenant resolution.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the display name of the tenant organization.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the subdomain used for tenant identification in URLs.
    /// Must be unique across the entire system.
    /// </summary>
    public string SubDomain { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the tenant is active.
    /// Inactive tenants cannot access the system.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the date and time when the tenant was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties

    /// <summary>
    /// Gets or sets the collection of users belonging to this tenant.
    /// </summary>
    public ICollection<User> Users { get; set; } = new List<User>();

    /// <summary>
    /// Gets or sets the collection of products belonging to this tenant.
    /// </summary>
    public ICollection<Product> Products { get; set; } = new List<Product>();
}