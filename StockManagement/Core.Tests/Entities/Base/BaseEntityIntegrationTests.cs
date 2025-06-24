using Core.Entities.Base;
using Xunit;

namespace Core.Tests.Entities.Base;

/// <summary>
/// Complete entity implementing all base interfaces
/// Represents a real-world scenario where an entity needs all functionality
/// </summary>
public class CompleteTestEntity : TenantEntity, IEntity<int>, IAuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // IAuditableEntity adds additional user tracking beyond TenantEntity
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}

/// <summary>
/// Integration tests for base entity infrastructure working together
/// </summary>
public class BaseEntityIntegrationTests
{
    [Fact]
    public void CompleteEntity_AllInterfaces_ShouldWorkTogether()
    {
        // Arrange
        var entity = new CompleteTestEntity();
        
        // Act - Set all properties
        entity.Id = 1;
        entity.Name = "Test Product";
        entity.TenantId = "tenant-abc";
        entity.CreatedBy = "admin";
        entity.IsActive = true;
        
        // Simulate update
        entity.UpdatedAt = DateTime.UtcNow.AddMinutes(5);
        entity.UpdatedBy = "user123";

        // Assert - All interfaces work correctly
        
        // IEntity<int>
        Assert.Equal(1, entity.Id);
        
        // TenantEntity
        Assert.Equal("tenant-abc", entity.TenantId);
        Assert.True(entity.IsActive);
        Assert.True(entity.CreatedAt <= DateTime.UtcNow);
        Assert.NotNull(entity.UpdatedAt);
        
        // IAuditableEntity (additional user tracking)
        Assert.Equal("admin", entity.CreatedBy);
        Assert.Equal("user123", entity.UpdatedBy);
        
        // Custom properties
        Assert.Equal("Test Product", entity.Name);
    }

    [Fact]
    public void MultiTenantScenario_DifferentTenants_ShouldIsolateData()
    {
        // Arrange - Simulate multi-tenant scenario
        var tenant1Entity = new CompleteTestEntity
        {
            Id = 1,
            Name = "Product A",
            TenantId = "tenant-1",
            CreatedBy = "admin1"
        };

        var tenant2Entity = new CompleteTestEntity
        {
            Id = 1, // Same ID but different tenant
            Name = "Product B",
            TenantId = "tenant-2",
            CreatedBy = "admin2"
        };

        // Act & Assert - Entities should be independent despite same ID
        Assert.Equal(tenant1Entity.Id, tenant2Entity.Id); // Same ID
        Assert.NotEqual(tenant1Entity.TenantId, tenant2Entity.TenantId); // Different tenants
        Assert.NotEqual(tenant1Entity.Name, tenant2Entity.Name); // Different data
        Assert.NotEqual(tenant1Entity.CreatedBy, tenant2Entity.CreatedBy);
    }

    [Fact]
    public void AuditTrail_CompleteLifecycle_ShouldTrackAllChanges()
    {
        // Arrange
        var entity = new CompleteTestEntity();
        var createdAt = DateTime.UtcNow;

        // Act - Creation
        entity.Id = 1;
        entity.TenantId = "tenant-test";
        entity.Name = "Initial Name";
        entity.CreatedAt = createdAt;
        entity.CreatedBy = "creator";

        // Assert - Initial state
        Assert.Equal(createdAt, entity.CreatedAt);
        Assert.Equal("creator", entity.CreatedBy);
        Assert.Null(entity.UpdatedAt);
        Assert.Null(entity.UpdatedBy);
        Assert.True(entity.IsActive);

        // Act - First update
        var firstUpdateTime = DateTime.UtcNow.AddMinutes(10);
        entity.Name = "Updated Name";
        entity.UpdatedAt = firstUpdateTime;
        entity.UpdatedBy = "updater1";

        // Assert - After first update
        Assert.Equal("Updated Name", entity.Name);
        Assert.Equal(firstUpdateTime, entity.UpdatedAt);
        Assert.Equal("updater1", entity.UpdatedBy);
        Assert.Equal(createdAt, entity.CreatedAt); // Creation info preserved
        Assert.Equal("creator", entity.CreatedBy);

        // Act - Soft delete
        var deleteTime = DateTime.UtcNow.AddMinutes(20);
        entity.IsActive = false;
        entity.UpdatedAt = deleteTime;
        entity.UpdatedBy = "admin";

        // Assert - After soft delete
        Assert.False(entity.IsActive);
        Assert.Equal(deleteTime, entity.UpdatedAt);
        Assert.Equal("admin", entity.UpdatedBy);
        // All other data preserved
        Assert.Equal("Updated Name", entity.Name);
        Assert.Equal("tenant-test", entity.TenantId);
        Assert.Equal(createdAt, entity.CreatedAt);
        Assert.Equal("creator", entity.CreatedBy);
    }

