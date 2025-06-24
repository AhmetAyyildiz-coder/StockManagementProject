using Core.Services;
using Xunit;

namespace Core.Tests.Services;

/// <summary>
/// Mock implementation of ICurrentUserService for testing
/// </summary>
public class MockCurrentUserService : ICurrentUserService
{
    public string? UserId { get; set; }
    public string? Username { get; set; }
    public bool IsAuthenticated => !string.IsNullOrEmpty(UserId);
    public IEnumerable<string> Roles { get; set; } = new List<string>();
    public string? TenantId { get; set; }

    public bool HasRole(string role)
    {
        return Roles.Contains(role, StringComparer.OrdinalIgnoreCase);
    }

    public bool HasAnyRole(params string[] roles)
    {
        return roles.Any(role => HasRole(role));
    }
}

/// <summary>
/// Unit tests for ICurrentUserService interface
/// </summary>
public class ICurrentUserServiceTests
{
    [Fact]
    public void CurrentUserService_InitialState_ShouldBeUnauthenticated()
    {
        // Arrange & Act
        var service = new MockCurrentUserService();

        // Assert
        Assert.Null(service.UserId);
        Assert.Null(service.Username);
        Assert.False(service.IsAuthenticated);
        Assert.Empty(service.Roles);
        Assert.Null(service.TenantId);
    }

    [Fact]
    public void CurrentUserService_WithUserId_ShouldBeAuthenticated()
    {
        // Arrange
        var service = new MockCurrentUserService();

        // Act
        service.UserId = "user123";
        service.Username = "testuser";

        // Assert
        Assert.Equal("user123", service.UserId);
        Assert.Equal("testuser", service.Username);
        Assert.True(service.IsAuthenticated);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void CurrentUserService_WithEmptyOrNullUserId_ShouldNotBeAuthenticated(string userId)
    {
        // Arrange
        var service = new MockCurrentUserService();

        // Act
        service.UserId = userId;
        service.Username = "testuser";

        // Assert
        Assert.False(service.IsAuthenticated);
    }

    [Fact]
    public void HasRole_WithMatchingRole_ShouldReturnTrue()
    {
        // Arrange
        var service = new MockCurrentUserService
        {
            UserId = "user123",
            Roles = new[] { "Admin", "Manager", "User" }
        };

        // Act & Assert
        Assert.True(service.HasRole("Admin"));
        Assert.True(service.HasRole("Manager"));
        Assert.True(service.HasRole("User"));
    }

    [Fact]
    public void HasRole_WithNonExistentRole_ShouldReturnFalse()
    {
        // Arrange
        var service = new MockCurrentUserService
        {
            UserId = "user123",
            Roles = new[] { "Admin", "Manager" }
        };

        // Act & Assert
        Assert.False(service.HasRole("SuperAdmin"));
        Assert.False(service.HasRole("Guest"));
    }

    [Fact]
    public void HasRole_CaseInsensitive_ShouldWork()
    {
        // Arrange
        var service = new MockCurrentUserService
        {
            UserId = "user123",
            Roles = new[] { "Admin", "Manager" }
        };

        // Act & Assert
        Assert.True(service.HasRole("admin"));
        Assert.True(service.HasRole("ADMIN"));
        Assert.True(service.HasRole("manager"));
        Assert.True(service.HasRole("MANAGER"));
    }

    [Fact]
    public void HasAnyRole_WithMatchingRoles_ShouldReturnTrue()
    {
        // Arrange
        var service = new MockCurrentUserService
        {
            UserId = "user123",
            Roles = new[] { "Manager", "User" }
        };

        // Act & Assert
        Assert.True(service.HasAnyRole("Admin", "Manager"));
        Assert.True(service.HasAnyRole("User", "Guest"));
        Assert.True(service.HasAnyRole("Manager"));
    }

    [Fact]
    public void HasAnyRole_WithNoMatchingRoles_ShouldReturnFalse()
    {
        // Arrange
        var service = new MockCurrentUserService
        {
            UserId = "user123",
            Roles = new[] { "User" }
        };

        // Act & Assert
        Assert.False(service.HasAnyRole("Admin", "Manager"));
        Assert.False(service.HasAnyRole("SuperAdmin", "Guest"));
    }

    [Fact]
    public void HasAnyRole_WithEmptyRoles_ShouldReturnFalse()
    {
        // Arrange
        var service = new MockCurrentUserService
        {
            UserId = "user123",
            Roles = new[] { "User" }
        };

        // Act & Assert
        Assert.False(service.HasAnyRole());
    }

    [Fact]
    public void TenantId_ShouldBeSettableAndGettable()
    {
        // Arrange
        var service = new MockCurrentUserService();
        const string tenantId = "tenant-123";

        // Act
        service.TenantId = tenantId;

        // Assert
        Assert.Equal(tenantId, service.TenantId);
    }

    [Fact]
    public void CompleteUserContext_ShouldWorkTogether()
    {
        // Arrange
        var service = new MockCurrentUserService();

        // Act
        service.UserId = "user456";
        service.Username = "john.doe@example.com";
        service.TenantId = "tenant-abc";
        service.Roles = new[] { "TenantAdmin", "Manager" };

        // Assert
        Assert.Equal("user456", service.UserId);
        Assert.Equal("john.doe@example.com", service.Username);
        Assert.Equal("tenant-abc", service.TenantId);
        Assert.True(service.IsAuthenticated);
        Assert.True(service.HasRole("TenantAdmin"));
        Assert.True(service.HasRole("Manager"));
        Assert.True(service.HasAnyRole("Admin", "TenantAdmin"));
        Assert.False(service.HasRole("Employee"));
    }

    [Theory]
    [InlineData("Admin")]
    [InlineData("TenantAdmin")]
    [InlineData("Manager")]
    [InlineData("Employee")]
    [InlineData("ReadOnly")]
    public void HasRole_WithVariousRoles_ShouldWorkCorrectly(string role)
    {
        // Arrange
        var service = new MockCurrentUserService
        {
            UserId = "user123",
            Roles = new[] { role }
        };

        // Act & Assert
        Assert.True(service.HasRole(role));
        Assert.False(service.HasRole("NonExistentRole"));
    }

    [Fact]
    public void MultipleRoles_Scenario_ShouldHandleCorrectly()
    {
        // Arrange
        var service = new MockCurrentUserService
        {
            UserId = "user789",
            Username = "admin@tenant.com",
            TenantId = "tenant-789",
            Roles = new[] { "TenantAdmin", "Manager", "Employee", "ReadOnly" }
        };

        // Act & Assert - Individual role checks
        Assert.True(service.HasRole("TenantAdmin"));
        Assert.True(service.HasRole("Manager"));
        Assert.True(service.HasRole("Employee"));
        Assert.True(service.HasRole("ReadOnly"));

        // Act & Assert - Multiple role checks
        Assert.True(service.HasAnyRole("Admin", "TenantAdmin"));
        Assert.True(service.HasAnyRole("Manager", "Supervisor"));
        Assert.False(service.HasAnyRole("SuperAdmin", "SystemAdmin"));

        // Act & Assert - Context validation
        Assert.True(service.IsAuthenticated);
        Assert.Equal("tenant-789", service.TenantId);
    }
}