using Core.Entities;
using Core.Enums;
using Core.Interfaces.Repositories;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for user-specific operations.
/// Extends the generic repository with user-specific business logic.
/// </summary>
public interface IUserRepository : IGenericRepository<User, int>
{
    /// <summary>
    /// Gets a user by their email address.
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByEmailAsync(string email);
    
    /// <summary>
    /// Gets a user by their email address within a specific tenant.
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="tenantId">The tenant identifier</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByEmailAndTenantAsync(string email, string tenantId);
    
    /// <summary>
    /// Gets all users belonging to a specific tenant.
    /// </summary>
    /// <param name="tenantId">The tenant identifier</param>
    /// <returns>Collection of users in the specified tenant</returns>
    Task<IEnumerable<User>> GetByTenantIdAsync(string tenantId);
    
    /// <summary>
    /// Gets all users with a specific role.
    /// </summary>
    /// <param name="role">The user role to filter by</param>
    /// <returns>Collection of users with the specified role</returns>
    Task<IEnumerable<User>> GetByRoleAsync(UserRole role);
    
    /// <summary>
    /// Checks if an email address is unique within a tenant.
    /// </summary>
    /// <param name="email">The email address to check</param>
    /// <param name="tenantId">The tenant identifier</param>
    /// <param name="excludeUserId">Optional user ID to exclude from the check (for updates)</param>
    /// <returns>True if the email is unique within the tenant, false otherwise</returns>
    Task<bool> IsEmailUniqueInTenantAsync(string email, string tenantId, int? excludeUserId = null);
    
    /// <summary>
    /// Updates the last login timestamp for a user.
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <param name="loginTime">The login timestamp</param>
    /// <returns>Task representing the update operation</returns>
    Task UpdateLastLoginAsync(int userId, DateTime loginTime);
}