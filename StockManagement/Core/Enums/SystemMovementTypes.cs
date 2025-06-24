namespace Core.Enums;

/// <summary>
/// Defines system-defined movement types that are automatically created for each tenant.
/// These types cannot be modified or deleted by tenant users and provide standard stock movement operations.
/// </summary>
public enum SystemMovementTypes
{
    /// <summary>
    /// Purchase movement type (+1 Direction) - Incoming stock from suppliers.
    /// </summary>
    Purchase = 1,
    
    /// <summary>
    /// Sale movement type (-1 Direction) - Outgoing stock to customers.
    /// </summary>
    Sale = 2,
    
    /// <summary>
    /// Loss movement type (-1 Direction) - Stock lost due to damage, theft, or other reasons.
    /// </summary>
    Loss = 3,
    
    /// <summary>
    /// Found movement type (+1 Direction) - Stock found during inventory counts.
    /// </summary>
    Found = 4,
    
    /// <summary>
    /// Return movement type (+1 Direction) - Returned stock from customers.
    /// </summary>
    Return = 5,
    
    /// <summary>
    /// Damage movement type (-1 Direction) - Stock marked as damaged and removed from inventory.
    /// </summary>
    Damage = 6
}