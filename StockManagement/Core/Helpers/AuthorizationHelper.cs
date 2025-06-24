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
}