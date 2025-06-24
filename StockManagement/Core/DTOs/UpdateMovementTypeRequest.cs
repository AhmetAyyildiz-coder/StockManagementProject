namespace Core.DTOs;

/// <summary>
/// Data transfer object for updating an existing movement type.
/// Contains the modifiable properties of a movement type.
/// </summary>
public class UpdateMovementTypeRequest
{
    /// <summary>
    /// Gets or sets the display name of the movement type.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique code identifier for the movement type.
    /// Should be uppercase and contain no spaces (e.g., "PURCHASE", "SALE").
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the direction of stock change for this movement type.
    /// Use +1 for stock increases (incoming) and -1 for stock decreases (outgoing).
    /// </summary>
    public int Direction { get; set; }

    /// <summary>
    /// Gets or sets an optional description explaining when this movement type should be used.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether movements of this type require manager approval.
    /// </summary>
    public bool RequiresManagerApproval { get; set; }
}