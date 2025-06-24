namespace Core.DTOs;

/// <summary>
/// Data transfer object for searching and filtering stock movements.
/// Provides flexible criteria for querying stock movement history with pagination support.
/// </summary>
public class StockMovementSearchCriteria
{
    /// <summary>
    /// Gets or sets the product identifier to filter movements by specific product.
    /// </summary>
    public int? ProductId { get; set; }

    /// <summary>
    /// Gets or sets the movement type identifier to filter by movement type.
    /// </summary>
    public int? MovementTypeId { get; set; }

    /// <summary>
    /// Gets or sets the category identifier to filter movements by product category.
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the start date for filtering movements by date range.
    /// </summary>
    public DateTime? FromDate { get; set; }

    /// <summary>
    /// Gets or sets the end date for filtering movements by date range.
    /// </summary>
    public DateTime? ToDate { get; set; }

    /// <summary>
    /// Gets or sets the reference to search for in movement references.
    /// Supports partial matching for finding related transactions.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Gets or sets the user identifier to filter movements created by specific user.
    /// </summary>
    public int? CreatedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the page number for pagination (1-based).
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page for pagination.
    /// </summary>
    public int PageSize { get; set; } = 50;

    /// <summary>
    /// Gets or sets the field name to sort results by.
    /// Common values include "Date", "ProductName", "MovementType", "Quantity".
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to sort in descending order.
    /// Default is true to show most recent movements first.
    /// </summary>
    public bool SortDescending { get; set; } = true;
}