using Core.Entities.Base;
using Core.Entities.Enums;

namespace Core.Entities;

/// <summary>
/// Represents a user within a tenant in the multi-tenant system.
/// Inherits from TenantEntity to ensure proper tenant isolation.
/// </summary>
public class User : TenantEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// Used for authentication and communication.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hashed password for user authentication.
    /// Should never store plain text passwords.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the role of the user within the tenant.
    /// Determines the user's permissions and access level.
    /// </summary>
    public UserRole Role { get; set; } = UserRole.TenantAdmin;

    /// <summary>
    /// Gets or sets the date and time of the user's last login.
    /// Null indicates the user has never logged in.
    /// </summary>
    public DateTime? LastLoginAt { get; set; }

    // Navigation Properties

    /// <summary>
    /// Gets or sets the tenant that this user belongs to.
    /// </summary>
    public Tenant Tenant { get; set; } = null!;

    // Helper Methods

    /// <summary>
    /// Gets the full name of the user by combining first and last names.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Determines whether the user can manage movement types based on their role.
    /// Only TenantAdmin and Manager roles have this permission.
    /// </summary>
    /// <returns>True if the user can manage movement types, false otherwise.</returns>
    public bool CanManageMovementTypes() => 
        Role == UserRole.TenantAdmin || Role == UserRole.Manager;
}