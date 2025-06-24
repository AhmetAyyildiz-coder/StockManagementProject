using Core.Enums;
using Xunit;

namespace Core.Tests.Entities.Enums;

/// <summary>
/// Unit tests for UserRole enum
/// </summary>
public class UserRoleTests
{
    [Fact]
    public void UserRole_Values_ShouldHaveCorrectDefinition()
    {
        // Arrange & Act & Assert
        Assert.Equal(1, (int)UserRole.SystemAdmin);
        Assert.Equal(2, (int)UserRole.TenantAdmin);
        Assert.Equal(3, (int)UserRole.Manager);
        Assert.Equal(4, (int)UserRole.Employee);
        Assert.Equal(5, (int)UserRole.ReadOnly);
    }

    [Fact]
    public void UserRole_AllValues_ShouldBeDefined()
    {
        // Arrange & Act
        var values = Enum.GetValues<UserRole>();

        // Assert
        Assert.Equal(5, values.Length);
        Assert.Contains(UserRole.SystemAdmin, values);
        Assert.Contains(UserRole.TenantAdmin, values);
        Assert.Contains(UserRole.Manager, values);
        Assert.Contains(UserRole.Employee, values);
        Assert.Contains(UserRole.ReadOnly, values);
    }

    [Theory]
    [InlineData(UserRole.SystemAdmin)]
    [InlineData(UserRole.TenantAdmin)]
    [InlineData(UserRole.Manager)]
    [InlineData(UserRole.Employee)]
    [InlineData(UserRole.ReadOnly)]
    public void UserRole_ToString_ShouldReturnValidString(UserRole role)
    {
        // Arrange & Act
        var result = role.ToString();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void UserRole_Parsing_ShouldWork()
    {
        // Arrange & Act & Assert
        Assert.True(Enum.TryParse<UserRole>("SystemAdmin", out var systemAdmin));
        Assert.Equal(UserRole.SystemAdmin, systemAdmin);

        Assert.True(Enum.TryParse<UserRole>("TenantAdmin", out var tenantAdmin));
        Assert.Equal(UserRole.TenantAdmin, tenantAdmin);

        Assert.True(Enum.TryParse<UserRole>("Manager", out var manager));
        Assert.Equal(UserRole.Manager, manager);

        Assert.True(Enum.TryParse<UserRole>("Employee", out var employee));
        Assert.Equal(UserRole.Employee, employee);

        Assert.True(Enum.TryParse<UserRole>("ReadOnly", out var readOnly));
        Assert.Equal(UserRole.ReadOnly, readOnly);
    }

    [Fact]
    public void UserRole_Hierarchy_ShouldBeOrderedByPrivilege()
    {
        // Arrange & Act & Assert - Lower values = higher privileges
        Assert.True(UserRole.SystemAdmin < UserRole.TenantAdmin);
        Assert.True(UserRole.TenantAdmin < UserRole.Manager);
        Assert.True(UserRole.Manager < UserRole.Employee);
        Assert.True(UserRole.Employee < UserRole.ReadOnly);
    }
}