namespace Core.Enums;

/// <summary>
/// Defines methods for calculating stock costs and valuations.
/// These methods determine how unit costs are calculated when multiple purchases occur at different prices.
/// </summary>
public enum StockCalculationMethod
{
    /// <summary>
    /// First In, First Out - assumes the oldest inventory is used first.
    /// Cost is calculated based on the chronological order of purchases.
    /// </summary>
    FIFO = 1,

    /// <summary>
    /// Last In, First Out - assumes the newest inventory is used first.
    /// Cost is calculated based on the reverse chronological order of purchases.
    /// </summary>
    LIFO = 2,

    /// <summary>
    /// Weighted Average - calculates cost based on the average of all purchases.
    /// Total cost divided by total quantity across all inventory batches.
    /// </summary>
    WeightedAverage = 3,

    /// <summary>
    /// Standard Cost - uses a predetermined fixed cost regardless of actual purchase prices.
    /// Useful for consistent pricing and budgeting purposes.
    /// </summary>
    StandardCost = 4
}