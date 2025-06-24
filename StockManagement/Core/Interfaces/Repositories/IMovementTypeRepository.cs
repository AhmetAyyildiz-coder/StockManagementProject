using Core.Common;
using Core.Entities;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for movement type specific operations.
/// Extends the generic repository with movement type-specific business logic.
/// </summary>
public interface IMovementTypeRepository : IGenericRepository<MovementType, int>
{
    /// <summary>
    /// Gets all active movement types for the current tenant.
    /// </summary>
    /// <returns>Collection of active movement types</returns>
    Task<IEnumerable<MovementType>> GetActiveMovementTypesAsync();
    
    /// <summary>
    /// Gets only system-defined movement types that cannot be modified by tenants.
    /// </summary>
    /// <returns>Collection of system-defined movement types</returns>
    Task<IEnumerable<MovementType>> GetSystemDefinedTypesAsync();
    
    /// <summary>
    /// Gets a movement type by its unique code within the tenant.
    /// </summary>
    /// <param name="code">The movement type code to search for</param>
    /// <returns>The movement type if found, null otherwise</returns>
    Task<MovementType?> GetByCodeAsync(string code);
    
    /// <summary>
    /// Checks if a movement type code is unique within the tenant.
    /// </summary>
    /// <param name="code">The code to check for uniqueness</param>
    /// <param name="tenantId">The tenant identifier</param>
    /// <param name="excludeId">Optional movement type ID to exclude from the check (for updates)</param>
    /// <returns>True if the code is unique within the tenant, false otherwise</returns>
    Task<bool> IsCodeUniqueInTenantAsync(string code, string tenantId, int? excludeId = null);
    
    /// <summary>
    /// Checks if a movement type has any associated stock movements.
    /// Used to prevent deletion of movement types that are in use.
    /// </summary>
    /// <param name="movementTypeId">The movement type identifier</param>
    /// <returns>True if the movement type has movements, false otherwise</returns>
    Task<bool> HasMovementsAsync(int movementTypeId);
}