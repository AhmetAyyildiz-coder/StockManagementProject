namespace Core.Attributes;

/// <summary>
/// Attribute for declarative permission-based authorization.
/// Can be applied to methods or classes to require specific permissions for access.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class RequirePermissionAttribute : Attribute
{
    /// <summary>
    /// Gets the permission code required for access.
    /// </summary>
    public string PermissionCode { get; }

    /// <summary>
    /// Gets or sets a value indicating whether all specified permissions are required.
    /// When multiple attributes are applied, this determines if ALL permissions are needed (true)
    /// or if ANY one permission is sufficient (false). Default is false.
    /// </summary>
    public bool RequireAll { get; set; } = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequirePermissionAttribute"/> class.
    /// </summary>
    /// <param name="permissionCode">The permission code required for access</param>
    public RequirePermissionAttribute(string permissionCode)
    {
        PermissionCode = permissionCode;
    }
}