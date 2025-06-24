using Core.Entities;
using Xunit;

namespace Core.Tests.Entities;

/// <summary>
/// Unit tests for StockMovement entity
/// </summary>
public class StockMovementTests
{
    [Fact]
    public void StockMovement_DefaultValues_ShouldBeSetCorrectly()
    {
        // Arrange & Act
        var stockMovement = new StockMovement();

        // Assert
        Assert.Equal(0, stockMovement.Id);
        Assert.Equal(0, stockMovement.ProductId);
        Assert.Equal(0, stockMovement.Quantity);
        Assert.Equal(0, stockMovement.MovementTypeId);
        Assert.Null(stockMovement.Reference);
        Assert.Null(stockMovement.Notes);
        Assert.Null(stockMovement.UnitPrice);
        Assert.Null(stockMovement.SupplierId);
        Assert.Equal(0, stockMovement.CreatedByUserId);
        // Inherited from TenantEntity
        Assert.Equal(string.Empty, stockMovement.TenantId);
        Assert.True(stockMovement.IsActive);
        Assert.True(stockMovement.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void StockMovement_SetProperties_ShouldRetainValues()
    {
        // Arrange
        var stockMovement = new StockMovement();
        var testReference = "INV-2024-001";
        var testNotes = "Initial stock purchase";
        var testUnitPrice = 49.99m;

        // Act
        stockMovement.Id = 123;
        stockMovement.ProductId = 456;
        stockMovement.Quantity = 100;
        stockMovement.MovementTypeId = 1;
        stockMovement.Reference = testReference;
        stockMovement.Notes = testNotes;
        stockMovement.UnitPrice = testUnitPrice;
        stockMovement.SupplierId = 789;
        stockMovement.CreatedByUserId = 321;
        stockMovement.TenantId = "tenant-123";

        // Assert
        Assert.Equal(123, stockMovement.Id);
        Assert.Equal(456, stockMovement.ProductId);
        Assert.Equal(100, stockMovement.Quantity);
        Assert.Equal(1, stockMovement.MovementTypeId);
        Assert.Equal(testReference, stockMovement.Reference);
        Assert.Equal(testNotes, stockMovement.Notes);
        Assert.Equal(testUnitPrice, stockMovement.UnitPrice);
        Assert.Equal(789, stockMovement.SupplierId);
        Assert.Equal(321, stockMovement.CreatedByUserId);
        Assert.Equal("tenant-123", stockMovement.TenantId);
    }

    [Theory]
    [InlineData(1, 1, 1)]    // Purchase: quantity 1, direction +1 = +1
    [InlineData(5, 1, 5)]    // Purchase: quantity 5, direction +1 = +5
    [InlineData(10, -1, -10)] // Sale: quantity 10, direction -1 = -10
    [InlineData(3, -1, -3)]   // Loss: quantity 3, direction -1 = -3
    public void StockMovement_CalculatedQuantity_ShouldApplyDirection(int quantity, int direction, int expectedCalculated)
    {
        // Arrange
        var movementType = new MovementType
        {
            Direction = direction
        };
        
        var stockMovement = new StockMovement
        {
            Quantity = quantity,
            MovementType = movementType
        };

        // Act
        var calculatedQuantity = stockMovement.CalculatedQuantity;

        // Assert
        Assert.Equal(expectedCalculated, calculatedQuantity);
    }

    [Fact]
    public void StockMovement_PurchaseMovement_ShouldHaveSupplierAndPositiveDirection()
    {
        // Arrange
        var purchaseMovementType = new MovementType
        {
            Name = "Purchase",
            Code = "PURCHASE",
            Direction = 1 // Incoming stock
        };

        var stockMovement = new StockMovement
        {
            ProductId = 123,
            Quantity = 50,
            MovementType = purchaseMovementType,
            SupplierId = 456, // Should have supplier for purchases
            UnitPrice = 25.00m,
            Reference = "PO-2024-001",
            CreatedByUserId = 789
        };

        // Act & Assert
        Assert.Equal(50, stockMovement.Quantity);
        Assert.Equal(1, purchaseMovementType.Direction);
        Assert.Equal(50, stockMovement.CalculatedQuantity); // 50 * 1 = +50
        Assert.Equal(456, stockMovement.SupplierId);
        Assert.Equal(25.00m, stockMovement.UnitPrice);
        Assert.Equal("PO-2024-001", stockMovement.Reference);
    }

    [Fact]
    public void StockMovement_SaleMovement_ShouldNotRequireSupplierAndHaveNegativeDirection()
    {
        // Arrange
        var saleMovementType = new MovementType
        {
            Name = "Sale",
            Code = "SALE",
            Direction = -1 // Outgoing stock
        };

        var stockMovement = new StockMovement
        {
            ProductId = 123,
            Quantity = 25,
            MovementType = saleMovementType,
            SupplierId = null, // Sales typically don't have suppliers
            Reference = "SO-2024-001",
            CreatedByUserId = 789
        };

        // Act & Assert
        Assert.Equal(25, stockMovement.Quantity);
        Assert.Equal(-1, saleMovementType.Direction);
        Assert.Equal(-25, stockMovement.CalculatedQuantity); // 25 * -1 = -25
        Assert.Null(stockMovement.SupplierId);
        Assert.Equal("SO-2024-001", stockMovement.Reference);
    }

    [Fact]
    public void StockMovement_WithoutUnitPrice_ShouldAllowNullValue()
    {
        // Arrange & Act
        var stockMovement = new StockMovement
        {
            ProductId = 123,
            Quantity = 10,
            MovementTypeId = 1,
            UnitPrice = null // Unit price might not always be available
        };

        // Assert
        Assert.Null(stockMovement.UnitPrice);
    }

    [Theory]
    [InlineData("INV-2024-001")]
    [InlineData("PO-12345")]
    [InlineData("ADJ-2024-0015")]
    [InlineData(null)] // Reference is optional
    public void StockMovement_Reference_ShouldAcceptVariousFormats(string reference)
    {
        // Arrange
        var stockMovement = new StockMovement();

        // Act
        stockMovement.Reference = reference;

        // Assert
        Assert.Equal(reference, stockMovement.Reference);
    }
}