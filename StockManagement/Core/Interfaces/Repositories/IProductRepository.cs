using Core.Common;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for product-specific operations.
/// Extends the generic repository with product-specific business logic.
/// </summary>
public interface IProductRepository : IGenericRepository<Product, int>
{
    /// <summary>
    /// Gets a product by its SKU (Stock Keeping Unit).
    /// </summary>
    /// <param name="sku">The SKU to search for</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<Product?> GetBySKUAsync(string sku);
    
    /// <summary>
    /// Gets a product by its barcode.
    /// </summary>
    /// <param name="barcode">The barcode to search for</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<Product?> GetByBarcodeAsync(string barcode);
    
    /// <summary>
    /// Gets all products belonging to a specific category.
    /// </summary>
    /// <param name="categoryId">The category identifier</param>
    /// <returns>Collection of products in the specified category</returns>
    Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
    
    /// <summary>
    /// Searches products by name using partial matching.
    /// </summary>
    /// <param name="searchTerm">The search term to match against product names</param>
    /// <returns>Collection of products matching the search term</returns>
    Task<IEnumerable<Product>> SearchByNameAsync(string searchTerm);
    
    /// <summary>
    /// Checks if a SKU is unique within a tenant.
    /// </summary>
    /// <param name="sku">The SKU to check</param>
    /// <param name="tenantId">The tenant identifier</param>
    /// <param name="excludeProductId">Optional product ID to exclude from the check (for updates)</param>
    /// <returns>True if the SKU is unique within the tenant, false otherwise</returns>
    Task<bool> IsSKUUniqueInTenantAsync(string sku, string tenantId, int? excludeProductId = null);
    
    /// <summary>
    /// Searches products using comprehensive criteria with pagination and sorting.
    /// </summary>
    /// <param name="criteria">The search criteria containing filters, pagination, and sorting options</param>
    /// <returns>Paged result containing matching products and pagination metadata</returns>
    Task<PagedResult<Product>> SearchProductsAsync(ProductSearchCriteria criteria);
}