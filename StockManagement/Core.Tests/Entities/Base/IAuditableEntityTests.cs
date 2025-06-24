using Core.Entities.Base;
using Xunit;

namespace Core.Tests.Entities.Base;

/// <summary>
/// Concrete implementation of IAuditableEntity for testing purposes
/// </summary>
public class TestAuditableEntity : IAuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}

/// <summary>
/// Unit tests for IAuditableEntity interface
/// </summary>
public class IAuditableEntityTests
{
    [Fact]
    public void IAuditableEntity_Properties_ShouldBeSettableAndGettable()
    {
        // Arrange
        var entity = new TestAuditableEntity();
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow.AddHours(1);
        const string createdBy = "user123";
        const string updatedBy = "user456";

        // Act
        entity.CreatedAt = createdAt;
        entity.UpdatedAt = updatedAt;
        entity.CreatedBy = createdBy;
        entity.UpdatedBy = updatedBy;

        // Assert
        Assert.Equal(createdAt, entity.CreatedAt);
        Assert.Equal(updatedAt, entity.UpdatedAt);
        Assert.Equal(createdBy, entity.CreatedBy);
        Assert.Equal(updatedBy, entity.UpdatedBy);
    }

    [Fact]
    public void IAuditableEntity_NullableProperties_ShouldAcceptNull()
    {
        // Arrange & Act
        var entity = new TestAuditableEntity
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            CreatedBy = null,
            UpdatedBy = null
        };

        // Assert
        Assert.Null(entity.UpdatedAt);
        Assert.Null(entity.CreatedBy);
        Assert.Null(entity.UpdatedBy);
        Assert.NotEqual(default(DateTime), entity.CreatedAt);
    }

    [Theory]
    [InlineData("admin")]
    [InlineData("user@example.com")]
    [InlineData("123")]
    [InlineData("SYSTEM")]
    [InlineData("")]
    public void IAuditableEntity_CreatedBy_ShouldAcceptVariousUserIdentifiers(string createdBy)
    {
        // Arrange & Act
        var entity = new TestAuditableEntity { CreatedBy = createdBy };

        // Assert
        Assert.Equal(createdBy, entity.CreatedBy);
    }

    [Theory]
    [InlineData("admin")]
    [InlineData("user@example.com")]
    [InlineData("123")]
    [InlineData("SYSTEM")]
    [InlineData("")]
    public void IAuditableEntity_UpdatedBy_ShouldAcceptVariousUserIdentifiers(string updatedBy)
    {
        // Arrange & Act
        var entity = new TestAuditableEntity { UpdatedBy = updatedBy };

        // Assert
        Assert.Equal(updatedBy, entity.UpdatedBy);
    }

    [Fact]
    public void IAuditableEntity_AuditTrail_Scenario()
    {
        // Arrange
        var entity = new TestAuditableEntity();
        var createdAt = DateTime.UtcNow;
        const string creator = "admin";

        // Act - Initial creation
        entity.CreatedAt = createdAt;
        entity.CreatedBy = creator;

        // Assert - Creation audit
        Assert.Equal(createdAt, entity.CreatedAt);
        Assert.Equal(creator, entity.CreatedBy);
        Assert.Null(entity.UpdatedAt);
        Assert.Null(entity.UpdatedBy);

        // Act - Update
        var updatedAt = DateTime.UtcNow.AddMinutes(5);
        const string updater = "user123";
        entity.UpdatedAt = updatedAt;
        entity.UpdatedBy = updater;

        // Assert - Update audit
        Assert.Equal(createdAt, entity.CreatedAt); // Creation info preserved
        Assert.Equal(creator, entity.CreatedBy);
        Assert.Equal(updatedAt, entity.UpdatedAt);
        Assert.Equal(updater, entity.UpdatedBy);
    }

    [Fact]
    public void IAuditableEntity_MultipleUpdates_ShouldTrackLatestUpdate()
    {
        // Arrange
        var entity = new TestAuditableEntity
        {
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "admin"
        };

        // Act - First update
        entity.UpdatedAt = DateTime.UtcNow.AddMinutes(5);
        entity.UpdatedBy = "user1";
        
        var firstUpdate = entity.UpdatedAt;
        var firstUpdater = entity.UpdatedBy;

        // Act - Second update
        entity.UpdatedAt = DateTime.UtcNow.AddMinutes(10);
        entity.UpdatedBy = "user2";

        // Assert - Should have latest update info
        Assert.NotEqual(firstUpdate, entity.UpdatedAt);
        Assert.NotEqual(firstUpdater, entity.UpdatedBy);
        Assert.Equal("user2", entity.UpdatedBy);
        Assert.True(entity.UpdatedAt > firstUpdate);
    }
}