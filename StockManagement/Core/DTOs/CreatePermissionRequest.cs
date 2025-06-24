namespace Core.DTOs;

/// <summary>
/// Request DTO for creating a new permission in the system.
/// Contains the required information to define a new permission.
/// </summary>
public class CreatePermissionRequest
{
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
}