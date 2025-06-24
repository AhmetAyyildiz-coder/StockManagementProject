using Core.DTOs;
using Core.Entities;
using Core.Enums;

namespace Core.Interfaces.Services;

/// <summary>
/// Service interface for permission management operations.
/// Provides CRUD operations for permissions and role-permission assignments.
/// </summary>
public interface IPermissionService
{
    /// <summary>
    /// Creates a new permission.
    /// </summary>
    /// <param name="request">The create permission request.</param>
    Task CreatePermissionAsync(CreatePermissionRequest request);

    /// <summary>
    /// Updates an existing permission.
    /// </summary>
    /// <param name="id">The permission ID to update.</param>
    /// <param name="request">The update permission request.</param>
    /// <returns>The updated permission.</returns>
    Task<Permission> UpdatePermissionAsync(int id, UpdatePermissionRequest request);

    /// <summary>
    /// Deletes a permission if it is not system-defined.
    /// </summary>
    /// <param name="id">The permission ID to delete.</param>
    /// <returns>True if the permission was deleted, false if it couldn't be deleted.</returns>
    Task<bool> DeletePermissionAsync(int id);

    /// <summary>
    /// Gets a permission by its ID.
    /// </summary>
    /// <param name="id">The permission ID to retrieve.</param>
    /// <returns>The permission if found, null otherwise.</returns>
    Task<Permission?> GetPermissionAsync(int id);

    /// <summary>
    /// Gets all permissions, optionally filtered by module.
    /// </summary>
    /// <param name="module">Optional module filter.</param>
    /// <returns>A list of permissions.</returns>
    Task<List<Permission>> GetPermissionsAsync(string? module = null);

    /// <summary>
    /// Assigns multiple permissions to the specified role.
    /// </summary>
    /// <param name="role">The user role to assign permissions to.</param>
    /// <param name="permissionIds">The list of permission IDs to assign.</param>
    Task AssignPermissionsToRoleAsync(UserRole role, List<int> permissionIds);

    /// <summary>
    /// Gets all permissions associated with the specified role.
    /// </summary>
    /// <param name="role">The user role to get permissions for.</param>
    /// <returns>A list of permissions granted to the role.</returns>
    Task<List<Permission>> GetRolePermissionsAsync(UserRole role);

    /// <summary>
    /// Checks if the specified role has the given permission.
    /// </summary>
    /// <param name="role">The user role to check.</param>
    /// <param name="permissionCode">The permission code to check for.</param>
    /// <returns>True if the role has the permission, false otherwise.</returns>
    Task<bool> RoleHasPermissionAsync(UserRole role, string permissionCode);

    /// <summary>
    /// Initializes default permissions for a new tenant.
    /// </summary>
    /// <param name="tenantId">The tenant ID to initialize permissions for.</param>
    Task InitializeDefaultPermissionsAsync(string tenantId);

    /// <summary>
    /// Initializes default role-permission assignments for a new tenant.
    /// </summary>
    /// <param name="tenantId">The tenant ID to initialize role permissions for.</param>
    Task InitializeRolePermissionsAsync(string tenantId);

    /// <summary>
    /// Gets all permissions that are available for assignment to the specified role.
    /// </summary>
    /// <param name="role">The user role to get available permissions for.</param>
    /// <returns>A list of permissions that can be assigned to the role.</returns>
    Task<List<Permission>> GetAvailablePermissionsForRoleAsync(UserRole role);
}