namespace Core.Enums;

/// <summary>
/// Defines the result of stock movement validation operations.
/// Used to categorize and handle different types of validation failures.
/// </summary>
public enum StockValidationResult
{
    /// <summary>
    /// The stock movement is valid and can be processed.
    /// </summary>
    Valid = 0,

    /// <summary>
    /// The requested quantity exceeds available stock.
    /// Applies to outbound movements when current stock is insufficient.
    /// </summary>
    InsufficientStock = 1,

    /// <summary>
    /// The specified product was not found in the system.
    /// </summary>
    ProductNotFound = 2,

    /// <summary>
    /// The movement type is not allowed for the current user or context.
    /// </summary>
    MovementTypeNotAllowed = 3,

    /// <summary>
    /// The requested quantity exceeds system or business rule limits.
    /// </summary>
    QuantityTooLarge = 4,

    /// <summary>
    /// The movement requires manager approval before processing.
    /// </summary>
    RequiresApproval = 5,

    /// <summary>
    /// The current user is not authorized to perform this movement.
    /// </summary>
    UnauthorizedUser = 6
}