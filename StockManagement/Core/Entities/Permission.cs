using Core.Entities.Base;

namespace Core.Entities;

/// <summary>
/// Represents a permission entity in the multi-tenant authorization system.
/// Defines specific permissions that can be assigned to roles for access control.
/// </summary>
public class Permission : TenantEntity
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
    /// Used for programmatic access. Example: "MANAGE_MOVEMENT_TYPES"
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the optional description of what this permission allows.
    /// Provides additional context for administrators.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the module or area this permission belongs to.
    /// Examples: "Stock", "Product", "User", "Settings"
    /// </summary>
    public string Module { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this is a system-defined permission.
    /// System-defined permissions cannot be deleted and are created during tenant initialization.
    /// </summary>
    public bool IsSystemDefined { get; set; } = false;

    /// <summary>
    /// Gets or sets the collection of role-permission mappings.
    /// Navigation property for roles that have this permission.
    /// </summary>
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}