namespace Core.Services;

/// <summary>
/// Service interface for managing user context and authentication state.
/// Provides access to current user information for audit and authorization purposes.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the identifier of the current authenticated user.
    /// Used for audit tracking and authorization decisions.
    /// </summary>
    string? UserId { get; }
    
    /// <summary>
    /// Gets the username or email of the current authenticated user.
    /// </summary>
    string? Username { get; }
    
    /// <summary>
    /// Gets a value indicating whether a user is currently authenticated.
    /// </summary>
    bool IsAuthenticated { get; }
    
    /// <summary>
    /// Gets the roles assigned to the current user.
    /// Used for role-based authorization.
    /// </summary>
    IEnumerable<string> Roles { get; }
    
    /// <summary>
    /// Gets the tenant ID of the current user.
    /// Ensures the user belongs to the current tenant context.
    /// </summary>
    string? TenantId { get; }
    
    /// <summary>
    /// Checks if the current user has a specific role.
    /// </summary>
    /// <param name="role">The role to check</param>
    /// <returns>True if the user has the specified role</returns>
    bool HasRole(string role);
    
    /// <summary>
    /// Checks if the current user has any of the specified roles.
    /// </summary>
    /// <param name="roles">The roles to check</param>
    /// <returns>True if the user has any of the specified roles</returns>
    bool HasAnyRole(params string[] roles);
}