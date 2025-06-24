namespace Core.DTOs;

/// <summary>
/// Information DTO for permission details with assignment status.
/// Used in role-permission summaries and permission management scenarios.
/// </summary>
public class PermissionInfo
{
    /// <summary>
    /// Gets or sets the unique identifier of the permission.
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
    /// Gets or sets the module or area this permission belongs to.
    /// Examples: "Stock", "Product", "User", "Settings"
    /// </summary>
    public string Module { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this permission is assigned to the current context.
    /// Used in role-permission management scenarios to show assignment status.
    /// </summary>
    public bool IsAssigned { get; set; }
}