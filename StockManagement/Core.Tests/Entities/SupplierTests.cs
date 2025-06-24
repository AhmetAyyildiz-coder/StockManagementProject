using Core.Entities;
using Xunit;

namespace Core.Tests.Entities;

/// <summary>
/// Unit tests for Supplier entity
/// </summary>
public class SupplierTests
{
    [Fact]
    public void Supplier_DefaultValues_ShouldBeSetCorrectly()
    {
        // Arrange & Act
        var supplier = new Supplier();

        // Assert
        Assert.Equal(0, supplier.Id);
        Assert.Equal(string.Empty, supplier.Name);
        Assert.Null(supplier.ContactInfo);
        Assert.Null(supplier.Notes);
        Assert.NotNull(supplier.StockMovements);
        Assert.Empty(supplier.StockMovements);
        // Inherited from TenantEntity
        Assert.Equal(string.Empty, supplier.TenantId);
        Assert.True(supplier.IsActive);
        Assert.True(supplier.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void Supplier_SetProperties_ShouldRetainValues()
    {
        // Arrange
        var supplier = new Supplier();
        var testName = "ACME Corporation";
        var testContactInfo = "contact@acme.com, +1-555-0123";
        var testNotes = "Reliable supplier for electronics";

        // Act
        supplier.Id = 123;
        supplier.Name = testName;
        supplier.ContactInfo = testContactInfo;
        supplier.Notes = testNotes;
        supplier.TenantId = "tenant-123";

        // Assert
        Assert.Equal(123, supplier.Id);
        Assert.Equal(testName, supplier.Name);
        Assert.Equal(testContactInfo, supplier.ContactInfo);
        Assert.Equal(testNotes, supplier.Notes);
        Assert.Equal("tenant-123", supplier.TenantId);
    }

    [Fact]
    public void Supplier_WithStockMovements_ShouldMaintainCollection()
    {
        // Arrange
        var supplier = new Supplier
        {
            Id = 123,
            Name = "Test Supplier"
        };

        var stockMovement1 = new StockMovement
        {
            Id = 1,
            ProductId = 456,
            Quantity = 100,
            SupplierId = supplier.Id
        };

        var stockMovement2 = new StockMovement
        {
            Id = 2,
            ProductId = 789,
            Quantity = 50,
            SupplierId = supplier.Id
        };

        // Act
        supplier.StockMovements.Add(stockMovement1);
        supplier.StockMovements.Add(stockMovement2);

        // Assert
        Assert.Equal(2, supplier.StockMovements.Count);
        Assert.Contains(stockMovement1, supplier.StockMovements);
        Assert.Contains(stockMovement2, supplier.StockMovements);
    }

    [Theory]
    [InlineData("ABC Corp")]
    [InlineData("Supplier with Special Characters & Co.")]
    [InlineData("Çünkü Türkçe Karakterler")]
    [InlineData("123 Numeric Supplier")]
    public void Supplier_Name_ShouldAcceptVariousFormats(string supplierName)
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.Name = supplierName;

        // Assert
        Assert.Equal(supplierName, supplier.Name);
    }

    [Theory]
    [InlineData("email@example.com")]
    [InlineData("phone: +1-555-0123")]
    [InlineData("email@example.com, phone: +1-555-0123, address: 123 Main St")]
    [InlineData(null)] // Contact info is optional
    public void Supplier_ContactInfo_ShouldAcceptVariousFormats(string contactInfo)
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.ContactInfo = contactInfo;

        // Assert
        Assert.Equal(contactInfo, supplier.ContactInfo);
    }
}