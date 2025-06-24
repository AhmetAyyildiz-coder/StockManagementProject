namespace Core.Exceptions;

/// <summary>
/// Exception thrown when a user attempts to use a movement type that is not allowed for their role or context.
/// Provides information about the authorization failure for audit and user feedback purposes.
/// </summary>
public class MovementTypeNotAllowedException : DomainException
{
    /// <summary>
    /// Gets the identifier of the movement type that was not allowed.
    /// </summary>
    public int MovementTypeId { get; }

    /// <summary>
    /// Gets the identifier of the user who attempted to use the movement type.
    /// </summary>
    public int UserId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MovementTypeNotAllowedException"/> class.
    /// </summary>
    /// <param name="movementTypeId">The identifier of the movement type that was not allowed.</param>
    /// <param name="userId">The identifier of the user who attempted the operation.</param>
    public MovementTypeNotAllowedException(int movementTypeId, int userId)
        : base($"Movement type {movementTypeId} is not allowed for user {userId}", "MOVEMENT_TYPE_NOT_ALLOWED")
    {
        MovementTypeId = movementTypeId;
        UserId = userId;
    }
}