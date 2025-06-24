namespace Core.Exceptions;

/// <summary>
/// Base exception class for all domain-specific exceptions in the Stock Management system.
/// Provides a common foundation for business logic errors and domain rule violations.
/// </summary>
public abstract class DomainException : Exception
{
    /// <summary>
    /// Gets the error code associated with this exception.
    /// Used for categorization and localization of error messages.
    /// </summary>
    public string ErrorCode { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    protected DomainException()
    {
        ErrorCode = GetType().Name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="errorCode">The error code for categorization. If null, uses the exception type name.</param>
    protected DomainException(string message, string? errorCode = null) : base(message)
    {
        ErrorCode = errorCode ?? GetType().Name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    /// <param name="errorCode">The error code for categorization. If null, uses the exception type name.</param>
    protected DomainException(string message, Exception innerException, string? errorCode = null) : base(message, innerException)
    {
        ErrorCode = errorCode ?? GetType().Name;
    }
}