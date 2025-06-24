using Core.Entities;
using System.Reflection;
using Xunit;

namespace Core.Tests.Entities;

/// <summary>
/// Unit tests for Product entity
/// </summary>
public class ProductTests
{
    [Fact]
    public void Product_DefaultValues_ShouldBeSetCorrectly()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.Equal(0, product.Id);
        Assert.Equal(string.Empty, product.Name);
        Assert.Equal(string.Empty, product.SKU);
        Assert.Null(product.Barcode);
        Assert.Equal(0, product.CategoryId);
        Assert.Equal(0, product.MinStockLevel);
        Assert.Null(product.Description);
        Assert.NotNull(product.StockMovements);
        Assert.Empty(product.StockMovements);
        // Inherited from TenantEntity
        Assert.Equal(string.Empty, product.TenantId);
        Assert.True(product.IsActive);
        Assert.True(product.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void Product_SetProperties_ShouldRetainValues()
    {
        // Arrange
        var product = new Product();
        var testName = "iPhone 15";
        var testSKU = "IPH15-128GB-BLU";
        var testBarcode = "1234567890123";
        var testDescription = "Latest iPhone model";

        // Act
        product.Id = 123;
        product.Name = testName;
        product.SKU = testSKU;
        product.Barcode = testBarcode;
        product.CategoryId = 5;
        product.MinStockLevel = 10;
        product.Description = testDescription;

        // Assert
        Assert.Equal(123, product.Id);
        Assert.Equal(testName, product.Name);
        Assert.Equal(testSKU, product.SKU);
        Assert.Equal(testBarcode, product.Barcode);
        Assert.Equal(5, product.CategoryId);
        Assert.Equal(10, product.MinStockLevel);
        Assert.Equal(testDescription, product.Description);
    }

    [Fact]
    public void Product_CategoryRelationship_ShouldWork()
    {
        // Arrange
        var category = new Category
        {
            Id = 1,
            Name = "Electronics"
        };

        var product = new Product
        {
            Id = 123,
            Name = "Test Product",
            CategoryId = 1,
            Category = category
        };

        // Act
        category.Products.Add(product);

        // Assert
        Assert.Equal(category, product.Category);
        Assert.Equal(1, product.CategoryId);
        Assert.Contains(product, category.Products);
    }

    [Theory]
    [InlineData("PROD-001")]
    [InlineData("SKU-12345")]
    [InlineData("ABC_123_XYZ")]
    [InlineData("product-sku-2024")]
    public void Product_SKU_ShouldAcceptVariousFormats(string sku)
    {
        // Arrange & Act
        var product = new Product { SKU = sku };

        // Assert
        Assert.Equal(sku, product.SKU);
    }

    [Fact]
    public void Product_OptionalFields_ShouldAllowNull()
    {
        // Arrange & Act
        var product = new Product
        {
            Name = "Test Product",
            SKU = "TEST-001",
            Barcode = null,
            Description = null
        };

        // Assert
        Assert.Equal("Test Product", product.Name);
        Assert.Equal("TEST-001", product.SKU);
        Assert.Null(product.Barcode);
        Assert.Null(product.Description);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void Product_MinStockLevel_ShouldAcceptValidValues(int minStockLevel)
    {
        // Arrange & Act
        var product = new Product { MinStockLevel = minStockLevel };

        // Assert
        Assert.Equal(minStockLevel, product.MinStockLevel);
    }

    [Theory]
    [InlineData(0, 10, true)]  // Current stock 0, min level 10 -> low stock
    [InlineData(5, 10, true)]  // Current stock 5, min level 10 -> low stock
    [InlineData(10, 10, true)] // Current stock 10, min level 10 -> low stock (equal)
    [InlineData(15, 10, false)] // Current stock 15, min level 10 -> not low stock
    [InlineData(0, 0, true)]   // Current stock 0, min level 0 -> low stock (equal)
    public void Product_IsLowStock_ShouldReturnCorrectValue(int currentStock, int minStockLevel, bool expectedIsLowStock)
    {
        // Arrange
        var product = new Product { MinStockLevel = minStockLevel };
        // Note: In real implementation, CurrentStock would be set by the infrastructure layer
        // For testing, we're testing the logic directly
        
        // Using reflection to set the private setter for testing purposes
        var currentStockProperty = typeof(Product).GetProperty(nameof(Product.CurrentStock));
        currentStockProperty?.SetValue(product, currentStock);

        // Act
        var isLowStock = product.IsLowStock();

        // Assert
        Assert.Equal(expectedIsLowStock, isLowStock);
    }

    [Theory]
    [InlineData(0, false)]  // No stock
    [InlineData(1, true)]   // Has stock
    [InlineData(10, true)]  // Has stock
    [InlineData(-1, false)] // Edge case: negative stock (should not happen in practice)
    public void Product_HasStock_ShouldReturnCorrectValue(int currentStock, bool expectedHasStock)
    {
        // Arrange
        var product = new Product();
        
        // Using reflection to set the private setter for testing purposes
        var currentStockProperty = typeof(Product).GetProperty(nameof(Product.CurrentStock));
        currentStockProperty?.SetValue(product, currentStock);

        // Act
        var hasStock = product.HasStock();

        // Assert
        Assert.Equal(expectedHasStock, hasStock);
    }

    [Fact]
    public void Product_StockMovements_ShouldBeInitializedAsEmptyCollection()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.NotNull(product.StockMovements);
        Assert.Empty(product.StockMovements);
        Assert.IsAssignableFrom<ICollection<StockMovement>>(product.StockMovements);
    }

    [Fact]
    public void Product_CurrentStock_ShouldDefaultToZero()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.Equal(0, product.CurrentStock);
    }
}