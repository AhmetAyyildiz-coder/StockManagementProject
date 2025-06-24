namespace Core.Entities.Base;

/// <summary>
/// Generic interface for entities with a typed identifier.
/// Supports the generic repository pattern by providing a common contract for entity identification.
/// </summary>
/// <typeparam name="T">The type of the entity identifier (e.g., int, string, Guid)</typeparam>
public interface IEntity<T>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    T Id { get; set; }
}