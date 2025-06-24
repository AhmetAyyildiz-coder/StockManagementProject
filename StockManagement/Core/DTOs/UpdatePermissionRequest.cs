namespace Core.DTOs;

/// <summary>
/// Request DTO for updating an existing permission in the system.
/// Contains the modifiable fields for permission updates.
/// </summary>
public class UpdatePermissionRequest
{
    /// <summary>
    /// Gets or sets the human-readable name of the permission.
    /// Example: "Hareket Tiplerini Yönet"
    /// </summary>
    public string Name { get; set; } = string.Empty;

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