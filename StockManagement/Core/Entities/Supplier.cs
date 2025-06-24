using Core.Entities.Base;

namespace Core.Entities;

/// <summary>
/// Represents a supplier entity in the inventory management system.
/// Basic implementation to support stock movement tracking - full implementation planned for Phase 3.
/// Inherits from TenantEntity to ensure proper tenant isolation.
/// </summary>
public class Supplier : TenantEntity, IEntity<int>
{
    /// <summary>
    /// Gets or sets the unique identifier for the supplier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the supplier company.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the contact information for the supplier.
    /// Optional field for basic contact details.
    /// </summary>
    public string? ContactInfo { get; set; }

    /// <summary>
    /// Gets or sets additional notes about the supplier.
    /// Optional field for additional supplier information.
    /// </summary>
    public string? Notes { get; set; }

    // Navigation Properties

    /// <summary>
    /// Gets or sets the collection of stock movements associated with this supplier.
    /// </summary>
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}