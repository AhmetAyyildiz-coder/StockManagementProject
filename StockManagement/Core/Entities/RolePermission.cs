using Core.Entities.Base;
using Core.Enums;

namespace Core.Entities;

/// <summary>
/// Represents the mapping between user roles and permissions in the multi-tenant authorization system.
/// This entity establishes the many-to-many relationship between UserRole enum and Permission entities.
/// </summary>
public class RolePermission : TenantEntity, IEntity<int>
{
    /// <summary>
    /// Gets or sets the unique identifier for the role-permission mapping.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the user role that has the permission.
    /// References the UserRole enumeration.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the permission assigned to the role.
    /// Foreign key reference to the Permission entity.
    /// </summary>
    public int PermissionId { get; set; }

    /// <summary>
    /// Gets or sets the permission entity associated with this mapping.
    /// Navigation property to the Permission entity.
    /// </summary>
    public Permission Permission { get; set; } = null!;
}