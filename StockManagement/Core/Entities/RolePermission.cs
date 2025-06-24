using Core.Entities.Base;
using Core.Enums;

namespace Core.Entities;

/// <summary>
/// Represents a many-to-many relationship between user roles and permissions.
/// Maps which permissions are granted to each user role within a tenant.
/// </summary>
public class RolePermission : TenantEntity, IEntity<int>
{
    /// <summary>
    /// Gets or sets the unique identifier for the role-permission mapping.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the user role that is granted the permission.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the permission being granted.
    /// </summary>
    public int PermissionId { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the associated permission.
    /// </summary>
    public Permission Permission { get; set; } = null!;
}