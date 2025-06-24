namespace Core.DTOs;

/// <summary>
/// Result DTO for authorization checks indicating whether access is granted and why.
/// </summary>
public class AuthorizationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the authorization check passed.
    /// </summary>
    public bool IsAuthorized { get; set; }

    /// <summary>
    /// Gets or sets the reason for authorization failure (if applicable).
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// Gets or sets the list of permissions that were required for this operation.
    /// </summary>
    public List<string> RequiredPermissions { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of permissions that the user is missing for this operation.
    /// </summary>
    public List<string> MissingPermissions { get; set; } = new();
}