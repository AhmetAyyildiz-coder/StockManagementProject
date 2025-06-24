namespace Core.Constants;

/// <summary>
/// Defines system-wide default values and configuration constants for the Stock Management system.
/// These constants are used for consistent behavior across the application and to enforce system limits.
/// </summary>
public static class SystemDefaults
{
    #region Pagination Defaults
    
    /// <summary>
    /// Default number of items per page for paginated results.
    /// </summary>
    public const int DEFAULT_PAGE_SIZE = 20;
    
    /// <summary>
    /// Maximum number of items allowed per page to prevent performance issues.
    /// </summary>
    public const int MAX_PAGE_SIZE = 100;
    
    #endregion
    
    #region Multi-tenancy Defaults
    
    /// <summary>
    /// Default schema name prefix for tenant data isolation in database.
    /// </summary>
    public const string DEFAULT_TENANT_SCHEMA = "tenant";
    
    /// <summary>
    /// System tenant identifier used for system-wide operations and data.
    /// </summary>
    public const string SYSTEM_TENANT_ID = "system";
    
    #endregion
    
    #region Stock Management Defaults
    
    /// <summary>
    /// Default minimum stock level for products when not specified.
    /// </summary>
    public const int DEFAULT_MIN_STOCK_LEVEL = 0;
    
    /// <summary>
    /// Threshold below which stock is considered critically low and requires attention.
    /// </summary>
    public const int CRITICAL_STOCK_THRESHOLD = 5;
    
    #endregion
    
    #region User Management Defaults
    
    /// <summary>
    /// Minimum required length for user passwords.
    /// </summary>
    public const int PASSWORD_MIN_LENGTH = 6;
    
    /// <summary>
    /// Maximum allowed length for user passwords.
    /// </summary>
    public const int PASSWORD_MAX_LENGTH = 50;
    
    #endregion
    
    #region Product Defaults
    
    /// <summary>
    /// Maximum allowed length for product SKU codes.
    /// </summary>
    public const int SKU_MAX_LENGTH = 50;
    
    /// <summary>
    /// Maximum allowed length for product names.
    /// </summary>
    public const int PRODUCT_NAME_MAX_LENGTH = 200;
    
    /// <summary>
    /// Maximum allowed length for product barcodes.
    /// </summary>
    public const int BARCODE_MAX_LENGTH = 50;
    
    #endregion
}