using Core.Entities;
using Xunit;

namespace Core.Tests.Entities;

/// <summary>
/// Unit tests for Category entity
/// </summary>
public class CategoryTests
{
    [Fact]
    public void Category_DefaultValues_ShouldBeSetCorrectly()
    {
        // Arrange & Act
        var category = new Category();

        // Assert
        Assert.Equal(0, category.Id);
        Assert.Equal(string.Empty, category.Name);
        Assert.Null(category.Description);
        Assert.Null(category.ParentCategoryId);
        Assert.Null(category.ParentCategory);
        Assert.NotNull(category.SubCategories);
        Assert.Empty(category.SubCategories);
        Assert.NotNull(category.Products);
        Assert.Empty(category.Products);
        // Inherited from TenantEntity
        Assert.Equal(string.Empty, category.TenantId);
        Assert.True(category.IsActive);
        Assert.True(category.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void Category_SetProperties_ShouldRetainValues()
    {
        // Arrange
        var category = new Category();
        var testName = "Electronics";
        var testDescription = "Electronic products and devices";
        var testParentId = 1;

        // Act
        category.Id = 123;
        category.Name = testName;
        category.Description = testDescription;
        category.ParentCategoryId = testParentId;

        // Assert
        Assert.Equal(123, category.Id);
        Assert.Equal(testName, category.Name);
        Assert.Equal(testDescription, category.Description);
        Assert.Equal(testParentId, category.ParentCategoryId);
    }

    [Fact]
    public void Category_HierarchicalStructure_ShouldSupportParentChild()
    {
        // Arrange
        var parentCategory = new Category
        {
            Id = 1,
            Name = "Electronics"
        };

        var childCategory = new Category
        {
            Id = 2,
            Name = "Smartphones",
            ParentCategoryId = 1,
            ParentCategory = parentCategory
        };

        // Act
        parentCategory.SubCategories.Add(childCategory);

        // Assert
        Assert.Single(parentCategory.SubCategories);
        Assert.Contains(childCategory, parentCategory.SubCategories);
        Assert.Equal(parentCategory, childCategory.ParentCategory);
        Assert.Equal(1, childCategory.ParentCategoryId);
    }

    [Theory]
    [InlineData("Electronics")]
    [InlineData("Food & Beverage")]
    [InlineData("Multi-Word Category Name")]
    [InlineData("UPPERCASE")]
    [InlineData("lowercase")]
    public void Category_Name_ShouldAcceptVariousFormats(string name)
    {
        // Arrange & Act
        var category = new Category { Name = name };

        // Assert
        Assert.Equal(name, category.Name);
    }

    [Fact]
    public void Category_NullDescription_ShouldBeAllowed()
    {
        // Arrange & Act
        var category = new Category
        {
            Name = "Test Category",
            Description = null
        };

        // Assert
        Assert.Equal("Test Category", category.Name);
        Assert.Null(category.Description);
    }
}