    [Fact]
    public void RepositoryPattern_GenericOperations_ShouldWorkWithBaseClasses()
    {
        // Arrange - Simulate generic repository operations
        var entities = new List<CompleteTestEntity>
        {
            new() { Id = 1, TenantId = "tenant-1", Name = "Entity 1", IsActive = true },
            new() { Id = 2, TenantId = "tenant-1", Name = "Entity 2", IsActive = false },
            new() { Id = 3, TenantId = "tenant-2", Name = "Entity 3", IsActive = true }
        };

        // Act & Assert - Find by ID (IEntity<T>)
        var foundById = entities.FirstOrDefault(e => e.Id == 2);
        Assert.NotNull(foundById);
        Assert.Equal("Entity 2", foundById.Name);

        // Act & Assert - Filter by tenant (TenantEntity)
        var tenant1Entities = entities.Where(e => e.TenantId == "tenant-1").ToList();
        Assert.Equal(2, tenant1Entities.Count);
        Assert.All(tenant1Entities, e => Assert.Equal("tenant-1", e.TenantId));

        // Act & Assert - Filter active entities (TenantEntity soft delete)
        var activeEntities = entities.Where(e => e.IsActive).ToList();
        Assert.Equal(2, activeEntities.Count);
        Assert.All(activeEntities, e => Assert.True(e.IsActive));

        // Act & Assert - Combined filter (tenant + active)
        var activeTenant1Entities = entities
            .Where(e => e.TenantId == "tenant-1" && e.IsActive)
            .ToList();
        Assert.Single(activeTenant1Entities);
        Assert.Equal("Entity 1", activeTenant1Entities[0].Name);
    }

    [Fact]
    public void BaseClassHierarchy_PropertyInheritance_ShouldWorkCorrectly()
    {
        // Arrange
        var entity = new CompleteTestEntity();

        // Act - Test that all inherited properties are accessible
        
        // From IEntity<int>
        entity.Id = 100;
        
        // From TenantEntity
        entity.TenantId = "test-tenant";
        entity.IsActive = true;
        // CreatedAt is set automatically
        entity.UpdatedAt = DateTime.UtcNow;
        
        // From IAuditableEntity
        entity.CreatedBy = "system";
        entity.UpdatedBy = "user";

        // Assert - All properties should be accessible and functional
        Assert.Equal(100, entity.Id);
        Assert.Equal("test-tenant", entity.TenantId);
        Assert.True(entity.IsActive);
        Assert.True(entity.CreatedAt <= DateTime.UtcNow);
        Assert.NotNull(entity.UpdatedAt);
        Assert.Equal("system", entity.CreatedBy);
        Assert.Equal("user", entity.UpdatedBy);
    }

    [Theory]
    [InlineData("tenant-1", "user1", true)]
    [InlineData("tenant-2", "user2", false)]
    [InlineData("TENANT-3", "ADMIN", true)]
    [InlineData("tenant_4", "system", false)]
    public void BaseClasses_VariousScenarios_ShouldHandleCorrectly(
        string tenantId, string createdBy, bool isActive)
    {
        // Arrange & Act
        var entity = new CompleteTestEntity
        {
            Id = 1,
            TenantId = tenantId,
            CreatedBy = createdBy,
            IsActive = isActive,
            Name = $"Entity for {tenantId}"
        };

        // Assert
        Assert.Equal(tenantId, entity.TenantId);
        Assert.Equal(createdBy, entity.CreatedBy);
        Assert.Equal(isActive, entity.IsActive);
        Assert.Equal($"Entity for {tenantId}", entity.Name);
        Assert.True(entity.CreatedAt <= DateTime.UtcNow);
    }
}