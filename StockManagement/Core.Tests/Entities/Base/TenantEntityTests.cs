using Core.Entities.Base;
using Xunit;

namespace Core.Tests.Entities.Base;

/// <summary>
/// Concrete implementation of TenantEntity for testing purposes
/// </summary>
public class TestEntity : TenantEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Unit tests for TenantEntity abstract base class
/// </summary>
public class TenantEntityTests
{
    [Fact]
    public void TenantEntity_DefaultValues_ShouldBeSetCorrectly()
    {
        // Arrange & Act
        var entity = new TestEntity();

        // Assert
        Assert.Equal(string.Empty, entity.TenantId);
        Assert.True(entity.IsActive);
        Assert.Null(entity.UpdatedAt);
        Assert.True(entity.CreatedAt <= DateTime.UtcNow);
        Assert.True(entity.CreatedAt > DateTime.UtcNow.AddSeconds(-1)); // Created within last second
    }

    [Fact]
    public void TenantEntity_SetTenantId_ShouldRetainValue()
    {
        // Arrange
        var entity = new TestEntity();
        const string tenantId = "test-tenant-123";

        // Act
        entity.TenantId = tenantId;

        // Assert
        Assert.Equal(tenantId, entity.TenantId);
    }

    [Fact]
    public void TenantEntity_SetIsActive_ShouldRetainValue()
    {
        // Arrange
        var entity = new TestEntity();

        // Act
        entity.IsActive = false;

        // Assert
        Assert.False(entity.IsActive);

        // Act
        entity.IsActive = true;

        // Assert
        Assert.True(entity.IsActive);
    }

    [Fact]
    public void TenantEntity_SetUpdatedAt_ShouldRetainValue()
    {
        // Arrange
        var entity = new TestEntity();
        var updatedTime = DateTime.UtcNow.AddHours(-1);

        // Act
        entity.UpdatedAt = updatedTime;

        // Assert
        Assert.Equal(updatedTime, entity.UpdatedAt);
    }

    [Fact]
    public void TenantEntity_CreatedAt_ShouldUseUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var entity = new TestEntity();
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.True(entity.CreatedAt >= beforeCreation);
        Assert.True(entity.CreatedAt <= afterCreation);
        Assert.Equal(DateTimeKind.Utc, entity.CreatedAt.Kind);
    }

    [Theory]
    [InlineData("")]
    [InlineData("tenant-1")]
    [InlineData("TENANT_2")]
    [InlineData("multi-word-tenant")]
    [InlineData("tenant_with_underscore")]
    public void TenantEntity_TenantId_ShouldAcceptVariousFormats(string tenantId)
    {
        // Arrange & Act
        var entity = new TestEntity { TenantId = tenantId };

        // Assert
        Assert.Equal(tenantId, entity.TenantId);
    }

    [Fact]
    public void TenantEntity_MultipleInstances_ShouldHaveIndependentValues()
    {
        // Arrange & Act
        var entity1 = new TestEntity { TenantId = "tenant-1", IsActive = true };
        var entity2 = new TestEntity { TenantId = "tenant-2", IsActive = false };

        // Assert
        Assert.Equal("tenant-1", entity1.TenantId);
        Assert.Equal("tenant-2", entity2.TenantId);
        Assert.True(entity1.IsActive);
        Assert.False(entity2.IsActive);
    }

    [Fact]
    public void TenantEntity_SoftDelete_Scenario()
    {
        // Arrange
        var entity = new TestEntity { TenantId = "test-tenant" };
        Assert.True(entity.IsActive); // Initially active

        // Act - Soft delete
        entity.IsActive = false;
        entity.UpdatedAt = DateTime.UtcNow;

        // Assert
        Assert.False(entity.IsActive);
        Assert.NotNull(entity.UpdatedAt);
        Assert.Equal("test-tenant", entity.TenantId); // Other properties remain unchanged
    }
}