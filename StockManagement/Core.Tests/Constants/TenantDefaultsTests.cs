using Core.Constants;
using Core.Enums;
using Xunit;

namespace Core.Tests.Constants;

/// <summary>
/// Unit tests for TenantDefaults constants class
/// </summary>
public class TenantDefaultsTests
{
    [Fact]
    public void DefaultCategories_ShouldContainExpectedItems()
    {
        // Act
        var categories = TenantDefaults.DefaultCategories;
        
        // Assert
        Assert.NotNull(categories);
        Assert.NotEmpty(categories);
        Assert.Contains("Genel", categories);
        Assert.Contains("Elektronik", categories);
        Assert.Contains("Giyim", categories);
        Assert.Contains("Ev & Yaşam", categories);
        Assert.Contains("Kitap & Kırtasiye", categories);
    }
    
    [Fact]
    public void DefaultMovementTypes_ShouldContainSystemDefinedTypes()
    {
        // Act
        var movementTypes = TenantDefaults.DefaultMovementTypes;
        
        // Assert
        Assert.NotNull(movementTypes);
        Assert.NotEmpty(movementTypes);
        
        // Check key movement types exist
        Assert.True(movementTypes.ContainsKey("PURCHASE"));
        Assert.True(movementTypes.ContainsKey("SALE"));
        Assert.True(movementTypes.ContainsKey("LOSS"));
        
        // Verify PURCHASE details
        var purchase = movementTypes["PURCHASE"];
        Assert.Equal("Mal Alımı", purchase.Name);
        Assert.Equal("PURCHASE", purchase.Code);
        Assert.Equal(1, purchase.Direction); // Stock In
        Assert.True(purchase.IsSystemDefined);
        
        // Verify SALE details
        var sale = movementTypes["SALE"];
        Assert.Equal("Satış", sale.Name);
        Assert.Equal("SALE", sale.Code);
        Assert.Equal(-1, sale.Direction); // Stock Out
        Assert.True(sale.IsSystemDefined);
    }
    
    [Fact]
    public void DefaultRolePermissions_ShouldContainAllRoles()
    {
        // Act
        var rolePermissions = TenantDefaults.DefaultRolePermissions;
        
        // Assert
        Assert.NotNull(rolePermissions);
        Assert.NotEmpty(rolePermissions);
        
        // Check all roles are present
        Assert.True(rolePermissions.ContainsKey(UserRole.TenantAdmin));
        Assert.True(rolePermissions.ContainsKey(UserRole.Manager));
        Assert.True(rolePermissions.ContainsKey(UserRole.Employee));
        Assert.True(rolePermissions.ContainsKey(UserRole.ReadOnly));
    }
    
    [Fact]
    public void TenantAdmin_ShouldHaveMostPermissions()
    {
        // Act
        var tenantAdminPermissions = TenantDefaults.DefaultRolePermissions[UserRole.TenantAdmin];
        
        // Assert
        Assert.Contains(Permissions.TENANT_ADMIN, tenantAdminPermissions);
        Assert.Contains(Permissions.MANAGE_USERS, tenantAdminPermissions);
        Assert.Contains(Permissions.MANAGE_PRODUCTS, tenantAdminPermissions);
        Assert.Contains(Permissions.MANAGE_MOVEMENT_TYPES, tenantAdminPermissions);
    }
    
    [Fact]
    public void ReadOnly_ShouldHaveMinimalPermissions()
    {
        // Act
        var readOnlyPermissions = TenantDefaults.DefaultRolePermissions[UserRole.ReadOnly];
        
        // Assert
        Assert.Contains(Permissions.VIEW_PRODUCTS, readOnlyPermissions);
        Assert.Contains(Permissions.VIEW_STOCK_REPORTS, readOnlyPermissions);
        Assert.DoesNotContain(Permissions.MANAGE_PRODUCTS, readOnlyPermissions);
        Assert.DoesNotContain(Permissions.CREATE_STOCK_MOVEMENT, readOnlyPermissions);
    }
}