using Core.Attributes;
using Core.Enums;
using Xunit;

namespace Core.Tests.Attributes;

/// <summary>
/// Unit tests for RequirePermissionAttribute
/// </summary>
public class RequirePermissionAttributeTests
{
    [Fact]
    public void RequirePermissionAttribute_ShouldSetPermissionCode()
    {
        // Arrange
        const string permissionCode = "MANAGE_MOVEMENT_TYPES";

        // Act
        var attribute = new RequirePermissionAttribute(permissionCode);

        // Assert
        Assert.Equal(permissionCode, attribute.PermissionCode);
        Assert.False(attribute.RequireAll); // Default value
    }

    [Fact]
    public void RequirePermissionAttribute_ShouldAllowSettingRequireAll()
    {
        // Arrange
        const string permissionCode = "MANAGE_MOVEMENT_TYPES";

        // Act
        var attribute = new RequirePermissionAttribute(permissionCode)
        {
            RequireAll = true
        };

        // Assert
        Assert.Equal(permissionCode, attribute.PermissionCode);
        Assert.True(attribute.RequireAll);
    }

    [Fact]
    public void RequirePermissionAttribute_ShouldBeApplicableToMethodsAndClasses()
    {
        // Arrange
        var attributeType = typeof(RequirePermissionAttribute);

        // Act
        var attributeUsage = attributeType.GetCustomAttributes(typeof(AttributeUsageAttribute), false)
            .Cast<AttributeUsageAttribute>()
            .FirstOrDefault();

        // Assert
        Assert.NotNull(attributeUsage);
        Assert.True(attributeUsage.ValidOn.HasFlag(AttributeTargets.Method));
        Assert.True(attributeUsage.ValidOn.HasFlag(AttributeTargets.Class));
        Assert.True(attributeUsage.AllowMultiple);
    }
}