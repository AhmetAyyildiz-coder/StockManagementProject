using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Helpers;
using Xunit;

namespace Core.Tests.Helpers;

/// <summary>
/// Unit tests for AuthorizationHelper static class
/// </summary>
public class AuthorizationHelperTests
{
    [Theory]
    [InlineData(UserRole.SystemAdmin, true)]
    [InlineData(UserRole.TenantAdmin, true)]
    [InlineData(UserRole.Manager, true)]
    [InlineData(UserRole.Employee, false)]
    [InlineData(UserRole.ReadOnly, false)]
    public void EnsureCanManageMovementTypes_ShouldEnforceRoleRequirements(UserRole userRole, bool shouldSucceed)
    {
        // Arrange
        var user = new User { Role = userRole, TenantId = "tenant-1" };

        // Act & Assert
        if (shouldSucceed)
        {
            // Should not throw exception
            AuthorizationHelper.EnsureCanManageMovementTypes(user);
        }
        else
        {
            // Should throw UnauthorizedException
            var exception = Assert.Throws<UnauthorizedException>(() => 
                AuthorizationHelper.EnsureCanManageMovementTypes(user));
            Assert.Equal("Hareket tipi yönetme yetkiniz yok.", exception.Message);
        }
    }

    [Theory]
    [InlineData(UserRole.SystemAdmin, UserRole.SystemAdmin, true)]
    [InlineData(UserRole.SystemAdmin, UserRole.TenantAdmin, true)]
    [InlineData(UserRole.SystemAdmin, UserRole.Manager, true)]
    [InlineData(UserRole.TenantAdmin, UserRole.TenantAdmin, true)]
    [InlineData(UserRole.TenantAdmin, UserRole.Manager, true)]
    [InlineData(UserRole.Manager, UserRole.Manager, true)]
    [InlineData(UserRole.Manager, UserRole.Employee, true)]
    [InlineData(UserRole.Employee, UserRole.Manager, false)]
    [InlineData(UserRole.ReadOnly, UserRole.Employee, false)]
    [InlineData(UserRole.ReadOnly, UserRole.SystemAdmin, false)]
    public void EnsureHasRole_ShouldEnforceRoleHierarchy(UserRole userRole, UserRole requiredRole, bool shouldSucceed)
    {
        // Arrange
        var user = new User { Role = userRole, TenantId = "tenant-1" };

        // Act & Assert
        if (shouldSucceed)
        {
            // Should not throw exception
            AuthorizationHelper.EnsureHasRole(user, requiredRole);
        }
        else
        {
            // Should throw UnauthorizedException
            var exception = Assert.Throws<UnauthorizedException>(() => 
                AuthorizationHelper.EnsureHasRole(user, requiredRole));
            Assert.Contains(requiredRole.ToString(), exception.Message);
        }
    }

