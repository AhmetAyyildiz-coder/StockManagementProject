using Core.Entities.Base;

namespace Core.Entities;

/// <summary>
/// Represents a category for organizing products in a hierarchical structure.
/// Inherits from TenantEntity to ensure proper tenant isolation.
/// </summary>
public class Category : TenantEntity, IEntity<int>
{
    /// <summary>
    /// Gets or sets the unique identifier for the category.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the category.
    /// Optional field for additional category information.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the parent category.
    /// Null indicates this is a root-level category.
    /// </summary>
    public int? ParentCategoryId { get; set; }

    // Navigation Properties

    /// <summary>
    /// Gets or sets the parent category for hierarchical organization.
    /// Null for root-level categories.
    /// </summary>
    public Category? ParentCategory { get; set; }

    /// <summary>
    /// Gets or sets the collection of sub-categories under this category.
    /// Enables hierarchical category structures.
    /// </summary>
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();

    /// <summary>
    /// Gets or sets the collection of products belonging to this category.
    /// </summary>
    public ICollection<Product> Products { get; set; } = new List<Product>();
}