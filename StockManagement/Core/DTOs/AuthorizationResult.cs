namespace Core.DTOs;

/// <summary>
/// Represents the result of an authorization check operation.
/// Provides detailed information about authorization success or failure.
/// </summary>
public class AuthorizationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the authorization was successful.
    /// </summary>
    public bool IsAuthorized { get; set; }

    /// <summary>
    /// Gets or sets the reason for authorization failure.
    /// Null or empty if authorization was successful.
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// Gets or sets the list of permissions that were required for the operation.
    /// Useful for understanding what permissions are needed.
    /// </summary>
    public List<string> RequiredPermissions { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of permissions that the user is missing.
    /// Only populated when authorization fails due to missing permissions.
    /// </summary>
    public List<string> MissingPermissions { get; set; } = new();
}