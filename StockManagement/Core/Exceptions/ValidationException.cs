namespace Core.Exceptions;

/// <summary>
/// Exception thrown when validation rules are violated.
/// Supports both single property validation errors and multiple validation errors.
/// </summary>
public class ValidationException : DomainException
{
    /// <summary>
    /// Gets the dictionary of validation errors organized by property name.
    /// Each property can have multiple error messages.
    /// </summary>
    public Dictionary<string, string[]> Errors { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class 
    /// with multiple validation errors.
    /// </summary>
    /// <param name="message">The general validation error message.</param>
    /// <param name="errors">Dictionary of validation errors organized by property name.</param>
    public ValidationException(string message, Dictionary<string, string[]> errors) 
        : base(message, "VALIDATION_ERROR")
    {
        Errors = errors;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class 
    /// with a single property validation error.
    /// </summary>
    /// <param name="propertyName">The name of the property that failed validation.</param>
    /// <param name="errorMessage">The validation error message for the property.</param>
    public ValidationException(string propertyName, string errorMessage) 
        : base($"Validation failed for {propertyName}: {errorMessage}", "VALIDATION_ERROR")
    {
        Errors = new Dictionary<string, string[]>
        {
            { propertyName, new[] { errorMessage } }
        };
    }
}