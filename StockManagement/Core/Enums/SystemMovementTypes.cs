namespace Core.Enums;

/// <summary>
/// Defines system-defined movement types that are automatically created for each tenant.
/// These types cannot be modified or deleted by tenants as they are essential for core operations.
/// </summary>
public enum SystemMovementTypes
{
    /// <summary>
    /// Purchase/Incoming stock movement (+1 Direction).
    /// Used when products are received from suppliers.
    /// </summary>
    Purchase = 1,

    /// <summary>
    /// Sale/Outgoing stock movement (-1 Direction).
    /// Used when products are sold to customers.
    /// </summary>
    Sale = 2,

    /// <summary>
    /// Loss/Shrinkage stock movement (-1 Direction).
    /// Used for damaged, expired, or lost inventory.
    /// </summary>
    Loss = 3,

    /// <summary>
    /// Found/Discovered stock movement (+1 Direction).
    /// Used when previously unknown inventory is discovered.
    /// </summary>
    Found = 4,

    /// <summary>
    /// Return/Customer return stock movement (+1 Direction).
    /// Used when customers return products to inventory.
    /// </summary>
    Return = 5,

    /// <summary>
    /// Damage/Damaged goods stock movement (-1 Direction).
    /// Used for items that are damaged and need to be removed from inventory.
    /// </summary>
    Damage = 6
}