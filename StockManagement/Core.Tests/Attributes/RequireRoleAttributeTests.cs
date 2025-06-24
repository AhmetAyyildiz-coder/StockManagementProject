using Core.Attributes;
using Core.Enums;
using Xunit;

namespace Core.Tests.Attributes;

/// <summary>
/// Unit tests for RequireRoleAttribute
/// </summary>
public class RequireRoleAttributeTests
{
    [Theory]
    [InlineData(UserRole.SystemAdmin)]
    [InlineData(UserRole.TenantAdmin)]
    [InlineData(UserRole.Manager)]
    [InlineData(UserRole.Employee)]
    [InlineData(UserRole.ReadOnly)]
    public void RequireRoleAttribute_ShouldSetMinimumRole(UserRole role)
    {
        // Act
        var attribute = new RequireRoleAttribute(role);

        // Assert
        Assert.Equal(role, attribute.MinimumRole);
    }

    [Fact]
    public void RequireRoleAttribute_ShouldBeApplicableToMethodsAndClasses()
    {
        // Arrange
        var attributeType = typeof(RequireRoleAttribute);

        // Act
        var attributeUsage = attributeType.GetCustomAttributes(typeof(AttributeUsageAttribute), false)
            .Cast<AttributeUsageAttribute>()
            .FirstOrDefault();

        // Assert
        Assert.NotNull(attributeUsage);
        Assert.True(attributeUsage.ValidOn.HasFlag(AttributeTargets.Method));
        Assert.True(attributeUsage.ValidOn.HasFlag(AttributeTargets.Class));
        Assert.False(attributeUsage.AllowMultiple); // Should only allow one role requirement
    }
}