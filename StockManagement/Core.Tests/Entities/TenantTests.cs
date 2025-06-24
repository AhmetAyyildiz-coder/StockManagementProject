using Core.Entities;
using Xunit;

namespace Core.Tests.Entities;

/// <summary>
/// Unit tests for Tenant entity
/// </summary>
public class TenantTests
{
    [Fact]
    public void Tenant_DefaultValues_ShouldBeSetCorrectly()
    {
        // Arrange & Act
        var tenant = new Tenant();

        // Assert
        Assert.Equal(0, tenant.Id);
        Assert.Equal(string.Empty, tenant.Name);
        Assert.Equal(string.Empty, tenant.Code);
        Assert.True(tenant.IsEnabled);
    }

    [Fact]
    public void Tenant_InheritsFromTenantEntity_ShouldHaveBaseProperties()
    {
        // Arrange
        var tenant = new Tenant
        {
            TenantId = "tenant-123",
            IsActive = false
        };

        // Act & Assert
        Assert.Equal("tenant-123", tenant.TenantId);
        Assert.False(tenant.IsActive);
        Assert.True(tenant.CreatedAt <= DateTime.UtcNow);
        Assert.Null(tenant.UpdatedAt);
    }

    [Theory]
    [InlineData("Test Tenant", "TEST-TENANT")]
    [InlineData("My Shop", "SHOP-001")]
    [InlineData("", "")]
    public void Tenant_Properties_ShouldBeSettableAndGettable(string name, string code)
    {
        // Arrange & Act
        var tenant = new Tenant
        {
            Name = name,
            Code = code,
            IsEnabled = false
        };

        // Assert
        Assert.Equal(name, tenant.Name);
        Assert.Equal(code, tenant.Code);
        Assert.False(tenant.IsEnabled);
    }

    [Fact]
    public void Tenant_Id_ShouldBeSettableAndGettable()
    {
        // Arrange
        var tenant = new Tenant();
        var testId = 123;

        // Act
        tenant.Id = testId;

        // Assert
        Assert.Equal(testId, tenant.Id);
    }
}