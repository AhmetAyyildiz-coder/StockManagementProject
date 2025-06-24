namespace Core.DTOs;

/// <summary>
/// Data transfer object representing a comprehensive summary of product stock information.
/// Used for inventory reports, low stock alerts, and stock analysis.
/// </summary>
public class ProductStockSummary
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Stock Keeping Unit (SKU) code of the product.
    /// </summary>
    public string SKU { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the product category.
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current stock quantity available.
    /// </summary>
    public int CurrentStock { get; set; }

    /// <summary>
    /// Gets or sets the minimum stock level threshold for this product.
    /// </summary>
    public int MinStockLevel { get; set; }

    /// <summary>
    /// Gets or sets the calculated average unit cost of the current stock.
    /// </summary>
    public decimal? AverageUnitCost { get; set; }

    /// <summary>
    /// Gets or sets the total value of current stock (quantity × average unit cost).
    /// </summary>
    public decimal? TotalValue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the current stock is below the minimum threshold.
    /// </summary>
    public bool IsLowStock { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the last stock movement for this product.
    /// </summary>
    public DateTime LastMovementDate { get; set; }
}