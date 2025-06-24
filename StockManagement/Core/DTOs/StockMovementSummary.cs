namespace Core.DTOs;

/// <summary>
/// Data transfer object representing a summary of a stock movement for reporting and analysis purposes.
/// Contains essential information about stock movements with calculated running totals.
/// </summary>
public class StockMovementSummary
{
    /// <summary>
    /// Gets or sets the unique identifier of the stock movement.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the movement occurred.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the name of the movement type (e.g., "Purchase", "Sale", "Adjustment").
    /// </summary>
    public string MovementTypeName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the code of the movement type for system identification.
    /// </summary>
    public string MovementTypeCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity involved in the movement (always positive).
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the direction of the movement (+1 for in, -1 for out).
    /// </summary>
    public int Direction { get; set; }

    /// <summary>
    /// Gets or sets the calculated running total of stock after this movement.
    /// </summary>
    public int RunningTotal { get; set; }

    /// <summary>
    /// Gets or sets the unit price at the time of movement, if applicable.
    /// </summary>
    public decimal? UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the reference document or transaction identifier.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Gets or sets additional notes or comments about the movement.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the username of the person who created this movement.
    /// </summary>
    public string CreatedByUserName { get; set; } = string.Empty;
}