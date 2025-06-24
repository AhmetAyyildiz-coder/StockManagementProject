using Core.Enums;
using Xunit;

namespace Core.Tests.Enums;

/// <summary>
/// Unit tests for UserRole enum to verify role hierarchy and values
/// </summary>
public class UserRoleTests
{
    [Fact]
    public void UserRole_Values_ShouldMatchSpecification()
    {
        // Assert - Verify exact values match specification
        Assert.Equal(1, (int)UserRole.SystemAdmin);
        Assert.Equal(2, (int)UserRole.TenantAdmin);
        Assert.Equal(3, (int)UserRole.Manager);
        Assert.Equal(4, (int)UserRole.Employee);
        Assert.Equal(5, (int)UserRole.ReadOnly);
    }

    [Fact]
    public void UserRole_Hierarchy_ShouldBeCorrect()
    {
        // Arrange - Roles in hierarchy order (highest to lowest privilege)
        var roles = new[]
        {
            UserRole.SystemAdmin,
            UserRole.TenantAdmin,
            UserRole.Manager,
            UserRole.Employee,
            UserRole.ReadOnly
        };

        // Act & Assert - Each role should have higher numeric value than previous
        for (int i = 1; i < roles.Length; i++)
        {
            Assert.True((int)roles[i] > (int)roles[i - 1]);
        }
    }

    [Theory]
    [InlineData(UserRole.SystemAdmin, UserRole.TenantAdmin, true)]
    [InlineData(UserRole.TenantAdmin, UserRole.Manager, true)]
    [InlineData(UserRole.Manager, UserRole.Employee, true)]
    [InlineData(UserRole.Employee, UserRole.ReadOnly, true)]
    [InlineData(UserRole.ReadOnly, UserRole.SystemAdmin, false)]
    [InlineData(UserRole.Manager, UserRole.SystemAdmin, false)]
    public void UserRole_Comparison_ShouldWorkForHierarchy(UserRole higher, UserRole lower, bool higherHasMorePrivileges)
    {
        // Act & Assert - Lower numeric value = higher privilege
        if (higherHasMorePrivileges)
        {
            Assert.True(higher < lower);
            Assert.True(lower > higher);
        }
        else
        {
            Assert.True(higher > lower);
            Assert.True(lower < higher);
        }
    }

    [Fact]
    public void UserRole_AllEnumValues_ShouldBeDefined()
    {
        // Arrange
        var expectedRoles = new[]
        {
            UserRole.SystemAdmin,
            UserRole.TenantAdmin,
            UserRole.Manager,
            UserRole.Employee,
            UserRole.ReadOnly
        };

        // Act
        var allRoles = Enum.GetValues<UserRole>();

        // Assert
        Assert.Equal(expectedRoles.Length, allRoles.Length);
        foreach (var role in expectedRoles)
        {
            Assert.Contains(role, allRoles);
        }
    }
}