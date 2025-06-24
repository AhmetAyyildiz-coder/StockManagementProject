namespace Core.DTOs;

/// <summary>
/// Information DTO containing permission details with assignment status.
/// </summary>
public class PermissionInfo
{
    /// <summary>
    /// Gets or sets the unique identifier for the permission.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name of the permission.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique code identifier for the permission.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the module or feature area this permission belongs to.
    /// </summary>
    public string Module { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this permission is currently assigned.
    /// </summary>
    public bool IsAssigned { get; set; }
}