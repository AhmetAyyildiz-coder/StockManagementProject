using Core.Entities;
using Core.Enums;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for role-permission mapping operations.
/// Manages the many-to-many relationship between user roles and permissions.
/// </summary>
public interface IRolePermissionRepository : IGenericRepository<RolePermission, int>
{
    /// <summary>
    /// Gets all role-permission mappings for a specific user role.
    /// </summary>
    /// <param name="role">The user role to get mappings for</param>
    /// <returns>Collection of role-permission mappings for the specified role</returns>
    Task<IEnumerable<RolePermission>> GetByRoleAsync(UserRole role);

    /// <summary>
    /// Gets all role-permission mappings for a specific permission.
    /// </summary>
    /// <param name="permissionId">The permission identifier</param>
    /// <returns>Collection of role-permission mappings for the specified permission</returns>
    Task<IEnumerable<RolePermission>> GetByPermissionIdAsync(int permissionId);

    /// <summary>
    /// Gets all permissions assigned to a specific user role.
    /// Includes the full permission entities, not just the mappings.
    /// </summary>
    /// <param name="role">The user role to get permissions for</param>
    /// <returns>Collection of permissions assigned to the specified role</returns>
    Task<IEnumerable<Permission>> GetPermissionsForRoleAsync(UserRole role);

    /// <summary>
    /// Checks if a specific role has a specific permission by permission code.
    /// </summary>
    /// <param name="role">The user role to check</param>
    /// <param name="permissionCode">The permission code to check for</param>
    /// <returns>True if the role has the permission, false otherwise</returns>
    Task<bool> RoleHasPermissionAsync(UserRole role, string permissionCode);

    /// <summary>
    /// Gets all permission codes assigned to a specific user role.
    /// Returns only the permission codes for efficient permission checking.
    /// </summary>
    /// <param name="role">The user role to get permission codes for</param>
    /// <returns>List of permission codes assigned to the specified role</returns>
    Task<List<string>> GetPermissionCodesForRoleAsync(UserRole role);
}