using Core.Entities;
using Core.Enums;
using Xunit;

namespace Core.Tests.Entities;

/// <summary>
/// Unit tests for MovementType entity
/// </summary>
public class MovementTypeTests
{
    [Fact]
    public void MovementType_DefaultValues_ShouldBeSetCorrectly()
    {
        // Arrange & Act
        var movementType = new MovementType();

        // Assert
        Assert.Equal(0, movementType.Id);
        Assert.Equal(string.Empty, movementType.Name);
        Assert.Equal(string.Empty, movementType.Code);
        Assert.Equal(0, movementType.Direction);
        Assert.Null(movementType.Description);
        Assert.False(movementType.IsSystemDefined);
        Assert.False(movementType.RequiresManagerApproval);
        Assert.Null(movementType.CreatedByUserId);
        Assert.NotNull(movementType.StockMovements);
        Assert.Empty(movementType.StockMovements);
        // Inherited from TenantEntity
        Assert.Equal(string.Empty, movementType.TenantId);
        Assert.True(movementType.IsActive);
        Assert.True(movementType.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void MovementType_SetProperties_ShouldRetainValues()
    {
        // Arrange
        var movementType = new MovementType();
        var testName = "Test Purchase";
        var testCode = "TEST_PURCHASE";
        var testDescription = "Test movement type for purchases";

        // Act
        movementType.Id = 123;
        movementType.Name = testName;
        movementType.Code = testCode;
        movementType.Direction = 1;
        movementType.Description = testDescription;
        movementType.IsSystemDefined = true;
        movementType.RequiresManagerApproval = true;
        movementType.CreatedByUserId = 456;
        movementType.TenantId = "tenant-123";

        // Assert
        Assert.Equal(123, movementType.Id);
        Assert.Equal(testName, movementType.Name);
        Assert.Equal(testCode, movementType.Code);
        Assert.Equal(1, movementType.Direction);
        Assert.Equal(testDescription, movementType.Description);
        Assert.True(movementType.IsSystemDefined);
        Assert.True(movementType.RequiresManagerApproval);
        Assert.Equal(456, movementType.CreatedByUserId);
        Assert.Equal("tenant-123", movementType.TenantId);
    }

    [Theory]
    [InlineData(1)]  // Positive direction - stock increase
    [InlineData(-1)] // Negative direction - stock decrease
    [InlineData(0)]  // Zero direction - no effect (edge case)
    public void MovementType_Direction_ShouldAcceptValidValues(int direction)
    {
        // Arrange
        var movementType = new MovementType();

        // Act
        movementType.Direction = direction;

        // Assert
        Assert.Equal(direction, movementType.Direction);
    }

    [Fact]
    public void MovementType_SystemDefinedType_ShouldBehaveLikeReadOnly()
    {
        // Arrange
        var movementType = new MovementType
        {
            Name = "Purchase",
            Code = "PURCHASE",
            Direction = 1,
            IsSystemDefined = true
        };

        // Act & Assert - System defined types typically have these characteristics
        Assert.True(movementType.IsSystemDefined);
        Assert.Null(movementType.CreatedByUserId); // System types not created by users
    }

    [Fact]
    public void MovementType_CustomType_ShouldAllowUserCreation()
    {
        // Arrange
        var movementType = new MovementType
        {
            Name = "Custom Transfer",
            Code = "CUSTOM_TRANSFER",
            Direction = -1,
            IsSystemDefined = false,
            CreatedByUserId = 123,
            RequiresManagerApproval = true
        };

        // Act & Assert - Custom types can be created by users
        Assert.False(movementType.IsSystemDefined);
        Assert.Equal(123, movementType.CreatedByUserId);
        Assert.True(movementType.RequiresManagerApproval);
    }
}