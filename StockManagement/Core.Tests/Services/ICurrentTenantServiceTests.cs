using Core.Services;
using Xunit;

namespace Core.Tests.Services;

/// <summary>
/// Mock implementation of ICurrentTenantService for testing
/// </summary>
public class MockCurrentTenantService : ICurrentTenantService
{
    private string _tenantId = string.Empty;
    private string? _tenantName;

    public string TenantId => _tenantId;
    public string? TenantName => _tenantName;
    public bool HasTenant => !string.IsNullOrEmpty(_tenantId);

    public void SetTenant(string tenantId, string? tenantName = null)
    {
        _tenantId = tenantId ?? string.Empty;
        _tenantName = tenantName;
    }

    public void ClearTenant()
    {
        _tenantId = string.Empty;
        _tenantName = null;
    }
}

/// <summary>
/// Unit tests for ICurrentTenantService interface
/// </summary>
public class ICurrentTenantServiceTests
{
    [Fact]
    public void CurrentTenantService_InitialState_ShouldBeEmpty()
    {
        // Arrange & Act
        var service = new MockCurrentTenantService();

        // Assert
        Assert.Equal(string.Empty, service.TenantId);
        Assert.Null(service.TenantName);
        Assert.False(service.HasTenant);
    }

    [Fact]
    public void SetTenant_WithValidTenantId_ShouldSetTenantInfo()
    {
        // Arrange
        var service = new MockCurrentTenantService();
        const string tenantId = "test-tenant";
        const string tenantName = "Test Tenant";

        // Act
        service.SetTenant(tenantId, tenantName);

        // Assert
        Assert.Equal(tenantId, service.TenantId);
        Assert.Equal(tenantName, service.TenantName);
        Assert.True(service.HasTenant);
    }

    [Fact]
    public void SetTenant_WithOnlyTenantId_ShouldSetTenantId()
    {
        // Arrange
        var service = new MockCurrentTenantService();
        const string tenantId = "test-tenant";

        // Act
        service.SetTenant(tenantId);

        // Assert
        Assert.Equal(tenantId, service.TenantId);
        Assert.Null(service.TenantName);
        Assert.True(service.HasTenant);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void SetTenant_WithEmptyOrNullTenantId_ShouldSetEmptyTenant(string tenantId)
    {
        // Arrange
        var service = new MockCurrentTenantService();

        // Act
        service.SetTenant(tenantId);

        // Assert
        Assert.Equal(string.Empty, service.TenantId);
        Assert.False(service.HasTenant);
    }

    [Fact]
    public void ClearTenant_ShouldResetTenantInfo()
    {
        // Arrange
        var service = new MockCurrentTenantService();
        service.SetTenant("test-tenant", "Test Tenant");
        Assert.True(service.HasTenant); // Verify it was set

        // Act
        service.ClearTenant();

        // Assert
        Assert.Equal(string.Empty, service.TenantId);
        Assert.Null(service.TenantName);
        Assert.False(service.HasTenant);
    }

    [Fact]
    public void SetTenant_MultipleTimes_ShouldUpdateTenantInfo()
    {
        // Arrange
        var service = new MockCurrentTenantService();

        // Act - First tenant
        service.SetTenant("tenant-1", "Tenant 1");
        var firstTenantId = service.TenantId;
        var firstTenantName = service.TenantName;

        // Act - Second tenant
        service.SetTenant("tenant-2", "Tenant 2");

        // Assert
        Assert.NotEqual(firstTenantId, service.TenantId);
        Assert.NotEqual(firstTenantName, service.TenantName);
        Assert.Equal("tenant-2", service.TenantId);
        Assert.Equal("Tenant 2", service.TenantName);
        Assert.True(service.HasTenant);
    }

    [Theory]
    [InlineData("tenant-1")]
    [InlineData("TENANT-2")]
    [InlineData("tenant_with_underscore")]
    [InlineData("tenant-with-dashes")]
    [InlineData("123-numeric-tenant")]
    public void SetTenant_WithVariousTenantIdFormats_ShouldAcceptAll(string tenantId)
    {
        // Arrange
        var service = new MockCurrentTenantService();

        // Act
        service.SetTenant(tenantId);

        // Assert
        Assert.Equal(tenantId, service.TenantId);
        Assert.True(service.HasTenant);
    }

    [Fact]
    public void TenantLifecycle_CompleteScenario_ShouldWorkCorrectly()
    {
        // Arrange
        var service = new MockCurrentTenantService();

        // Act & Assert - Initial state
        Assert.False(service.HasTenant);

        // Act & Assert - Set tenant
        service.SetTenant("test-tenant", "Test Tenant");
        Assert.True(service.HasTenant);
        Assert.Equal("test-tenant", service.TenantId);
        Assert.Equal("Test Tenant", service.TenantName);

        // Act & Assert - Update tenant
        service.SetTenant("new-tenant", "New Tenant");
        Assert.True(service.HasTenant);
        Assert.Equal("new-tenant", service.TenantId);
        Assert.Equal("New Tenant", service.TenantName);

        // Act & Assert - Clear tenant
        service.ClearTenant();
        Assert.False(service.HasTenant);
        Assert.Equal(string.Empty, service.TenantId);
        Assert.Null(service.TenantName);
    }
}