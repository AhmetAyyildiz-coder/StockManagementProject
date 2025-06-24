using Core.Entities;
using Core.Enums;
using Xunit;

namespace Core.Tests.Entities;

/// <summary>
/// Unit tests for RolePermission entity
/// </summary>
public class RolePermissionTests
{
    [Fact]
    public void RolePermission_ShouldHaveDefaultValues()
    {
        // Act
        var rolePermission = new RolePermission();

        // Assert
        Assert.Equal(0, rolePermission.Id);
        Assert.Equal(default(UserRole), rolePermission.Role);
        Assert.Equal(0, rolePermission.PermissionId);
        Assert.Null(rolePermission.Permission); // Navigation property is null by default since we marked it as null!
    }

    [Fact]
    public void RolePermission_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var permission = new Permission
        {
            Id = 1,
            Code = "MANAGE_MOVEMENT_TYPES",
            Name = "Manage Movement Types"
        };

        // Act
        var rolePermission = new RolePermission
        {
            Id = 1,
            Role = UserRole.Manager,
            PermissionId = 1,
            Permission = permission,
            TenantId = "tenant-1"
        };

        // Assert
        Assert.Equal(1, rolePermission.Id);
        Assert.Equal(UserRole.Manager, rolePermission.Role);
        Assert.Equal(1, rolePermission.PermissionId);
        Assert.Equal(permission, rolePermission.Permission);
        Assert.Equal("tenant-1", rolePermission.TenantId);
    }

    [Fact]
    public void RolePermission_ShouldInheritFromTenantEntity()
    {
        // Arrange & Act
        var rolePermission = new RolePermission { TenantId = "test-tenant" };

        // Assert
        Assert.Equal("test-tenant", rolePermission.TenantId);
        Assert.True(rolePermission.CreatedAt > DateTime.MinValue);
        Assert.Null(rolePermission.UpdatedAt); // UpdatedAt is nullable and starts as null
        Assert.True(rolePermission.IsActive); // Default value should be true
    }

    [Theory]
    [InlineData(UserRole.SystemAdmin)]
    [InlineData(UserRole.TenantAdmin)]
    [InlineData(UserRole.Manager)]
    [InlineData(UserRole.Employee)]
    [InlineData(UserRole.ReadOnly)]
    public void RolePermission_ShouldAcceptAllUserRoles(UserRole role)
    {
        // Act
        var rolePermission = new RolePermission { Role = role };

        // Assert
        Assert.Equal(role, rolePermission.Role);
    }
}