namespace Core.Exceptions;

/// <summary>
/// Exception thrown when a requested tenant cannot be found or does not exist in the system.
/// Used in multi-tenant scenarios for tenant validation and access control.
/// </summary>
public class TenantNotFoundException : DomainException
{
    /// <summary>
    /// Gets the tenant identifier that was not found.
    /// </summary>
    public string TenantId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TenantNotFoundException"/> class with the specified tenant identifier.
    /// </summary>
    /// <param name="tenantId">The identifier of the tenant that was not found.</param>
    public TenantNotFoundException(string tenantId) 
        : base($"Tenant '{tenantId}' bulunamadı.")
    {
        TenantId = tenantId;
    }
}