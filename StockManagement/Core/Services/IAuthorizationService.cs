using Core.DTOs;
using Core.Entities;
using Core.Enums;

namespace Core.Services;

/// <summary>
/// Service interface for authorization and permission management operations.
/// Provides comprehensive user authorization, role-based access control, and permission validation.
/// </summary>
public interface IAuthorizationService
{
    #region User Authorization

    /// <summary>
    /// Validates if a user can manage movement types based on their permissions.
    /// Throws an exception if the user lacks the required permissions.
    /// </summary>
    /// <param name="userId">The user identifier to check</param>
    /// <returns>Task representing the authorization check operation</returns>
    /// <exception cref="UnauthorizedException">Thrown when the user cannot manage movement types</exception>
    Task CanUserManageMovementTypesAsync(int userId);

    /// <summary>
    /// Checks if a user can create stock movements for a specific movement type.
    /// </summary>
    /// <param name="userId">The user identifier to check</param>
    /// <param name="movementTypeId">The movement type identifier</param>
    /// <returns>True if the user can create the stock movement, false otherwise</returns>
    Task<bool> CanUserCreateStockMovementAsync(int userId, int movementTypeId);

    /// <summary>
    /// Checks if a user can view reports and analytics.
    /// </summary>
    /// <param name="userId">The user identifier to check</param>
    /// <returns>True if the user can view reports, false otherwise</returns>
    Task<bool> CanUserViewReportsAsync(int userId);

    /// <summary>
    /// Checks if a user has a specific permission by permission code.
    /// </summary>
    /// <param name="userId">The user identifier to check</param>
    /// <param name="permissionCode">The permission code to check for</param>
    /// <returns>True if the user has the permission, false otherwise</returns>
    Task<bool> HasPermissionAsync(int userId, string permissionCode);

    /// <summary>
    /// Gets the current authenticated user.
    /// </summary>
    /// <returns>The current user if authenticated, null otherwise</returns>
    Task<User?> GetCurrentUserAsync();

    /// <summary>
    /// Gets all permissions assigned to a specific user through their role.
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <returns>List of permissions assigned to the user</returns>
    Task<List<Permission>> GetUserPermissionsAsync(int userId);

    #endregion

    #region Role-based Authorization

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

    /// <summary>
    /// Gets all permission codes assigned to a specific user.
    /// Efficient method for getting just the permission codes for permission checking.
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <returns>List of permission codes assigned to the user</returns>
    Task<List<string>> GetUserPermissionCodesAsync(int userId);

    #endregion

    #region Permission Management

    /// <summary>
    /// Creates a new permission in the system.
    /// </summary>
    /// <param name="request">The permission creation request</param>
    /// <returns>The created permission entity</returns>
    Task<Permission> CreatePermissionAsync(CreatePermissionRequest request);

    /// <summary>
    /// Assigns a permission to a specific user role.
    /// </summary>
    /// <param name="role">The user role to assign the permission to</param>
    /// <param name="permissionId">The permission identifier to assign</param>
    /// <returns>Task representing the assignment operation</returns>
    Task AssignPermissionToRoleAsync(UserRole role, int permissionId);

    /// <summary>
    /// Removes a permission from a specific user role.
    /// </summary>
    /// <param name="role">The user role to remove the permission from</param>
    /// <param name="permissionId">The permission identifier to remove</param>
    /// <returns>Task representing the removal operation</returns>
    Task RemovePermissionFromRoleAsync(UserRole role, int permissionId);

    /// <summary>
    /// Gets all user roles that have a specific permission.
    /// </summary>
    /// <param name="permissionCode">The permission code to check for</param>
    /// <returns>List of user roles that have the specified permission</returns>
    Task<List<UserRole>> GetRolesWithPermissionAsync(string permissionCode);

    #endregion

    #region Validation

    /// <summary>
    /// Validates user access to a specific resource and action.
    /// Generic method for resource-based authorization.
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <param name="resource">The resource being accessed</param>
    /// <param name="action">The action being performed</param>
    /// <returns>True if access is allowed, false otherwise</returns>
    Task<bool> ValidateUserAccessAsync(int userId, string resource, string action);

    /// <summary>
    /// Performs comprehensive authorization check and returns detailed result.
    /// Includes information about missing permissions and authorization failure reasons.
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <param name="permissionCode">The permission code to check</param>
    /// <returns>Detailed authorization result with success status and additional information</returns>
    Task<AuthorizationResult> CheckAuthorizationAsync(int userId, string permissionCode);

    #endregion
}