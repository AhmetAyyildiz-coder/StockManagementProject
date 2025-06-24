using Core.Entities.Base;

namespace Core.Entities;

/// <summary>
/// Represents a permission entity in the role-based authorization system.
/// Defines specific capabilities that can be granted to user roles within a tenant.
/// </summary>
public class Permission : TenantEntity, IEntity<int>
{
    /// <summary>
    /// Gets or sets the unique identifier for the permission.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name of the permission.
    /// Example: "Hareket Tiplerini Yönet"
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique code identifier for the permission.
    /// Used for programmatic permission checks.
    /// Example: "MANAGE_MOVEMENT_TYPES"
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets an optional description explaining what this permission allows.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the module or feature area this permission belongs to.
    /// Examples: "Stock", "Product", "User", "Settings"
    /// </summary>
    public string Module { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this permission is system-defined.
    /// System-defined permissions cannot be deleted and are created during tenant initialization.
    /// </summary>
    public bool IsSystemDefined { get; set; } = false;

    /// <summary>
    /// Gets or sets the navigation property for role-permission mappings.
    /// </summary>
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}