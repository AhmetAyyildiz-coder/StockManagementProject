namespace Core.DTOs;

/// <summary>
/// Request DTO for updating an existing permission.
/// </summary>
public class UpdatePermissionRequest
{
    /// <summary>
    /// Gets or sets the human-readable name of the permission.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets an optional description explaining what this permission allows.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the module or feature area this permission belongs to.
    /// </summary>
    public string Module { get; set; } = string.Empty;
}