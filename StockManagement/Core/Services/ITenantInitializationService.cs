using Core.DTOs;
using Core.Entities;

namespace Core.Services;

/// <summary>
/// Service interface for tenant initialization and setup operations in the multi-tenant Stock Management system.
/// Handles the complete process of creating new tenants, setting up their data, and creating admin accounts.
/// </summary>
public interface ITenantInitializationService
{
    /// <summary>
    /// Creates a new tenant with all necessary initialization data and administrator account.
    /// This is a complete tenant setup operation that includes tenant creation, data initialization, 
    /// and admin user creation.
    /// </summary>
    /// <param name="request">The tenant creation request containing all necessary information.</param>
    /// <returns>A task representing the asynchronous tenant creation operation.</returns>
    /// <exception cref="ValidationException">Thrown when the request contains invalid data.</exception>
    /// <exception cref="DomainException">Thrown when tenant creation fails due to business rules.</exception>
    Task CreateTenantAsync(CreateTenantRequest request);
    
    /// <summary>
    /// Initializes the default data for a tenant, including default categories, 
    /// movement types, and role permissions.
    /// </summary>
    /// <param name="tenantId">The unique identifier of the tenant to initialize.</param>
    /// <returns>A task representing the asynchronous tenant data initialization operation.</returns>
    /// <exception cref="NotFoundException">Thrown when the tenant is not found.</exception>
    /// <exception cref="DomainException">Thrown when initialization fails.</exception>
    Task InitializeTenantDataAsync(string tenantId);
    
    /// <summary>
    /// Creates a tenant administrator account for the specified tenant.
    /// </summary>
    /// <param name="tenantId">The unique identifier of the tenant.</param>
    /// <param name="request">The admin creation request containing user details.</param>
    /// <returns>A task representing the asynchronous operation that returns the created user.</returns>
    /// <exception cref="NotFoundException">Thrown when the tenant is not found.</exception>
    /// <exception cref="ValidationException">Thrown when the request contains invalid data.</exception>
    /// <exception cref="DomainException">Thrown when user creation fails.</exception>
    Task<User> CreateTenantAdminAsync(string tenantId, CreateTenantAdminRequest request);
    
    /// <summary>
    /// Validates if a subdomain is available and follows the required naming conventions.
    /// </summary>
    /// <param name="subdomain">The subdomain to validate.</param>
    /// <returns>A task representing the asynchronous operation that returns true if the subdomain is valid and available.</returns>
    Task<bool> ValidateTenantSubdomainAsync(string subdomain);
}