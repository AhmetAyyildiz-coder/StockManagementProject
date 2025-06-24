using Core.Entities;
using Core.Interfaces.Repositories;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for category-specific operations.
/// Extends the generic repository with category-specific business logic.
/// </summary>
public interface ICategoryRepository : IGenericRepository<Category, int>
{
    /// <summary>
    /// Gets all root-level categories (categories without a parent).
    /// </summary>
    /// <returns>Collection of root categories</returns>
    Task<IEnumerable<Category>> GetRootCategoriesAsync();
    
    /// <summary>
    /// Gets all sub-categories of a specific parent category.
    /// </summary>
    /// <param name="parentCategoryId">The parent category identifier</param>
    /// <returns>Collection of sub-categories</returns>
    Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentCategoryId);
    
    /// <summary>
    /// Gets a category with its associated products loaded.
    /// </summary>
    /// <param name="categoryId">The category identifier</param>
    /// <returns>The category with products if found, null otherwise</returns>
    Task<Category?> GetCategoryWithProductsAsync(int categoryId);
    
    /// <summary>
    /// Checks if a category has any products associated with it.
    /// </summary>
    /// <param name="categoryId">The category identifier</param>
    /// <returns>True if the category has products, false otherwise</returns>
    Task<bool> HasProductsAsync(int categoryId);
    
    /// <summary>
    /// Checks if a category has any sub-categories.
    /// </summary>
    /// <param name="categoryId">The category identifier</param>
    /// <returns>True if the category has sub-categories, false otherwise</returns>
    Task<bool> HasSubCategoriesAsync(int categoryId);
}