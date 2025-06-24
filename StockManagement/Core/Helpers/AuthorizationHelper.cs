using Core.Entities;
using Core.Enums;
using Core.Exceptions;

namespace Core.Helpers;

/// <summary>
/// Static helper class providing common authorization operations and role-based access control.
/// Contains reusable methods for enforcing business rules and user permissions.
/// </summary>
public static class AuthorizationHelper
{
    /// <summary>
    /// Ensures that the specified user has permission to manage movement types.
    /// Throws an exception if the user lacks the required permissions.
    /// </summary>
    /// <param name="user">The user to check permissions for.</param>
    /// <exception cref="UnauthorizedException">Thrown when the user cannot manage movement types.</exception>
    public static void EnsureCanManageMovementTypes(User user)
    {
        if (!user.CanManageMovementTypes())
            throw new UnauthorizedException("Hareket tipi yönetme yetkiniz yok.");
    }

    /// <summary>
    /// Ensures that the specified user has the required role or higher.
    /// Role hierarchy is enforced where lower numeric values represent higher privileges.
    /// </summary>
    /// <param name="user">The user to check the role for.</param>
    /// <param name="requiredRole">The minimum required role.</param>
    /// <exception cref="UnauthorizedException">Thrown when the user lacks the required role.</exception>
    public static void EnsureHasRole(User user, UserRole requiredRole)
    {
        if (user.Role > requiredRole) // Higher numeric value = lower privilege
            throw new UnauthorizedException($"Bu işlem için {requiredRole} yetkisi gereklidir.");
    }

    /// <summary>
    /// Determines whether a user can access data from the specified tenant.
    /// SystemAdmin users can access all tenants, while other users are restricted to their own tenant.
    /// </summary>
    /// <param name="user">The user requesting access.</param>
    /// <param name="tenantId">The tenant identifier being accessed.</param>
    /// <returns>True if the user can access the tenant, false otherwise.</returns>
    public static bool CanUserAccessTenant(User user, string tenantId)
    {
        return user.TenantId == tenantId || user.Role == UserRole.SystemAdmin;
    }

    /// <summary>
    /// Ensures that the specified user can access the given tenant.
    /// Throws an exception if access is not allowed.
    /// </summary>
    /// <param name="user">The user requesting access.</param>
    /// <param name="tenantId">The tenant identifier being accessed.</param>
    /// <exception cref="UnauthorizedException">Thrown when the user cannot access the tenant.</exception>
    public static void EnsureCanAccessTenant(User user, string tenantId)
    {
        if (!CanUserAccessTenant(user, tenantId))
            throw new UnauthorizedException($"Tenant '{tenantId}' erişim yetkiniz yok.");
    }

    #region Permission-based Authorization Methods

    /// <summary>
    /// Ensures that the user has a specific permission based on their assigned permissions.
    /// Throws an exception if the user lacks the required permission.
    /// </summary>
    /// <param name="user">The user to check permissions for</param>
    /// <param name="userPermissions">The list of permissions assigned to the user</param>
    /// <param name="permissionCode">The permission code required for the operation</param>
    /// <exception cref="UnauthorizedException">Thrown when the user does not have the required permission</exception>
    public static void EnsureHasPermission(User user, List<Permission> userPermissions, string permissionCode)
    {
        if (!userPermissions.Any(p => p.Code == permissionCode))
            throw new UnauthorizedException($"Bu işlem için '{permissionCode}' yetkisi gereklidir.");
    }

    /// <summary>
    /// Checks if the user has any of the specified permissions.
    /// Useful for operations that can be performed with multiple different permissions.
    /// </summary>
    /// <param name="user">The user to check permissions for</param>
    /// <param name="userPermissions">The list of permissions assigned to the user</param>
    /// <param name="permissionCodes">The permission codes to check for (any one is sufficient)</param>
    /// <returns>True if the user has at least one of the specified permissions, false otherwise</returns>
    public static bool HasAnyPermission(User user, List<Permission> userPermissions, params string[] permissionCodes)
    {
        return permissionCodes.Any(code => userPermissions.Any(p => p.Code == code));
    }

    /// <summary>
    /// Checks if the user has all of the specified permissions.
    /// Useful for operations that require multiple permissions simultaneously.
    /// </summary>
    /// <param name="user">The user to check permissions for</param>
    /// <param name="userPermissions">The list of permissions assigned to the user</param>
    /// <param name="permissionCodes">The permission codes that are all required</param>
    /// <returns>True if the user has all of the specified permissions, false otherwise</returns>
    public static bool HasAllPermissions(User user, List<Permission> userPermissions, params string[] permissionCodes)
    {
        return permissionCodes.All(code => userPermissions.Any(p => p.Code == code));
    }

    #endregion

    #region Permission Validation

    /// <summary>
    /// Validates if a permission code follows the expected format and conventions.
    /// Permission codes should be uppercase, alphanumeric with underscores, and not exceed 50 characters.
    /// </summary>
    /// <param name="code">The permission code to validate</param>
    /// <returns>True if the permission code is valid, false otherwise</returns>
    public static bool IsValidPermissionCode(string code)
    {
        return !string.IsNullOrWhiteSpace(code) && 
               code.All(c => char.IsUpper(c) || char.IsDigit(c) || c == '_') &&
               code.Length <= 50;
    }

    #endregion

    #region Role Hierarchy

    /// <summary>
    /// Determines if the current role has higher privileges than the target role.
    /// In the role hierarchy, lower numeric values represent higher privileges.
    /// </summary>
    /// <param name="currentRole">The current user's role</param>
    /// <param name="targetRole">The target role to compare against</param>
    /// <returns>True if the current role has higher privileges than the target role, false otherwise</returns>
    public static bool IsHigherRole(UserRole currentRole, UserRole targetRole)
    {
        return (int)currentRole < (int)targetRole; // Lower number = higher privilege
    }

    #endregion
}