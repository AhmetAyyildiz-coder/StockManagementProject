using Core.Enums;

namespace Core.DTOs;

/// <summary>
/// Summary DTO containing role information and associated permissions.
/// </summary>
public class RolePermissionSummary
{
    /// <summary>
    /// Gets or sets the user role.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name of the role.
    /// </summary>
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of permissions associated with this role.
    /// </summary>
    public List<PermissionInfo> Permissions { get; set; } = new();

    /// <summary>
    /// Gets or sets the total count of permissions for this role.
    /// </summary>
    public int TotalPermissions { get; set; }
}