    [Theory]
    [InlineData(UserRole.SystemAdmin, "tenant-1", "tenant-2", true)] // SystemAdmin can access any tenant
    [InlineData(UserRole.TenantAdmin, "tenant-1", "tenant-1", true)] // Same tenant access
    [InlineData(UserRole.TenantAdmin, "tenant-1", "tenant-2", false)] // Different tenant access
    [InlineData(UserRole.Manager, "tenant-1", "tenant-1", true)] // Same tenant access
    [InlineData(UserRole.Manager, "tenant-1", "tenant-2", false)] // Different tenant access
    [InlineData(UserRole.Employee, "tenant-1", "tenant-1", true)] // Same tenant access
    [InlineData(UserRole.Employee, "tenant-1", "tenant-2", false)] // Different tenant access
    [InlineData(UserRole.ReadOnly, "tenant-1", "tenant-1", true)] // Same tenant access
    [InlineData(UserRole.ReadOnly, "tenant-1", "tenant-2", false)] // Different tenant access
    public void CanUserAccessTenant_ShouldEnforceTenantIsolation(UserRole userRole, string userTenantId, string accessTenantId, bool expectedResult)
    {
        // Arrange
        var user = new User { Role = userRole, TenantId = userTenantId };

        // Act
        var result = AuthorizationHelper.CanUserAccessTenant(user, accessTenantId);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void EnsureCanAccessTenant_WithValidAccess_ShouldNotThrow()
    {
        // Arrange
        var user = new User { Role = UserRole.TenantAdmin, TenantId = "tenant-1" };

        // Act & Assert - Should not throw
        AuthorizationHelper.EnsureCanAccessTenant(user, "tenant-1");
    }

    [Fact]
    public void EnsureCanAccessTenant_WithInvalidAccess_ShouldThrowException()
    {
        // Arrange
        var user = new User { Role = UserRole.TenantAdmin, TenantId = "tenant-1" };

        // Act & Assert
        var exception = Assert.Throws<UnauthorizedException>(() => 
            AuthorizationHelper.EnsureCanAccessTenant(user, "tenant-2"));
        Assert.Contains("tenant-2", exception.Message);
        Assert.Contains("erişim yetkiniz yok", exception.Message);
    }

    [Fact]
    public void EnsureCanAccessTenant_SystemAdmin_ShouldAccessAnyTenant()
    {
        // Arrange
        var user = new User { Role = UserRole.SystemAdmin, TenantId = "tenant-1" };

        // Act & Assert - Should not throw for any tenant
        AuthorizationHelper.EnsureCanAccessTenant(user, "tenant-2");
        AuthorizationHelper.EnsureCanAccessTenant(user, "tenant-3");
        AuthorizationHelper.EnsureCanAccessTenant(user, "different-tenant");
    }

    #region Permission-based Authorization Tests

    [Fact]
    public void EnsureHasPermission_WithRequiredPermission_ShouldNotThrow()
    {
        // Arrange
        var user = new User { Role = UserRole.Manager, TenantId = "tenant-1" };
        var permissions = new List<Permission>
        {
            new Permission { Code = "MANAGE_MOVEMENT_TYPES", Name = "Manage Movement Types" }
        };

        // Act & Assert - Should not throw
        AuthorizationHelper.EnsureHasPermission(user, permissions, "MANAGE_MOVEMENT_TYPES");
    }

    [Fact]
    public void EnsureHasPermission_WithoutRequiredPermission_ShouldThrow()
    {
        // Arrange
        var user = new User { Role = UserRole.Employee, TenantId = "tenant-1" };
        var permissions = new List<Permission>
        {
            new Permission { Code = "VIEW_PRODUCTS", Name = "View Products" }
        };

        // Act & Assert
        var exception = Assert.Throws<UnauthorizedException>(() =>
            AuthorizationHelper.EnsureHasPermission(user, permissions, "MANAGE_MOVEMENT_TYPES"));
        Assert.Contains("MANAGE_MOVEMENT_TYPES", exception.Message);
    }

    [Fact]
    public void HasAnyPermission_WithOneMatchingPermission_ShouldReturnTrue()
    {
        // Arrange
        var user = new User { Role = UserRole.Manager, TenantId = "tenant-1" };
        var permissions = new List<Permission>
        {
            new Permission { Code = "MANAGE_MOVEMENT_TYPES", Name = "Manage Movement Types" },
            new Permission { Code = "VIEW_PRODUCTS", Name = "View Products" }
        };

        // Act
        var result = AuthorizationHelper.HasAnyPermission(user, permissions, "MANAGE_MOVEMENT_TYPES", "MANAGE_USERS");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasAllPermissions_WithAllMatchingPermissions_ShouldReturnTrue()
    {
        // Arrange
        var user = new User { Role = UserRole.Manager, TenantId = "tenant-1" };
        var permissions = new List<Permission>
        {
            new Permission { Code = "MANAGE_MOVEMENT_TYPES", Name = "Manage Movement Types" },
            new Permission { Code = "VIEW_PRODUCTS", Name = "View Products" }
        };

        // Act
        var result = AuthorizationHelper.HasAllPermissions(user, permissions, "MANAGE_MOVEMENT_TYPES", "VIEW_PRODUCTS");

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("MANAGE_MOVEMENT_TYPES", true)]
    [InlineData("VIEW_PRODUCTS", true)]
    [InlineData("manage_movement_types", false)] // Case sensitive
    [InlineData("MANAGE MOVEMENT TYPES", false)] // Spaces not allowed
    [InlineData("MANAGE-MOVEMENT-TYPES", false)] // Dashes not allowed
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValidPermissionCode_ShouldValidateCorrectly(string code, bool expected)
    {
        // Act
        var result = AuthorizationHelper.IsValidPermissionCode(code);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(UserRole.SystemAdmin, UserRole.TenantAdmin, true)]
    [InlineData(UserRole.SystemAdmin, UserRole.Manager, true)]
    [InlineData(UserRole.TenantAdmin, UserRole.Manager, true)]
    [InlineData(UserRole.Manager, UserRole.Employee, true)]
    [InlineData(UserRole.Employee, UserRole.ReadOnly, true)]
    [InlineData(UserRole.Manager, UserRole.TenantAdmin, false)]
    [InlineData(UserRole.Employee, UserRole.Manager, false)]
    [InlineData(UserRole.ReadOnly, UserRole.Employee, false)]
    public void IsHigherRole_ShouldCompareRolesCorrectly(UserRole currentRole, UserRole targetRole, bool expected)
    {
        // Act
        var result = AuthorizationHelper.IsHigherRole(currentRole, targetRole);

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion
}