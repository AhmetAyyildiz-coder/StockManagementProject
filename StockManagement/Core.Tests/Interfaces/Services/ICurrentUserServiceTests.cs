using Core.Entities;
using Core.Enums;
using Core.Services;
using Xunit;

namespace Core.Tests.Interfaces.Services;

/// <summary>
/// Mock implementation of ICurrentUserService for testing
/// </summary>
public class MockCurrentUserService : ICurrentUserService
{
    private User? _currentUser;
    private string? _userId;
    private string _tenantId = string.Empty;
    private bool _isAuthenticated;

    public void SetCurrentUser(User? user)
    {
        _currentUser = user;
        _userId = user?.Id.ToString();
        _tenantId = user?.TenantId ?? string.Empty;
        _isAuthenticated = user != null;
    }

    public Task<User?> GetCurrentUserAsync()
    {
        return Task.FromResult(_currentUser);
    }

    public string? GetCurrentUserId()
    {
        return _userId;
    }

    public string GetCurrentTenantId()
    {
        return _tenantId;
    }

    public bool IsAuthenticated()
    {
        return _isAuthenticated;
    }

    public bool HasRole(UserRole role)
    {
        return _currentUser != null && _currentUser.Role <= role;
    }

    public bool CanManageMovementTypes()
    {
        return _currentUser?.CanManageMovementTypes() ?? false;
    }
}

/// <summary>
/// Unit tests for ICurrentUserService interface
/// </summary>
public class ICurrentUserServiceTests
{
    [Fact]
    public async Task GetCurrentUserAsync_WithAuthenticatedUser_ShouldReturnUser()
    {
        // Arrange
        var user = new User { Id = 1, Username = "testuser", Role = UserRole.Manager, TenantId = "tenant-1" };
        var service = new MockCurrentUserService();
        service.SetCurrentUser(user);

        // Act
        var result = await service.GetCurrentUserAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Username, result.Username);
        Assert.Equal(user.Role, result.Role);
    }

    [Fact]
    public async Task GetCurrentUserAsync_WithoutAuthentication_ShouldReturnNull()
    {
        // Arrange
        var service = new MockCurrentUserService();

        // Act
        var result = await service.GetCurrentUserAsync();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetCurrentUserId_WithAuthenticatedUser_ShouldReturnUserId()
    {
        // Arrange
        var user = new User { Id = 123, Username = "testuser", TenantId = "tenant-1" };
        var service = new MockCurrentUserService();
        service.SetCurrentUser(user);

        // Act
        var result = service.GetCurrentUserId();

        // Assert
        Assert.Equal("123", result);
    }

    [Fact]
    public void GetCurrentUserId_WithoutAuthentication_ShouldReturnNull()
    {
        // Arrange
        var service = new MockCurrentUserService();

        // Act
        var result = service.GetCurrentUserId();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetCurrentTenantId_WithAuthenticatedUser_ShouldReturnTenantId()
    {
        // Arrange
        var user = new User { Id = 1, TenantId = "tenant-123" };
        var service = new MockCurrentUserService();
        service.SetCurrentUser(user);

        // Act
        var result = service.GetCurrentTenantId();

        // Assert
        Assert.Equal("tenant-123", result);
    }

    [Fact]
    public void IsAuthenticated_WithUser_ShouldReturnTrue()
    {
        // Arrange
        var user = new User { Id = 1, TenantId = "tenant-1" };
        var service = new MockCurrentUserService();
        service.SetCurrentUser(user);

        // Act
        var result = service.IsAuthenticated();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsAuthenticated_WithoutUser_ShouldReturnFalse()
    {
        // Arrange
        var service = new MockCurrentUserService();

        // Act
        var result = service.IsAuthenticated();

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(UserRole.SystemAdmin, UserRole.SystemAdmin, true)]
    [InlineData(UserRole.SystemAdmin, UserRole.Manager, true)]
    [InlineData(UserRole.Manager, UserRole.Employee, true)]
    [InlineData(UserRole.Employee, UserRole.Manager, false)]
    [InlineData(UserRole.ReadOnly, UserRole.Employee, false)]
    public void HasRole_ShouldEnforceRoleHierarchy(UserRole userRole, UserRole checkRole, bool expectedResult)
    {
        // Arrange
        var user = new User { Id = 1, Role = userRole, TenantId = "tenant-1" };
        var service = new MockCurrentUserService();
        service.SetCurrentUser(user);

        // Act
        var result = service.HasRole(checkRole);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(UserRole.SystemAdmin, true)]
    [InlineData(UserRole.TenantAdmin, true)]
    [InlineData(UserRole.Manager, true)]
    [InlineData(UserRole.Employee, false)]
    [InlineData(UserRole.ReadOnly, false)]
    public void CanManageMovementTypes_ShouldReturnCorrectValue(UserRole userRole, bool expectedResult)
    {
        // Arrange
        var user = new User { Id = 1, Role = userRole, TenantId = "tenant-1" };
        var service = new MockCurrentUserService();
        service.SetCurrentUser(user);

        // Act
        var result = service.CanManageMovementTypes();

        // Assert
        Assert.Equal(expectedResult, result);
    }
}