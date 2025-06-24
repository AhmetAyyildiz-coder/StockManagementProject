using Core.Entities;
using Core.Enums;

namespace Core.Services;

/// <summary>
/// Service interface for managing current user context and authentication state.
/// Provides access to current user information for audit and authorization purposes.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the current authenticated user asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that returns the current user.</returns>
    Task<User?> GetCurrentUserAsync();

    /// <summary>
    /// Gets the identifier of the current authenticated user.
    /// Used for audit tracking and authorization decisions.
    /// </summary>
    /// <returns>The current user's identifier, or null if not authenticated.</returns>
    string? GetCurrentUserId();

    /// <summary>
    /// Gets the tenant identifier of the current user.
    /// Ensures the user belongs to the current tenant context.
    /// </summary>
    /// <returns>The current user's tenant identifier.</returns>
    string GetCurrentTenantId();

    /// <summary>
    /// Gets a value indicating whether a user is currently authenticated.
    /// </summary>
    /// <returns>True if a user is authenticated, false otherwise.</returns>
    bool IsAuthenticated();

    /// <summary>
    /// Checks if the current user has the specified role.
    /// </summary>
    /// <param name="role">The role to check for.</param>
    /// <returns>True if the user has the specified role, false otherwise.</returns>
    bool HasRole(UserRole role);

    /// <summary>
    /// Determines if the current user can manage movement types.
    /// This is typically restricted to Manager role and above.
    /// </summary>
    /// <returns>True if the user can manage movement types, false otherwise.</returns>
    bool CanManageMovementTypes();
}