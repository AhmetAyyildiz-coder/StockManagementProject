using Core.Entities;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for permission-specific operations.
/// Extends the generic repository with permission-specific business logic.
/// </summary>
public interface IPermissionRepository : IGenericRepository<Permission, int>
{
    /// <summary>
    /// Gets all permissions for a specific module.
    /// </summary>
    /// <param name="module">The module to filter permissions by.</param>
    /// <returns>A collection of permissions for the specified module.</returns>
    Task<IEnumerable<Permission>> GetByModuleAsync(string module);

    /// <summary>
    /// Gets a permission by its unique code within the current tenant.
    /// </summary>
    /// <param name="code">The permission code to search for.</param>
    /// <returns>The permission if found, null otherwise.</returns>
    Task<Permission?> GetByCodeAsync(string code);

    /// <summary>
    /// Gets all system-defined permissions within the current tenant.
    /// </summary>
    /// <returns>A collection of system-defined permissions.</returns>
    Task<IEnumerable<Permission>> GetSystemDefinedPermissionsAsync();

    /// <summary>
    /// Checks if a permission code is unique within the tenant.
    /// </summary>
    /// <param name="code">The permission code to check.</param>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="excludeId">Optional permission ID to exclude from the check (for updates).</param>
    /// <returns>True if the code is unique, false otherwise.</returns>
    Task<bool> IsCodeUniqueInTenantAsync(string code, string tenantId, int? excludeId = null);
}