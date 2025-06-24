namespace Core.Attributes;

/// <summary>
/// Attribute to require specific permissions for method or class access.
/// Can be applied to methods or classes to enforce permission-based authorization.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class RequirePermissionAttribute : Attribute
{
    /// <summary>
    /// Gets the permission code that is required.
    /// </summary>
    public string PermissionCode { get; }

    /// <summary>
    /// Gets or sets a value indicating whether all specified permissions are required.
    /// When multiple RequirePermission attributes are used, this determines if ALL permissions
    /// are required (true) or if ANY permission is sufficient (false).
    /// </summary>
    public bool RequireAll { get; set; } = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequirePermissionAttribute"/> class.
    /// </summary>
    /// <param name="permissionCode">The permission code that is required for access.</param>
    public RequirePermissionAttribute(string permissionCode)
    {
        PermissionCode = permissionCode;
    }
}