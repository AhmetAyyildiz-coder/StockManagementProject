using Core.Entities;
using Xunit;

namespace Core.Tests.Entities;

/// <summary>
/// Unit tests for Permission entity
/// </summary>
public class PermissionTests
{
    [Fact]
    public void Permission_ShouldHaveDefaultValues()
    {
        // Act
        var permission = new Permission();

        // Assert
        Assert.Equal(0, permission.Id);
        Assert.Equal(string.Empty, permission.Name);
        Assert.Equal(string.Empty, permission.Code);
        Assert.Null(permission.Description);
        Assert.Equal(string.Empty, permission.Module);
        Assert.False(permission.IsSystemDefined);
        Assert.Empty(permission.RolePermissions);
    }

    [Fact]
    public void Permission_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        const string name = "Hareket Tiplerini Yönet";
        const string code = "MANAGE_MOVEMENT_TYPES";
        const string description = "Allows management of movement types";
        const string module = "Stock";
        const bool isSystemDefined = true;

        // Act
        var permission = new Permission
        {
            Id = 1,
            Name = name,
            Code = code,
            Description = description,
            Module = module,
            IsSystemDefined = isSystemDefined,
            TenantId = "tenant-1"
        };

        // Assert
        Assert.Equal(1, permission.Id);
        Assert.Equal(name, permission.Name);
        Assert.Equal(code, permission.Code);
        Assert.Equal(description, permission.Description);
        Assert.Equal(module, permission.Module);
        Assert.Equal(isSystemDefined, permission.IsSystemDefined);
        Assert.Equal("tenant-1", permission.TenantId);
    }

    [Fact]
    public void Permission_ShouldInheritFromTenantEntity()
    {
        // Arrange & Act
        var permission = new Permission { TenantId = "test-tenant" };

        // Assert
        Assert.Equal("test-tenant", permission.TenantId);
        Assert.True(permission.CreatedAt > DateTime.MinValue);
        Assert.Null(permission.UpdatedAt); // UpdatedAt is nullable and starts as null
        Assert.True(permission.IsActive); // Default value should be true
    }
}