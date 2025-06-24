namespace Core.DTOs;

/// <summary>
/// Request DTO for creating a new permission.
/// </summary>
public class CreatePermissionRequest
{
    /// <summary>
    /// Gets or sets the human-readable name of the permission.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique code identifier for the permission.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets an optional description explaining what this permission allows.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the module or feature area this permission belongs to.
    /// </summary>
    public string Module { get; set; } = string.Empty;
}