using Core.Enums;

namespace Core.Attributes;

/// <summary>
/// Attribute for declarative role-based authorization.
/// Can be applied to methods or classes to require a minimum role level for access.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequireRoleAttribute : Attribute
{
    /// <summary>
    /// Gets the minimum role required for access.
    /// Users with this role or higher privileges can access the decorated member.
    /// </summary>
    public UserRole MinimumRole { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RequireRoleAttribute"/> class.
    /// </summary>
    /// <param name="minimumRole">The minimum role required for access</param>
    public RequireRoleAttribute(UserRole minimumRole)
    {
        MinimumRole = minimumRole;
    }
}