namespace Core.Entities.Enums;

/// <summary>
/// Defines the available user roles within a tenant.
/// Used for role-based authorization and permission management.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Tenant administrator with full access to all tenant resources and settings.
    /// </summary>
    TenantAdmin = 0,

    /// <summary>
    /// Manager with elevated permissions for stock management operations.
    /// </summary>
    Manager = 1,

    /// <summary>
    /// Regular employee with basic stock management permissions.
    /// </summary>
    Employee = 2
}