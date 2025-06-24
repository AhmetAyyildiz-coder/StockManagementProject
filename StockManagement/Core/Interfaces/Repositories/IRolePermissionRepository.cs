using Core.Entities;
using Core.Enums;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for role-permission mapping operations.
/// Manages the many-to-many relationship between roles and permissions.
/// </summary>
public interface IRolePermissionRepository : IGenericRepository<RolePermission, int>
{
    /// <summary>
    /// Gets all role-permission mappings for the specified role.
    /// </summary>
    /// <param name="role">The user role to get mappings for.</param>
    /// <returns>A collection of role-permission mappings.</returns>
    Task<IEnumerable<RolePermission>> GetByRoleAsync(UserRole role);

    /// <summary>
    /// Gets all role-permission mappings for the specified permission.
    /// </summary>
    /// <param name="permissionId">The permission ID to get mappings for.</param>
    /// <returns>A collection of role-permission mappings.</returns>
    Task<IEnumerable<RolePermission>> GetByPermissionIdAsync(int permissionId);

    /// <summary>
    /// Gets all permissions associated with the specified role.
    /// </summary>
    /// <param name="role">The user role to get permissions for.</param>
    /// <returns>A collection of permissions granted to the role.</returns>
    Task<IEnumerable<Permission>> GetPermissionsForRoleAsync(UserRole role);

    /// <summary>
    /// Checks if the specified role has the given permission.
    /// </summary>
    /// <param name="role">The user role to check.</param>
    /// <param name="permissionCode">The permission code to check for.</param>
    /// <returns>True if the role has the permission, false otherwise.</returns>
    Task<bool> RoleHasPermissionAsync(UserRole role, string permissionCode);

    /// <summary>
    /// Gets all permission codes granted to the specified role.
    /// </summary>
    /// <param name="role">The user role to get permission codes for.</param>
    /// <returns>A list of permission codes granted to the role.</returns>
    Task<List<string>> GetPermissionCodesForRoleAsync(UserRole role);
}