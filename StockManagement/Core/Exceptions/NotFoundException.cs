namespace Core.Exceptions;

/// <summary>
/// Exception thrown when a requested entity or resource is not found.
/// Provides consistent handling for "not found" scenarios across the Stock Management system.
/// </summary>
public class NotFoundException : DomainException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class 
    /// with a standardized message format for entity lookup failures.
    /// </summary>
    /// <param name="entityName">The name of the entity type that was not found.</param>
    /// <param name="key">The key or identifier that was used in the lookup.</param>
    public NotFoundException(string entityName, object key) 
        : base($"{entityName} with key '{key}' was not found.", "NOT_FOUND")
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class 
    /// with a custom message.
    /// </summary>
    /// <param name="message">The custom error message describing what was not found.</param>
    public NotFoundException(string message) 
        : base(message, "NOT_FOUND")
    {
    }
}