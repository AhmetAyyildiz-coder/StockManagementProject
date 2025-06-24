using Core.Entities.Base;
using Xunit;

namespace Core.Tests.Entities.Base;

/// <summary>
/// Test entity implementing IEntity with int identifier
/// </summary>
public class TestEntityWithIntId : IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Test entity implementing IEntity with string identifier
/// </summary>
public class TestEntityWithStringId : IEntity<string>
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Test entity implementing IEntity with Guid identifier
/// </summary>
public class TestEntityWithGuidId : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Combined entity implementing both IEntity and TenantEntity
/// </summary>
public class TestTenantEntityWithId : TenantEntity, IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Unit tests for IEntity generic interface
/// </summary>
public class IEntityTests
{
    [Fact]
    public void IEntity_IntId_ShouldBeSettableAndGettable()
    {
        // Arrange
        var entity = new TestEntityWithIntId();
        const int id = 12345;

        // Act
        entity.Id = id;

        // Assert
        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void IEntity_StringId_ShouldBeSettableAndGettable()
    {
        // Arrange
        var entity = new TestEntityWithStringId();
        const string id = "test-entity-123";

        // Act
        entity.Id = id;

        // Assert
        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void IEntity_GuidId_ShouldBeSettableAndGettable()
    {
        // Arrange
        var entity = new TestEntityWithGuidId();
        var id = Guid.NewGuid();

        // Act
        entity.Id = id;

        // Assert
        Assert.Equal(id, entity.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public void IEntity_IntId_ShouldAcceptVariousIntValues(int id)
    {
        // Arrange & Act
        var entity = new TestEntityWithIntId { Id = id };

        // Assert
        Assert.Equal(id, entity.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("abc-123")]
    [InlineData("UPPERCASE")]
    [InlineData("with spaces")]
    [InlineData("with-special-chars!@#")]
    public void IEntity_StringId_ShouldAcceptVariousStringValues(string id)
    {
        // Arrange & Act
        var entity = new TestEntityWithStringId { Id = id };

        // Assert
        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void IEntity_GuidId_ShouldAcceptEmptyGuid()
    {
        // Arrange & Act
        var entity = new TestEntityWithGuidId { Id = Guid.Empty };

        // Assert
        Assert.Equal(Guid.Empty, entity.Id);
    }

    [Fact]
    public void IEntity_MultipleInstances_ShouldHaveIndependentIds()
    {
        // Arrange & Act
        var entity1 = new TestEntityWithIntId { Id = 1 };
        var entity2 = new TestEntityWithIntId { Id = 2 };
        var entity3 = new TestEntityWithStringId { Id = "string-id" };

        // Assert
        Assert.Equal(1, entity1.Id);
        Assert.Equal(2, entity2.Id);
        Assert.Equal("string-id", entity3.Id);
    }

    [Fact]
    public void IEntity_CombinedWithTenantEntity_ShouldWorkCorrectly()
    {
        // Arrange
        var entity = new TestTenantEntityWithId();
        const int id = 100;
        const string tenantId = "tenant-123";

        // Act
        entity.Id = id;
        entity.TenantId = tenantId;
        entity.Name = "Test Entity";

        // Assert - IEntity properties
        Assert.Equal(id, entity.Id);
        
        // Assert - TenantEntity properties
        Assert.Equal(tenantId, entity.TenantId);
        Assert.True(entity.IsActive);
        Assert.True(entity.CreatedAt <= DateTime.UtcNow);
        
        // Assert - Custom properties
        Assert.Equal("Test Entity", entity.Name);
    }

    [Fact]
    public void IEntity_RepositoryPattern_Scenario()
    {
        // Arrange - Simulate repository pattern usage
        var entities = new List<IEntity<int>>
        {
            new TestEntityWithIntId { Id = 1, Name = "Entity 1" },
            new TestTenantEntityWithId { Id = 2, Name = "Entity 2", TenantId = "tenant1" }
        };

        // Act - Find by ID (generic repository method simulation)
        var foundEntity = entities.FirstOrDefault(e => e.Id == 2);

        // Assert
        Assert.NotNull(foundEntity);
        Assert.Equal(2, foundEntity.Id);
        
        // Verify it's the tenant entity
        if (foundEntity is TestTenantEntityWithId tenantEntity)
        {
            Assert.Equal("tenant1", tenantEntity.TenantId);
            Assert.Equal("Entity 2", tenantEntity.Name);
        }
    }

    [Fact]
    public void IEntity_GenericConstraint_ShouldSupportDifferentTypes()
    {
        // This test verifies that IEntity<T> can work with different types
        // which is important for generic repository pattern implementation

        // Arrange & Act
        IEntity<int> intEntity = new TestEntityWithIntId { Id = 1 };
        IEntity<string> stringEntity = new TestEntityWithStringId { Id = "test" };
        IEntity<Guid> guidEntity = new TestEntityWithGuidId { Id = Guid.NewGuid() };

        // Assert - Type system should work correctly
        Assert.IsAssignableFrom<IEntity<int>>(intEntity);
        Assert.IsAssignableFrom<IEntity<string>>(stringEntity);
        Assert.IsAssignableFrom<IEntity<Guid>>(guidEntity);

        // Assert - Values should be correct
        Assert.Equal(1, intEntity.Id);
        Assert.Equal("test", stringEntity.Id);
        Assert.NotEqual(Guid.Empty, guidEntity.Id);
    }
}