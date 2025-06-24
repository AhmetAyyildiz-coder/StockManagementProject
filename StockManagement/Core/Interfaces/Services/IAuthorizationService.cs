using Core.DTOs;
using Core.Entities;
using Core.Enums;

namespace Core.Interfaces.Services;

/// <summary>
/// Service interface for authorization operations and permission management.
/// Provides comprehensive user authorization and role-based access control functionality.
/// </summary>
public interface IAuthorizationService
{
    /// <summary>
    /// Ensures the user can manage movement types, throwing an exception if not authorized.
    /// </summary>
    /// <param name="userId">The user ID to check authorization for.</param>
    /// <exception cref="UnauthorizedException">Thrown when the user lacks the required permission.</exception>
    Task CanUserManageMovementTypesAsync(int userId);

    /// <summary>
    /// Checks if the user can create stock movements with the specified movement type.
    /// </summary>
    /// <param name="userId">The user ID to check authorization for.</param>
    /// <param name="movementTypeId">The movement type ID to check authorization for.</param>
    /// <returns>True if the user can create the stock movement, false otherwise.</returns>
    Task<bool> CanUserCreateStockMovementAsync(int userId, int movementTypeId);

    /// <summary>
    /// Checks if the user has permission to view reports.
    /// </summary>
    /// <param name="userId">The user ID to check authorization for.</param>
    /// <returns>True if the user can view reports, false otherwise.</returns>
    Task<bool> CanUserViewReportsAsync(int userId);

    /// <summary>
    /// Checks if the user has the specified permission.
    /// </summary>
    /// <param name="userId">The user ID to check authorization for.</param>
    /// <param name="permissionCode">The permission code to check for.</param>
    /// <returns>True if the user has the permission, false otherwise.</returns>
    Task<bool> HasPermissionAsync(int userId, string permissionCode);

    /// <summary>
    /// Gets the current authenticated user.
    /// </summary>
    /// <returns>The current user.</returns>
    Task<User> GetCurrentUserAsync();

    /// <summary>
    /// Gets all permissions granted to the specified user through their role.
    /// </summary>
    /// <param name="userId">The user ID to get permissions for.</param>
    /// <returns>A list of permissions granted to the user.</returns>
    Task<List<Permission>> GetUserPermissionsAsync(int userId);

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
    /// Gets all permission codes granted to the specified user through their role.
    /// </summary>
    /// <param name="userId">The user ID to get permission codes for.</param>
    /// <returns>A list of permission codes granted to the user.</returns>
    Task<List<string>> GetUserPermissionCodesAsync(int userId);

    /// <summary>
    /// Creates a new permission.
    /// </summary>
    /// <param name="request">The create permission request.</param>
    /// <returns>The created permission.</returns>
    Task<Permission> CreatePermissionAsync(CreatePermissionRequest request);

    /// <summary>
    /// Assigns a permission to the specified role.
    /// </summary>
    /// <param name="role">The user role to assign the permission to.</param>
    /// <param name="permissionId">The permission ID to assign.</param>
    Task AssignPermissionToRoleAsync(UserRole role, int permissionId);

    /// <summary>
    /// Removes a permission from the specified role.
    /// </summary>
    /// <param name="role">The user role to remove the permission from.</param>
    /// <param name="permissionId">The permission ID to remove.</param>
    Task RemovePermissionFromRoleAsync(UserRole role, int permissionId);

    /// <summary>
    /// Gets all roles that have the specified permission.
    /// </summary>
    /// <param name="permissionCode">The permission code to search for.</param>
    /// <returns>A list of user roles that have the permission.</returns>
    Task<List<UserRole>> GetRolesWithPermissionAsync(string permissionCode);

    /// <summary>
    /// Validates user access to a resource with a specific action.
    /// </summary>
    /// <param name="userId">The user ID to validate access for.</param>
    /// <param name="resource">The resource being accessed.</param>
    /// <param name="action">The action being performed on the resource.</param>
    /// <returns>True if access is allowed, false otherwise.</returns>
    Task<bool> ValidateUserAccessAsync(int userId, string resource, string action);

    /// <summary>
    /// Performs a comprehensive authorization check and returns detailed results.
    /// </summary>
    /// <param name="userId">The user ID to check authorization for.</param>
    /// <param name="permissionCode">The permission code to check for.</param>
    /// <returns>An authorization result with detailed information.</returns>
    Task<AuthorizationResult> CheckAuthorizationAsync(int userId, string permissionCode);
}