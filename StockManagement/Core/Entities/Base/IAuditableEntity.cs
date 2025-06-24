namespace Core.Entities.Base;

/// <summary>
/// Interface for entities that support audit tracking functionality.
/// Provides properties to track creation and modification metadata.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// Null indicates the entity has never been updated since creation.
    /// </summary>
    DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created the entity.
    /// Can be null if creation user tracking is not required.
    /// </summary>
    string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated the entity.
    /// Can be null if update user tracking is not required.
    /// </summary>
    string? UpdatedBy { get; set; }
}