# Core Infrastructure Documentation

## Overview

This document describes the foundational infrastructure implemented in the Core project for the Stock Management SaaS application. The infrastructure supports multi-tenancy, audit trails, and the repository pattern.

## Base Entity Classes

### TenantEntity

Abstract base class for all tenant-aware entities in the multi-tenant system.

```csharp
public abstract class TenantEntity
{
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}
```

**Features:**
- **Multi-tenant support**: Automatic `TenantId` property for data isolation
- **Audit tracking**: `CreatedAt`/`UpdatedAt` timestamps with UTC timezone
- **Soft delete**: `IsActive` flag for logical deletion
- **Default values**: Proper initialization of properties

**Usage:**
```csharp
public class Product : TenantEntity, IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    // TenantId, CreatedAt, UpdatedAt, IsActive inherited automatically
}
```

### IAuditableEntity

Interface for entities requiring comprehensive audit tracking.

```csharp
public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
    string? CreatedBy { get; set; }
    string? UpdatedBy { get; set; }
}
```

**Features:**
- **User tracking**: Creation and modification user identification
- **Timestamp tracking**: Creation and update timestamps
- **Flexible implementation**: Can be combined with other base classes

**Usage:**
```csharp
public class ImportantEntity : TenantEntity, IAuditableEntity, IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // From IAuditableEntity (additional to TenantEntity)
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
```

### IEntity<T>

Generic interface for entities with typed identifiers.

```csharp
public interface IEntity<T>
{
    T Id { get; set; }
}
```

**Features:**
- **Generic repository support**: Enables type-safe repository patterns
- **Flexible identifiers**: Supports int, string, Guid, or custom types
- **Clean separation**: Standardizes entity identification

**Usage:**
```csharp
// Different identifier types
public class IntIdEntity : IEntity<int> { public int Id { get; set; } }
public class StringIdEntity : IEntity<string> { public string Id { get; set; } = string.Empty; }
public class GuidIdEntity : IEntity<Guid> { public Guid Id { get; set; } }
```

## Service Interfaces

### ICurrentTenantService

Service for managing current tenant context in multi-tenant applications.

```csharp
public interface ICurrentTenantService
{
    string TenantId { get; }
    string? TenantName { get; }
    bool HasTenant { get; }
    
    void SetTenant(string tenantId, string? tenantName = null);
    void ClearTenant();
}
```

**Purpose:**
- Provides current tenant context throughout the application
- Used by global query filters for automatic data isolation
- Enables tenant-aware service implementations

**Usage Scenario:**
```csharp
// In middleware
tenantService.SetTenant("tenant-123", "ABC Company");

// In services
if (tenantService.HasTenant)
{
    // Perform tenant-specific operations
    var data = repository.GetForTenant(tenantService.TenantId);
}
```

### ICurrentUserService

Service for managing current user context and authentication state.

```csharp
public interface ICurrentUserService
{
    string? UserId { get; }
    string? Username { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
    string? TenantId { get; }
    
    bool HasRole(string role);
    bool HasAnyRole(params string[] roles);
}
```

**Purpose:**
- Provides current user context for audit and authorization
- Enables role-based access control
- Supports user tracking in audit trails

**Usage Scenario:**
```csharp
// In services
if (userService.IsAuthenticated && userService.HasRole("Manager"))
{
    // Allow manager-specific operations
    entity.CreatedBy = userService.UserId;
}
```

## Repository Interfaces

### IRepository<TEntity, TKey>

Generic repository interface for basic CRUD operations.

```csharp
public interface IRepository<TEntity, TKey> 
    where TEntity : class, IEntity<TKey>
{
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default);
}
```

### ITenantRepository<TEntity, TKey>

Extended repository interface for tenant-aware entities.

```csharp
public interface ITenantRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : TenantEntity, IEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetActiveAsync(CancellationToken cancellationToken = default);
    Task<bool> SoftDeleteAsync(TKey id, CancellationToken cancellationToken = default);
    Task<bool> RestoreAsync(TKey id, CancellationToken cancellationToken = default);
}
```

**Features:**
- **Tenant isolation**: Automatic filtering by tenant
- **Soft delete support**: Logical deletion operations
- **Active entity filtering**: Easy access to non-deleted entities

## Architecture Patterns

### Multi-Tenancy

The infrastructure supports **Shared Database + Tenant ID Discrimination** pattern:

1. **TenantEntity** base class ensures every entity has a `TenantId`
2. **ICurrentTenantService** provides current tenant context
3. **Global query filters** (to be implemented in DbContext) automatically filter by tenant
4. **ITenantRepository** enforces tenant-aware operations

### Repository Pattern

The repository interfaces support clean separation of concerns:

1. **IEntity<T>** provides common identifier contract
2. **IRepository<TEntity, TKey>** defines standard CRUD operations
3. **ITenantRepository<TEntity, TKey>** adds tenant-specific operations
4. Generic constraints ensure type safety

### Audit Trail

The infrastructure provides flexible audit capabilities:

1. **TenantEntity** provides basic timestamp tracking
2. **IAuditableEntity** adds user tracking
3. **ICurrentUserService** provides current user context
4. Services can combine both for comprehensive audit trails

## Testing

The infrastructure includes comprehensive unit tests:

- **54 tests** for base entity classes
- **42 tests** for service interfaces  
- **Additional integration tests** showing real-world usage scenarios

### Test Coverage

- ✅ TenantEntity default values and behavior
- ✅ IAuditableEntity audit trail scenarios  
- ✅ IEntity generic type support
- ✅ ICurrentTenantService tenant management
- ✅ ICurrentUserService authentication and authorization
- ✅ Repository pattern CRUD operations
- ✅ Soft delete functionality
- ✅ Multi-tenant data isolation
- ✅ Integration scenarios

## Next Steps

This infrastructure provides the foundation for:

1. **Entity Framework DbContext** with global query filters
2. **Concrete repository implementations** 
3. **Service implementations** for tenant and user management
4. **Domain entities** (Product, Category, Supplier, etc.)
5. **API controllers** using the repository pattern
6. **Middleware** for tenant resolution and user context

## Usage Examples

### Creating a Domain Entity

```csharp
public class Product : TenantEntity, IEntity<int>, IAuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    
    // IAuditableEntity adds user tracking to TenantEntity
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
```

### Using Repository Pattern

```csharp
public class ProductService
{
    private readonly ITenantRepository<Product, int> _repository;
    private readonly ICurrentUserService _userService;
    
    public async Task<Product> CreateProductAsync(string name, string sku)
    {
        var product = new Product 
        { 
            Name = name, 
            SKU = sku,
            CreatedBy = _userService.UserId
        };
        
        return await _repository.AddAsync(product);
    }
    
    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        return await _repository.GetActiveAsync();
    }
}
```

This infrastructure enables rapid development of the Stock Management application while ensuring proper multi-tenancy, audit trails, and clean architecture patterns.