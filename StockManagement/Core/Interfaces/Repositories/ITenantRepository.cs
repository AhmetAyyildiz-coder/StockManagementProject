using Core.Entities;
using Core.Interfaces.Repositories;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for tenant-specific operations.
/// Extends the generic repository with tenant-specific business logic.
/// </summary>
public interface ITenantRepository : IGenericRepository<Tenant, int>
{
    /// <summary>
    /// Gets a tenant by its subdomain.
    /// </summary>
    /// <param name="subDomain">The subdomain to search for</param>
    /// <returns>The tenant if found, null otherwise</returns>
    Task<Tenant?> GetBySubDomainAsync(string subDomain);
    
    /// <summary>
    /// Checks if a subdomain is available for registration.
    /// </summary>
    /// <param name="subDomain">The subdomain to check</param>
    /// <returns>True if the subdomain is available, false otherwise</returns>
    Task<bool> IsSubDomainAvailableAsync(string subDomain);
    
    /// <summary>
    /// Gets all active tenants in the system.
    /// </summary>
    /// <returns>Collection of active tenants</returns>
    Task<IEnumerable<Tenant>> GetActiveTenantsAsync();
}