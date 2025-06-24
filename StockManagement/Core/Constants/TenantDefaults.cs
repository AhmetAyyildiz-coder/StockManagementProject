using Core.Enums;

namespace Core.Constants;

/// <summary>
/// Defines default data and configurations used during tenant initialization.
/// Contains predefined categories, movement types, and role permissions for new tenants.
/// </summary>
public static class TenantDefaults
{
    #region Default Categories
    
    /// <summary>
    /// Default product categories created for every new tenant.
    /// These provide a basic categorization structure for products.
    /// </summary>
    public static readonly List<string> DefaultCategories = new()
    {
        "Genel",
        "Elektronik", 
        "Giyim",
        "Ev & Yaşam",
        "Kitap & Kırtasiye"
    };
    
    #endregion
    
    #region Default Movement Types
    
    /// <summary>
    /// Default stock movement types with their configurations (Name, Code, Direction, IsSystemDefined).
    /// Direction: 1 = Stock In, -1 = Stock Out, IsSystemDefined: true = Cannot be modified by tenant.
    /// </summary>
    public static readonly Dictionary<string, (string Name, string Code, int Direction, bool IsSystemDefined)> DefaultMovementTypes = new()
    {
        { "PURCHASE", ("Mal Alımı", "PURCHASE", 1, true) },
        { "SALE", ("Satış", "SALE", -1, true) },
        { "LOSS", ("Fire/Kayıp", "LOSS", -1, true) },
        { "FOUND", ("Bulunan Ürün", "FOUND", 1, true) },
        { "RETURN", ("Müşteri İadesi", "RETURN", 1, true) },
        { "DAMAGE", ("Hasarlı Ürün", "DAMAGE", -1, true) }
    };
    
    #endregion
    
    #region Default Role Permissions
    
    /// <summary>
    /// Default permissions assigned to each user role when a tenant is initialized.
    /// This ensures consistent permission structure across all new tenants.
    /// </summary>
    public static readonly Dictionary<UserRole, List<string>> DefaultRolePermissions = new()
    {
        { 
            UserRole.TenantAdmin, 
            new List<string> 
            { 
                Permissions.MANAGE_MOVEMENT_TYPES, 
                Permissions.CREATE_STOCK_MOVEMENT, 
                Permissions.VIEW_STOCK_REPORTS, 
                Permissions.MANAGE_PRODUCTS, 
                Permissions.MANAGE_USERS, 
                Permissions.MANAGE_SUPPLIERS,
                Permissions.TENANT_ADMIN
            } 
        },
        { 
            UserRole.Manager, 
            new List<string> 
            { 
                Permissions.MANAGE_MOVEMENT_TYPES, 
                Permissions.CREATE_STOCK_MOVEMENT,
                Permissions.VIEW_STOCK_REPORTS, 
                Permissions.MANAGE_PRODUCTS,
                Permissions.VIEW_USERS
            } 
        },
        { 
            UserRole.Employee, 
            new List<string> 
            { 
                Permissions.CREATE_STOCK_MOVEMENT, 
                Permissions.VIEW_PRODUCTS,
                Permissions.VIEW_STOCK_REPORTS
            } 
        },
        { 
            UserRole.ReadOnly, 
            new List<string> 
            { 
                Permissions.VIEW_PRODUCTS, 
                Permissions.VIEW_STOCK_REPORTS
            } 
        }
    };
    
    #endregion
}