using Core.Entities.Enums;
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
        Assert.Equal(0, (int)UserRole.TenantAdmin);
        Assert.Equal(1, (int)UserRole.Manager);
        Assert.Equal(2, (int)UserRole.Employee);
    }

    [Fact]
    public void UserRole_AllValues_ShouldBeDefined()
    {
        // Arrange & Act
        var values = Enum.GetValues<UserRole>();

        // Assert
        Assert.Equal(3, values.Length);
        Assert.Contains(UserRole.TenantAdmin, values);
        Assert.Contains(UserRole.Manager, values);
        Assert.Contains(UserRole.Employee, values);
    }

    [Theory]
    [InlineData(UserRole.TenantAdmin)]
    [InlineData(UserRole.Manager)]
    [InlineData(UserRole.Employee)]
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
        Assert.True(Enum.TryParse<UserRole>("TenantAdmin", out var tenantAdmin));
        Assert.Equal(UserRole.TenantAdmin, tenantAdmin);

        Assert.True(Enum.TryParse<UserRole>("Manager", out var manager));
        Assert.Equal(UserRole.Manager, manager);

        Assert.True(Enum.TryParse<UserRole>("Employee", out var employee));
        Assert.Equal(UserRole.Employee, employee);
    }
}