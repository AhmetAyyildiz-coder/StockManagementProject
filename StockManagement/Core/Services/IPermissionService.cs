using Core.DTOs;
using Core.Entities;
using Core.Enums;

namespace Core.Services;

/// <summary>
/// Service interface for permission management operations.
/// Provides CRUD operations for permissions and role-permission assignments.
/// </summary>
public interface IPermissionService
{
    #region Permission CRUD

    /// <summary>
    /// Creates a new permission in the system.
    /// </summary>
    /// <param name="request">The permission creation request containing permission details</param>
    /// <returns>Task representing the creation operation</returns>
    Task CreatePermissionAsync(CreatePermissionRequest request);

    /// <summary>
    /// Updates an existing permission with new information.
    /// </summary>
    /// <param name="id">The permission identifier to update</param>
    /// <param name="request">The permission update request containing new details</param>
    /// <returns>The updated permission entity</returns>
    Task<Permission> UpdatePermissionAsync(int id, UpdatePermissionRequest request);

    /// <summary>
    /// Deletes a permission from the system.
    /// System-defined permissions cannot be deleted.
    /// </summary>
    /// <param name="id">The permission identifier to delete</param>
    /// <returns>True if the permission was deleted successfully, false otherwise</returns>
    Task<bool> DeletePermissionAsync(int id);

    /// <summary>
    /// Gets a specific permission by its identifier.
    /// </summary>
    /// <param name="id">The permission identifier</param>
    /// <returns>The permission if found, null otherwise</returns>
    Task<Permission?> GetPermissionAsync(int id);

    /// <summary>
    /// Gets all permissions, optionally filtered by module.
    /// </summary>
    /// <param name="module">Optional module name to filter by</param>
    /// <returns>List of permissions matching the criteria</returns>
    Task<List<Permission>> GetPermissionsAsync(string? module = null);

    #endregion

    #region Role-Permission Management

    /// <summary>
    /// Assigns multiple permissions to a specific user role.
    /// Replaces existing permission assignments for the role.
    /// </summary>
    /// <param name="role">The user role to assign permissions to</param>
    /// <param name="permissionIds">List of permission identifiers to assign</param>
    /// <returns>Task representing the assignment operation</returns>
    Task AssignPermissionsToRoleAsync(UserRole role, List<int> permissionIds);

    /// <summary>
    /// Gets all permissions assigned to a specific user role.
    /// </summary>
    /// <param name="role">The user role to get permissions for</param>
    /// <returns>List of permissions assigned to the role</returns>
    Task<List<Permission>> GetRolePermissionsAsync(UserRole role);

    /// <summary>
    /// Checks if a specific role has a specific permission by permission code.
    /// </summary>
    /// <param name="role">The user role to check</param>
    /// <param name="permissionCode">The permission code to check for</param>
    /// <returns>True if the role has the permission, false otherwise</returns>
    Task<bool> RoleHasPermissionAsync(UserRole role, string permissionCode);

    #endregion

    #region System Operations

    /// <summary>
    /// Initializes default system permissions for a new tenant.
    /// Creates all system-defined permissions that are required for the system to function.
    /// </summary>
    /// <param name="tenantId">The tenant identifier to initialize permissions for</param>
    /// <returns>Task representing the initialization operation</returns>
    Task InitializeDefaultPermissionsAsync(string tenantId);

    /// <summary>
    /// Initializes default role-permission assignments for a new tenant.
    /// Sets up the default permissions for each user role based on system defaults.
    /// </summary>
    /// <param name="tenantId">The tenant identifier to initialize role permissions for</param>
    /// <returns>Task representing the initialization operation</returns>
    Task InitializeRolePermissionsAsync(string tenantId);

    /// <summary>
    /// Gets all permissions that are available to be assigned to a specific role.
    /// Excludes permissions that are already assigned to the role.
    /// </summary>
    /// <param name="role">The user role to get available permissions for</param>
    /// <returns>List of permissions available for assignment to the role</returns>
    Task<List<Permission>> GetAvailablePermissionsForRoleAsync(UserRole role);

    #endregion
}