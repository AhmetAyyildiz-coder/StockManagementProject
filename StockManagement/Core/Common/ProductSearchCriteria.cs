namespace Core.Common;

/// <summary>
/// Data transfer object containing criteria for product search operations.
/// Supports filtering, pagination, and sorting of product results.
/// </summary>
public class ProductSearchCriteria
{
    /// <summary>
    /// Gets or sets the search term to filter products by name, SKU, or barcode.
    /// </summary>
    public string? SearchTerm { get; set; }
    
    /// <summary>
    /// Gets or sets the category identifier to filter products by category.
    /// </summary>
    public int? CategoryId { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to include only active products.
    /// Null includes both active and inactive products.
    /// </summary>
    public bool? IsActive { get; set; }
    
    /// <summary>
    /// Gets or sets the page number for pagination (1-based).
    /// </summary>
    public int Page { get; set; } = 1;
    
    /// <summary>
    /// Gets or sets the number of items per page for pagination.
    /// </summary>
    public int PageSize { get; set; } = 20;
    
    /// <summary>
    /// Gets or sets the field name to sort results by.
    /// Common values: "Name", "SKU", "CreatedAt", "CategoryName".
    /// </summary>
    public string? SortBy { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to sort in descending order.
    /// False indicates ascending order.
    /// </summary>
    public bool SortDescending { get; set; } = false;
}