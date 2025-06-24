namespace Core.DTOs;

/// <summary>
/// Data transfer object containing all information required to create a new tenant 
/// and its initial administrator account in the multi-tenant Stock Management system.
/// </summary>
public class CreateTenantRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the tenant.
    /// This will be used for data isolation and tenant identification.
    /// </summary>
    public string TenantId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the display name of the tenant (e.g., company or shop name).
    /// </summary>
    public string TenantName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the subdomain that will be used to access this tenant.
    /// Must be unique across the system and follow subdomain naming conventions.
    /// </summary>
    public string SubDomain { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the email address for the tenant administrator account.
    /// This will be used for initial login and system notifications.
    /// </summary>
    public string AdminEmail { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the password for the tenant administrator account.
    /// Should meet system password requirements for security.
    /// </summary>
    public string AdminPassword { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the first name of the tenant administrator.
    /// </summary>
    public string AdminFirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the last name of the tenant administrator.
    /// </summary>
    public string AdminLastName { get; set; } = string.Empty;
}