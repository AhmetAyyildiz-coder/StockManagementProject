using Core.Enums;

namespace Core.DTOs;

/// <summary>
/// Summary DTO containing role information and associated permissions.
/// Provides a comprehensive view of what permissions are assigned to a role.
/// </summary>
public class RolePermissionSummary
{
    /// <summary>
    /// Gets or sets the user role this summary is for.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name of the role.
    /// </summary>
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of permissions associated with this role.
    /// Includes both assigned and available permissions with assignment status.
    /// </summary>
    public List<PermissionInfo> Permissions { get; set; } = new();

    /// <summary>
    /// Gets or sets the total number of permissions assigned to this role.
    /// </summary>
    public int TotalPermissions { get; set; }
}