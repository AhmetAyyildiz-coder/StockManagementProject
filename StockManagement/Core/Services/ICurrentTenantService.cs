using Core.Entities;

namespace Core.Services;

/// <summary>
/// Service interface for managing current tenant context in multi-tenant applications.
/// Provides access to the current tenant information for data isolation and validation.
/// </summary>
public interface ICurrentTenantService
{
    /// <summary>
    /// Gets the identifier of the current tenant.
    /// This value is used for data filtering and isolation.
    /// </summary>
    string TenantId { get; }

    /// <summary>
    /// Gets the current tenant entity asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that returns the current tenant.</returns>
    Task<Tenant?> GetTenantAsync();

    /// <summary>
    /// Validates if the specified tenant identifier exists and is valid.
    /// </summary>
    /// <param name="tenantId">The tenant identifier to validate.</param>
    /// <returns>A task representing the asynchronous operation that returns true if the tenant is valid.</returns>
    Task<bool> ValidateTenantAsync(string tenantId);

    /// <summary>
    /// Gets a value indicating whether the current tenant context is valid.
    /// </summary>
    /// <returns>True if the current tenant is valid, false otherwise.</returns>
    bool IsValidTenant();
}