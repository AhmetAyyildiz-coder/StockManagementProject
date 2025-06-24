using Core.Entities.Base;
using Core.Enums;

namespace Core.Entities;

/// <summary>
/// Represents a user entity in the multi-tenant Stock Management system.
/// Placeholder entity to support interface contracts - full implementation in Issue #002.
/// </summary>
public class User : TenantEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the username for authentication.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's role within their tenant.
    /// </summary>
    public UserRole Role { get; set; } = UserRole.ReadOnly;

    /// <summary>
    /// Gets or sets the hashed password for authentication.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Determines if the user can manage movement types based on their role.
    /// </summary>
    /// <returns>True if the user has Manager role or higher, false otherwise.</returns>
    public bool CanManageMovementTypes()
    {
        return Role <= UserRole.Manager;
    }
}