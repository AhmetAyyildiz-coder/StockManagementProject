namespace Core.DTOs;

/// <summary>
/// Data transfer object containing information required to create a tenant administrator account.
/// Used when setting up the initial admin user for a new tenant.
/// </summary>
public class CreateTenantAdminRequest
{
    /// <summary>
    /// Gets or sets the email address for the tenant administrator account.
    /// This will be used for authentication and system notifications.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the password for the tenant administrator account.
    /// Should meet system password requirements for security.
    /// </summary>
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the first name of the tenant administrator.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the last name of the tenant administrator.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
}