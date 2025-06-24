namespace Core.Common;

/// <summary>
/// Represents a paged result containing items and pagination metadata.
/// Used to support pagination in data access operations.
/// </summary>
/// <typeparam name="T">The type of items in the paged result</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// Gets or sets the items in the current page.
    /// </summary>
    public IEnumerable<T> Items { get; set; } = new List<T>();
    
    /// <summary>
    /// Gets or sets the total count of items across all pages.
    /// </summary>
    public int TotalCount { get; set; }
    
    /// <summary>
    /// Gets or sets the current page number (1-based).
    /// </summary>
    public int Page { get; set; }
    
    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Gets the total number of pages based on TotalCount and PageSize.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    
    /// <summary>
    /// Gets a value indicating whether there is a next page available.
    /// </summary>
    public bool HasNext => Page < TotalPages;
    
    /// <summary>
    /// Gets a value indicating whether there is a previous page available.
    /// </summary>
    public bool HasPrevious => Page > 1;
}