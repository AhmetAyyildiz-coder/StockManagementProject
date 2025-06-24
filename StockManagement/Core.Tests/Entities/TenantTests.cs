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
        Assert.Equal(string.Empty, tenant.Id);
        Assert.Equal(string.Empty, tenant.Name);
        Assert.Equal(string.Empty, tenant.SubDomain);
        Assert.True(tenant.IsActive);
        Assert.True(tenant.CreatedAt <= DateTime.UtcNow);
        Assert.NotNull(tenant.Users);
        Assert.Empty(tenant.Users);
        Assert.NotNull(tenant.Products);
        Assert.Empty(tenant.Products);
    }

    [Fact]
    public void Tenant_SetProperties_ShouldRetainValues()
    {
        // Arrange
        var tenant = new Tenant();
        var testId = "test-tenant";
        var testName = "Test Tenant";
        var testSubDomain = "test";
        var testCreatedAt = DateTime.UtcNow.AddDays(-1);

        // Act
        tenant.Id = testId;
        tenant.Name = testName;
        tenant.SubDomain = testSubDomain;
        tenant.IsActive = false;
        tenant.CreatedAt = testCreatedAt;

        // Assert
        Assert.Equal(testId, tenant.Id);
        Assert.Equal(testName, tenant.Name);
        Assert.Equal(testSubDomain, tenant.SubDomain);
        Assert.False(tenant.IsActive);
        Assert.Equal(testCreatedAt, tenant.CreatedAt);
    }

    [Theory]
    [InlineData("tenant1")]
    [InlineData("multi-word-tenant")]
    [InlineData("tenant_with_underscore")]
    [InlineData("UPPERCASE")]
    public void Tenant_SubDomain_ShouldAcceptVariousFormats(string subDomain)
    {
        // Arrange & Act
        var tenant = new Tenant { SubDomain = subDomain };

        // Assert
        Assert.Equal(subDomain, tenant.SubDomain);
    }
}