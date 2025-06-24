namespace Core.Constants;

/// <summary>
/// Defines permission constants used for role-based authorization in the Stock Management system.
/// These constants are used throughout the application to control access to various features and operations.
/// </summary>
public static class Permissions
{
    #region Stock Management Permissions
    
    /// <summary>
    /// Permission to manage movement types (create, update, delete stock movement types).
    /// Typically granted to Manager role and above.
    /// </summary>
    public const string MANAGE_MOVEMENT_TYPES = "MANAGE_MOVEMENT_TYPES";
    
    /// <summary>
    /// Permission to create stock movements (stock in/out operations).
    /// Available to Employee role and above.
    /// </summary>
    public const string CREATE_STOCK_MOVEMENT = "CREATE_STOCK_MOVEMENT";
    
    /// <summary>
    /// Permission to view stock reports and analytics.
    /// Available to ReadOnly role and above.
    /// </summary>
    public const string VIEW_STOCK_REPORTS = "VIEW_STOCK_REPORTS";
    
    #endregion
    
    #region Product Management Permissions
    
    /// <summary>
    /// Permission to manage products (create, update, delete products).
    /// Typically granted to Manager role and above.
    /// </summary>
    public const string MANAGE_PRODUCTS = "MANAGE_PRODUCTS";
    
    /// <summary>
    /// Permission to view products and product information.
    /// Available to ReadOnly role and above.
    /// </summary>
    public const string VIEW_PRODUCTS = "VIEW_PRODUCTS";
    
    #endregion
    
    #region User Management Permissions
    
    /// <summary>
    /// Permission to manage users within the tenant (create, update, delete users).
    /// Typically granted to TenantAdmin role.
    /// </summary>
    public const string MANAGE_USERS = "MANAGE_USERS";
    
    /// <summary>
    /// Permission to view users and user information within the tenant.
    /// Available to Manager role and above.
    /// </summary>
    public const string VIEW_USERS = "VIEW_USERS";
    
    #endregion
    
    #region Supplier Management Permissions
    
    /// <summary>
    /// Permission to manage suppliers (create, update, delete suppliers).
    /// Typically granted to Manager role and above.
    /// </summary>
    public const string MANAGE_SUPPLIERS = "MANAGE_SUPPLIERS";
    
    /// <summary>
    /// Permission to view suppliers and supplier information.
    /// Available to ReadOnly role and above.
    /// </summary>
    public const string VIEW_SUPPLIERS = "VIEW_SUPPLIERS";
    
    #endregion
    
    #region System Management Permissions
    
    /// <summary>
    /// System Administrator permission with full access across all tenants.
    /// Reserved for SystemAdmin role only.
    /// </summary>
    public const string SYSTEM_ADMIN = "SYSTEM_ADMIN";
    
    /// <summary>
    /// Tenant Administrator permission with full access within their tenant.
    /// Reserved for TenantAdmin role only.
    /// </summary>
    public const string TENANT_ADMIN = "TENANT_ADMIN";
    
    #endregion
}