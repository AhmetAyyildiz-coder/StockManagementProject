using Core.DTOs;
using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Constants;
using Xunit;

namespace Core.Tests.Integration;

/// <summary>
/// Integration tests for the permission system components.
/// Validates that entities, DTOs, and helpers work together correctly.
/// </summary>
public class PermissionSystemIntegrationTests
{
    [Fact]
    public void Permission_Entity_ShouldHaveCorrectProperties()
    {
        // Arrange & Act
        var permission = new Permission
        {
            Id = 1,
            Name = "Manage Movement Types",
            Code = "MANAGE_MOVEMENT_TYPES",
            Description = "Allows management of stock movement types",
            Module = "Stock",
            IsSystemDefined = true,
            TenantId = "tenant-1"
        };

        // Assert
        Assert.Equal(1, permission.Id);
        Assert.Equal("MANAGE_MOVEMENT_TYPES", permission.Code);
        Assert.Equal("Stock", permission.Module);
        Assert.True(permission.IsSystemDefined);
        Assert.Equal("tenant-1", permission.TenantId);
    }

    [Fact]
    public void RolePermission_Entity_ShouldMapRoleToPermission()
    {
        // Arrange
        var permission = new Permission
        {
            Id = 1,
            Code = "MANAGE_MOVEMENT_TYPES",
            Name = "Manage Movement Types",
            Module = "Stock"
        };

        // Act
        var rolePermission = new RolePermission
        {
            Id = 1,
            Role = UserRole.Manager,
            PermissionId = permission.Id,
            Permission = permission,
            TenantId = "tenant-1"
        };

        // Assert
        Assert.Equal(UserRole.Manager, rolePermission.Role);
        Assert.Equal(permission.Id, rolePermission.PermissionId);
        Assert.Equal(permission, rolePermission.Permission);
    }

    [Fact]
    public void CreatePermissionRequest_DTO_ShouldValidateCorrectly()
    {
        // Arrange & Act
        var request = new CreatePermissionRequest
        {
            Name = "Test Permission",
            Code = "TEST_PERMISSION",
            Description = "A test permission",
            Module = "Test"
        };

        // Assert
        Assert.Equal("TEST_PERMISSION", request.Code);
        Assert.Equal("Test", request.Module);
        Assert.True(AuthorizationHelper.IsValidPermissionCode(request.Code));
    }

    [Fact]
    public void AuthorizationResult_DTO_ShouldTrackAuthorizationStatus()
    {
        // Arrange & Act
        var result = new AuthorizationResult
        {
            IsAuthorized = false,
            Reason = "Missing required permission",
            RequiredPermissions = { "MANAGE_MOVEMENT_TYPES", "CREATE_STOCK_MOVEMENT" },
            MissingPermissions = { "MANAGE_MOVEMENT_TYPES" }
        };

        // Assert
        Assert.False(result.IsAuthorized);
        Assert.Contains("MANAGE_MOVEMENT_TYPES", result.RequiredPermissions);
        Assert.Contains("MANAGE_MOVEMENT_TYPES", result.MissingPermissions);
    }

    [Fact]
    public void PermissionConstants_ShouldAllBeValid()
    {
        // Arrange - Get all permission constants using reflection
        var permissionFields = typeof(Permissions).GetFields()
            .Where(f => f.IsPublic && f.IsStatic && f.FieldType == typeof(string))
            .ToList();

        // Act & Assert
        foreach (var field in permissionFields)
        {
            var permissionCode = (string)field.GetValue(null)!;
            Assert.True(AuthorizationHelper.IsValidPermissionCode(permissionCode),
                $"Permission constant {field.Name} with value '{permissionCode}' is not valid");
        }

        // Verify we have some constants
        Assert.True(permissionFields.Count > 5, "Should have multiple permission constants defined");
    }

    [Fact]
    public void AuthorizationHelper_WithPermissionList_ShouldWorkCorrectly()
    {
        // Arrange
        var user = new User { Role = UserRole.Manager, TenantId = "tenant-1" };
        var permissions = new List<Permission>
        {
            new Permission { Code = Permissions.MANAGE_MOVEMENT_TYPES, Name = "Manage Movement Types" },
            new Permission { Code = Permissions.VIEW_PRODUCTS, Name = "View Products" }
        };

        // Act & Assert - Should work with existing permissions
        AuthorizationHelper.EnsureHasPermission(user, permissions, Permissions.MANAGE_MOVEMENT_TYPES);
        
        // Should work with any permission
        Assert.True(AuthorizationHelper.HasAnyPermission(user, permissions, 
            Permissions.MANAGE_MOVEMENT_TYPES, Permissions.MANAGE_USERS));
        
        // Should fail with all permissions when one is missing
        Assert.False(AuthorizationHelper.HasAllPermissions(user, permissions, 
            Permissions.MANAGE_MOVEMENT_TYPES, Permissions.MANAGE_USERS));
    }
}