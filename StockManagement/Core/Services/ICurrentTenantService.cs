namespace Core.Services;

/// <summary>
/// Service interface for managing current tenant context in multi-tenant applications.
/// Provides access to the current tenant information for data isolation.
/// </summary>
public interface ICurrentTenantService
{
    /// <summary>
    /// Gets the identifier of the current tenant.
    /// This value is used for data filtering and isolation.
    /// </summary>
    string TenantId { get; }
    
    /// <summary>
    /// Gets the name of the current tenant for display purposes.
    /// </summary>
    string? TenantName { get; }
    
    /// <summary>
    /// Gets a value indicating whether a valid tenant context is currently available.
    /// </summary>
    bool HasTenant { get; }
    
    /// <summary>
    /// Sets the current tenant context.
    /// This method is typically called by tenant resolution middleware.
    /// </summary>
    /// <param name="tenantId">The tenant identifier</param>
    /// <param name="tenantName">The tenant display name (optional)</param>
    void SetTenant(string tenantId, string? tenantName = null);
    
    /// <summary>
    /// Clears the current tenant context.
    /// </summary>
    void ClearTenant();
}