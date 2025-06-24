using Core.Entities.Base;
using Core.Repositories;
using Xunit;

namespace Core.Tests.Repositories;

/// <summary>
/// Test entity for repository testing
/// </summary>
public class TestRepositoryEntity : TenantEntity, IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Mock implementation of ITenantRepository for testing
/// </summary>
public class MockTenantRepository : ITenantRepository<TestRepositoryEntity, int>
{
    private readonly List<TestRepositoryEntity> _entities = new();
    private int _nextId = 1;

    public Task<TestRepositoryEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(entity);
    }

    public Task<IEnumerable<TestRepositoryEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<TestRepositoryEntity>>(_entities);
    }

    public Task<TestRepositoryEntity> AddAsync(TestRepositoryEntity entity, CancellationToken cancellationToken = default)
    {
        entity.Id = _nextId++;
        entity.CreatedAt = DateTime.UtcNow;
        _entities.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<TestRepositoryEntity> UpdateAsync(TestRepositoryEntity entity, CancellationToken cancellationToken = default)
    {
        var existing = _entities.FirstOrDefault(e => e.Id == entity.Id);
        if (existing != null)
        {
            existing.Name = entity.Name;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.IsActive = entity.IsActive;
            return Task.FromResult(existing);
        }
        throw new InvalidOperationException("Entity not found");
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        if (entity != null)
        {
            _entities.Remove(entity);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        var exists = _entities.Any(e => e.Id == id);
        return Task.FromResult(exists);
    }

    public Task<IEnumerable<TestRepositoryEntity>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        var activeEntities = _entities.Where(e => e.IsActive);
        return Task.FromResult(activeEntities);
    }

    public Task<bool> SoftDeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        if (entity != null)
        {
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        if (entity != null)
        {
            entity.IsActive = true;
            entity.UpdatedAt = DateTime.UtcNow;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}

/// <summary>
/// Unit tests for repository interfaces
/// </summary>
public class IRepositoryTests
{
    [Fact]
    public async Task AddAsync_ShouldAddEntityAndSetId()
    {
        // Arrange
        var repository = new MockTenantRepository();
        var entity = new TestRepositoryEntity 
        { 
            Name = "Test Entity",
            TenantId = "tenant-1"
        };

        // Act
        var result = await repository.AddAsync(entity);

        // Assert
        Assert.NotEqual(0, result.Id);
        Assert.Equal("Test Entity", result.Name);
        Assert.Equal("tenant-1", result.TenantId);
        Assert.True(result.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnEntity()
    {
        // Arrange
        var repository = new MockTenantRepository();
        var entity = await repository.AddAsync(new TestRepositoryEntity 
        { 
            Name = "Test Entity",
            TenantId = "tenant-1"
        });

        // Act
        var result = await repository.GetByIdAsync(entity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(entity.Id, result.Id);
        Assert.Equal("Test Entity", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistentId_ShouldReturnNull()
    {
        // Arrange
        var repository = new MockTenantRepository();

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_WithExistingEntity_ShouldUpdateEntity()
    {
        // Arrange
        var repository = new MockTenantRepository();
        var entity = await repository.AddAsync(new TestRepositoryEntity 
        { 
            Name = "Original Name",
            TenantId = "tenant-1"
        });

        // Act
        entity.Name = "Updated Name";
        var result = await repository.UpdateAsync(entity);

        // Assert
        Assert.Equal("Updated Name", result.Name);
        Assert.NotNull(result.UpdatedAt);
    }

    [Fact]
    public async Task DeleteAsync_WithExistingId_ShouldDeleteEntity()
    {
        // Arrange
        var repository = new MockTenantRepository();
        var entity = await repository.AddAsync(new TestRepositoryEntity 
        { 
            Name = "Test Entity",
            TenantId = "tenant-1"
        });

        // Act
        var result = await repository.DeleteAsync(entity.Id);

        // Assert
        Assert.True(result);
        
        // Verify entity is deleted
        var deletedEntity = await repository.GetByIdAsync(entity.Id);
        Assert.Null(deletedEntity);
    }

    [Fact]
    public async Task ExistsAsync_WithExistingId_ShouldReturnTrue()
    {
        // Arrange
        var repository = new MockTenantRepository();
        var entity = await repository.AddAsync(new TestRepositoryEntity 
        { 
            Name = "Test Entity",
            TenantId = "tenant-1"
        });

        // Act
        var result = await repository.ExistsAsync(entity.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithNonExistentId_ShouldReturnFalse()
    {
        // Arrange
        var repository = new MockTenantRepository();

        // Act
        var result = await repository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetActiveAsync_ShouldReturnOnlyActiveEntities()
    {
        // Arrange
        var repository = new MockTenantRepository();
        var activeEntity = await repository.AddAsync(new TestRepositoryEntity 
        { 
            Name = "Active Entity",
            TenantId = "tenant-1"
        });
        
        var inactiveEntity = await repository.AddAsync(new TestRepositoryEntity 
        { 
            Name = "Inactive Entity",
            TenantId = "tenant-1",
            IsActive = false
        });

        // Act
        var result = await repository.GetActiveAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal(activeEntity.Id, result.First().Id);
        Assert.Equal("Active Entity", result.First().Name);
    }

    [Fact]
    public async Task SoftDeleteAsync_ShouldSetEntityInactive()
    {
        // Arrange
        var repository = new MockTenantRepository();
        var entity = await repository.AddAsync(new TestRepositoryEntity 
        { 
            Name = "Test Entity",
            TenantId = "tenant-1"
        });

        // Act
        var result = await repository.SoftDeleteAsync(entity.Id);

        // Assert
        Assert.True(result);
        
        // Verify entity is soft deleted
        var softDeletedEntity = await repository.GetByIdAsync(entity.Id);
        Assert.NotNull(softDeletedEntity);
        Assert.False(softDeletedEntity.IsActive);
        Assert.NotNull(softDeletedEntity.UpdatedAt);
    }

    [Fact]
    public async Task RestoreAsync_ShouldSetEntityActive()
    {
        // Arrange
        var repository = new MockTenantRepository();
        var entity = await repository.AddAsync(new TestRepositoryEntity 
        { 
            Name = "Test Entity",
            TenantId = "tenant-1",
            IsActive = false
        });

        // Act
        var result = await repository.RestoreAsync(entity.Id);

        // Assert
        Assert.True(result);
        
        // Verify entity is restored
        var restoredEntity = await repository.GetByIdAsync(entity.Id);
        Assert.NotNull(restoredEntity);
        Assert.True(restoredEntity.IsActive);
        Assert.NotNull(restoredEntity.UpdatedAt);
    }

    [Fact]
    public async Task RepositoryLifecycle_CompleteScenario_ShouldWork()
    {
        // Arrange
        var repository = new MockTenantRepository();

        // Act & Assert - Create
        var entity = await repository.AddAsync(new TestRepositoryEntity 
        { 
            Name = "Test Entity",
            TenantId = "tenant-1"
        });
        Assert.True(entity.Id > 0);
        Assert.True(await repository.ExistsAsync(entity.Id));

        // Act & Assert - Read
        var retrievedEntity = await repository.GetByIdAsync(entity.Id);
        Assert.NotNull(retrievedEntity);
        Assert.Equal("Test Entity", retrievedEntity.Name);

        // Act & Assert - Update
        retrievedEntity.Name = "Updated Entity";
        var updatedEntity = await repository.UpdateAsync(retrievedEntity);
        Assert.Equal("Updated Entity", updatedEntity.Name);

        // Act & Assert - Soft Delete
        Assert.True(await repository.SoftDeleteAsync(entity.Id));
        var softDeletedEntity = await repository.GetByIdAsync(entity.Id);
        Assert.NotNull(softDeletedEntity);
        Assert.False(softDeletedEntity.IsActive);

        // Act & Assert - Restore
        Assert.True(await repository.RestoreAsync(entity.Id));
        var restoredEntity = await repository.GetByIdAsync(entity.Id);
        Assert.NotNull(restoredEntity);
        Assert.True(restoredEntity.IsActive);

        // Act & Assert - Hard Delete
        Assert.True(await repository.DeleteAsync(entity.Id));
        var deletedEntity = await repository.GetByIdAsync(entity.Id);
        Assert.Null(deletedEntity);
        Assert.False(await repository.ExistsAsync(entity.Id));
    }
}