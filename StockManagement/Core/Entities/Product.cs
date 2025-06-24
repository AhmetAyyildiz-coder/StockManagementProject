using Core.Entities.Base;

namespace Core.Entities;

/// <summary>
/// Represents a product in the inventory management system.
/// Inherits from TenantEntity to ensure proper tenant isolation.
/// </summary>
public class Product : TenantEntity, IEntity<int>
{
    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Stock Keeping Unit (SKU) for the product.
    /// Should be unique within the tenant for inventory tracking.
    /// </summary>
    public string SKU { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the barcode of the product.
    /// Optional field for barcode scanning functionality.
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the category this product belongs to.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the minimum stock level for this product.
    /// Used for low stock alerts and reorder notifications.
    /// </summary>
    public int MinStockLevel { get; set; } = 0;

    /// <summary>
    /// Gets or sets the description of the product.
    /// Optional field for additional product information.
    /// </summary>
    public string? Description { get; set; }

    // Navigation Properties

    /// <summary>
    /// Gets or sets the category that this product belongs to.
    /// </summary>
    public Category Category { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of stock movements for this product.
    /// </summary>
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    // Computed Properties

    /// <summary>
    /// Gets the current stock level for this product.
    /// This is a computed property that should be calculated by summing all stock movements.
    /// Implementation will be handled in the Infrastructure layer for performance optimization.
    /// </summary>
    public int CurrentStock { get; private set; } = 0;

    // Helper Methods

    /// <summary>
    /// Determines if the current stock is below the minimum stock level.
    /// </summary>
    /// <returns>True if current stock is at or below minimum level, false otherwise</returns>
    public bool IsLowStock() => CurrentStock <= MinStockLevel;

    /// <summary>
    /// Determines if the product has stock available.
    /// </summary>
    /// <returns>True if current stock is greater than zero, false otherwise</returns>
    public bool HasStock() => CurrentStock > 0;
}