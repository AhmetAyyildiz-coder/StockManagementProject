using Core.Entities;
using Core.Services;
using Xunit;

namespace Core.Tests.Interfaces.Services;

/// <summary>
/// Mock implementation of ICurrentTenantService for testing
/// </summary>
public class MockCurrentTenantService : ICurrentTenantService
{
    private readonly Dictionary<string, Tenant> _tenants = new();
    private string _currentTenantId = string.Empty;

    public string TenantId => _currentTenantId;

    public void SetCurrentTenant(string tenantId)
    {
        _currentTenantId = tenantId;
    }

    public void AddTenant(Tenant tenant)
    {
        _tenants[tenant.TenantId] = tenant;
    }

    public Task<Tenant?> GetTenantAsync()
    {
        _tenants.TryGetValue(_currentTenantId, out var tenant);
        return Task.FromResult(tenant);
    }

    public Task<bool> ValidateTenantAsync(string tenantId)
    {
        var exists = _tenants.ContainsKey(tenantId) && _tenants[tenantId].IsEnabled;
        return Task.FromResult(exists);
    }

    public bool IsValidTenant()
    {
        return !string.IsNullOrEmpty(_currentTenantId) && 
               _tenants.ContainsKey(_currentTenantId) && 
               _tenants[_currentTenantId].IsEnabled;
    }
}

/// <summary>
/// Unit tests for ICurrentTenantService interface
/// </summary>
public class ICurrentTenantServiceTests
{
    [Fact]
    public async Task GetTenantAsync_WithValidTenant_ShouldReturnTenant()
    {
        // Arrange
        var tenant = new Tenant 
        { 
            Id = 1, 
            Name = "Test Tenant", 
            TenantId = "tenant-1", 
            Code = "tenant-1",
            IsEnabled = true 
        };
        var service = new MockCurrentTenantService();
        service.AddTenant(tenant);
        service.SetCurrentTenant("tenant-1");

        // Act
        var result = await service.GetTenantAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tenant.Id, result.Id);
        Assert.Equal(tenant.Name, result.Name);
        Assert.Equal(tenant.TenantId, result.TenantId);
    }

    [Fact]
    public async Task GetTenantAsync_WithInvalidTenant_ShouldReturnNull()
    {
        // Arrange
        var service = new MockCurrentTenantService();
        service.SetCurrentTenant("non-existent-tenant");

        // Act
        var result = await service.GetTenantAsync();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ValidateTenantAsync_WithExistingTenant_ShouldReturnTrue()
    {
        // Arrange
        var tenant = new Tenant 
        { 
            Id = 1, 
            TenantId = "tenant-1", 
            Name = "Test Tenant",
            IsEnabled = true 
        };
        var service = new MockCurrentTenantService();
        service.AddTenant(tenant);

        // Act
        var result = await service.ValidateTenantAsync("tenant-1");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidateTenantAsync_WithNonExistingTenant_ShouldReturnFalse()
    {
        // Arrange
        var service = new MockCurrentTenantService();

        // Act
        var result = await service.ValidateTenantAsync("non-existent-tenant");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidateTenantAsync_WithDisabledTenant_ShouldReturnFalse()
    {
        // Arrange
        var tenant = new Tenant 
        { 
            Id = 1, 
            TenantId = "tenant-1", 
            Name = "Disabled Tenant",
            IsEnabled = false 
        };
        var service = new MockCurrentTenantService();
        service.AddTenant(tenant);

        // Act
        var result = await service.ValidateTenantAsync("tenant-1");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValidTenant_WithValidCurrentTenant_ShouldReturnTrue()
    {
        // Arrange
        var tenant = new Tenant 
        { 
            Id = 1, 
            TenantId = "tenant-1", 
            Name = "Test Tenant",
            IsEnabled = true 
        };
        var service = new MockCurrentTenantService();
        service.AddTenant(tenant);
        service.SetCurrentTenant("tenant-1");

        // Act
        var result = service.IsValidTenant();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValidTenant_WithoutCurrentTenant_ShouldReturnFalse()
    {
        // Arrange
        var service = new MockCurrentTenantService();

        // Act
        var result = service.IsValidTenant();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValidTenant_WithDisabledCurrentTenant_ShouldReturnFalse()
    {
        // Arrange
        var tenant = new Tenant 
        { 
            Id = 1, 
            TenantId = "tenant-1", 
            Name = "Disabled Tenant",
            IsEnabled = false 
        };
        var service = new MockCurrentTenantService();
        service.AddTenant(tenant);
        service.SetCurrentTenant("tenant-1");

        // Act
        var result = service.IsValidTenant();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TenantId_ShouldReturnCurrentTenantId()
    {
        // Arrange
        var service = new MockCurrentTenantService();
        service.SetCurrentTenant("tenant-123");

        // Act
        var result = service.TenantId;

        // Assert
        Assert.Equal("tenant-123", result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("tenant-1")]
    [InlineData("TENANT_2")]
    [InlineData("multi-word-tenant")]
    public void TenantId_WithVariousValues_ShouldReturnCorrectValue(string tenantId)
    {
        // Arrange
        var service = new MockCurrentTenantService();
        service.SetCurrentTenant(tenantId);

        // Act
        var result = service.TenantId;

        // Assert
        Assert.Equal(tenantId, result);
    }
}