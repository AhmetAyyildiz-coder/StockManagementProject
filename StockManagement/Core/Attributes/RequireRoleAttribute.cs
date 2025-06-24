using Core.Enums;

namespace Core.Attributes;

/// <summary>
/// Attribute to require a minimum user role for method or class access.
/// Can be applied to methods or classes to enforce role-based authorization.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequireRoleAttribute : Attribute
{
    /// <summary>
    /// Gets the minimum required user role for access.
    /// </summary>
    public UserRole MinimumRole { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RequireRoleAttribute"/> class.
    /// </summary>
    /// <param name="minimumRole">The minimum user role required for access.</param>
    public RequireRoleAttribute(UserRole minimumRole)
    {
        MinimumRole = minimumRole;
    }
}