using Core.Entities;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for permission-specific operations.
/// Extends the generic repository with permission-specific business logic and queries.
/// </summary>
public interface IPermissionRepository : IGenericRepository<Permission, int>
{
    /// <summary>
    /// Gets all permissions belonging to a specific module.
    /// </summary>
    /// <param name="module">The module name to filter by (e.g., "Stock", "Product", "User")</param>
    /// <returns>Collection of permissions in the specified module</returns>
    Task<IEnumerable<Permission>> GetByModuleAsync(string module);

    /// <summary>
    /// Gets a permission by its unique code identifier.
    /// </summary>
    /// <param name="code">The permission code to search for</param>
    /// <returns>The permission if found, null otherwise</returns>
    Task<Permission?> GetByCodeAsync(string code);

    /// <summary>
    /// Gets all system-defined permissions that cannot be deleted.
    /// These permissions are created during tenant initialization.
    /// </summary>
    /// <returns>Collection of system-defined permissions</returns>
    Task<IEnumerable<Permission>> GetSystemDefinedPermissionsAsync();

    /// <summary>
    /// Checks if a permission code is unique within a tenant.
    /// </summary>
    /// <param name="code">The permission code to check</param>
    /// <param name="tenantId">The tenant identifier</param>
    /// <param name="excludeId">Optional permission ID to exclude from the check (for updates)</param>
    /// <returns>True if the code is unique within the tenant, false otherwise</returns>
    Task<bool> IsCodeUniqueInTenantAsync(string code, string tenantId, int? excludeId = null);
}