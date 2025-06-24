namespace Core.Exceptions;

/// <summary>
/// Exception thrown when a user attempts to perform an action they are not authorized to execute.
/// Used for role-based access control violations and permission checks.
/// </summary>
public class UnauthorizedException : DomainException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the authorization error.</param>
    public UnauthorizedException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the authorization failure.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}