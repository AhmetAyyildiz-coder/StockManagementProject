namespace Core.Enums;

/// <summary>
/// Defines the role hierarchy for user authorization in the multi-tenant Stock Management system.
/// Roles are ordered by privilege level, with lower numeric values indicating higher privileges.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// System Administrator with full access across all tenants and system management capabilities.
    /// Can manage system-wide settings, all tenants, and has unrestricted access.
    /// </summary>
    SystemAdmin = 1,

    /// <summary>
    /// Tenant Administrator with full privileges within their specific tenant.
    /// Typically the shop owner with complete control over their tenant's data and settings.
    /// </summary>
    TenantAdmin = 2,

    /// <summary>
    /// Manager with extended privileges including stock movement type management.
    /// Can perform management operations and configure movement types within the tenant.
    /// </summary>
    Manager = 3,

    /// <summary>
    /// Employee with basic stock operations privileges.
    /// Limited to stock entry and exit operations without management capabilities.
    /// </summary>
    Employee = 4,

    /// <summary>
    /// Read-only access for viewing data without modification privileges.
    /// Can only view reports and stock information without making changes.
    /// </summary>
    ReadOnly = 5
}