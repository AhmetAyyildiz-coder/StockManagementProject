using Core.DTOs;
using Core.Entities;
using Core.Enums;

namespace Core.Interfaces.Services;

/// <summary>
/// Service interface for managing movement types in the stock management system.
/// Provides CRUD operations and business logic for stock movement type configuration.
/// </summary>
public interface IMovementTypeService
{
    #region CRUD Operations

    /// <summary>
    /// Creates a new movement type based on the provided request data.
    /// </summary>
    /// <param name="request">The request containing movement type creation data.</param>
    /// <returns>A task that represents the asynchronous operation containing the created movement type.</returns>
    Task<MovementType> CreateMovementTypeAsync(CreateMovementTypeRequest request);

    /// <summary>
    /// Updates an existing movement type with the provided data.
    /// </summary>
    /// <param name="id">The identifier of the movement type to update.</param>
    /// <param name="request">The request containing updated movement type data.</param>
    /// <returns>A task that represents the asynchronous operation containing the updated movement type.</returns>
    Task<MovementType> UpdateMovementTypeAsync(int id, UpdateMovementTypeRequest request);

    /// <summary>
    /// Deletes a movement type by setting it as inactive (soft delete).
    /// System-defined movement types cannot be deleted.
    /// </summary>
    /// <param name="id">The identifier of the movement type to delete.</param>
    /// <returns>A task that represents the asynchronous operation containing true if deleted successfully, false otherwise.</returns>
    Task<bool> DeleteMovementTypeAsync(int id);

    /// <summary>
    /// Retrieves a specific movement type by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the movement type.</param>
    /// <returns>A task that represents the asynchronous operation containing the movement type, or null if not found.</returns>
    Task<MovementType?> GetMovementTypeAsync(int id);

    /// <summary>
    /// Retrieves all movement types for the current tenant, optionally including inactive ones.
    /// </summary>
    /// <param name="includeInactive">Whether to include inactive movement types in the results.</param>
    /// <returns>A task that represents the asynchronous operation containing the list of movement types.</returns>
    Task<List<MovementType>> GetMovementTypesAsync(bool includeInactive = false);

    #endregion

    #region Business Logic

    /// <summary>
    /// Retrieves movement types that are available for a specific user based on their role and permissions.
    /// </summary>
    /// <param name="userId">The identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation containing the list of available movement types.</returns>
    Task<List<MovementType>> GetAvailableMovementTypesForUserAsync(int userId);

    /// <summary>
    /// Determines whether a specific user can use a particular movement type.
    /// </summary>
    /// <param name="userId">The identifier of the user.</param>
    /// <param name="movementTypeId">The identifier of the movement type.</param>
    /// <returns>A task that represents the asynchronous operation containing true if the user can use the movement type, false otherwise.</returns>
    Task<bool> CanUserUseMovementTypeAsync(int userId, int movementTypeId);

    /// <summary>
    /// Determines whether a movement requires manager approval based on the movement type, quantity, and value.
    /// </summary>
    /// <param name="movementTypeId">The identifier of the movement type.</param>
    /// <param name="quantity">The quantity being moved.</param>
    /// <param name="totalValue">The optional total value of the movement.</param>
    /// <returns>A task that represents the asynchronous operation containing true if approval is required, false otherwise.</returns>
    Task<bool> RequiresApprovalAsync(int movementTypeId, int quantity, decimal? totalValue = null);

    #endregion

    #region System Operations

    /// <summary>
    /// Initializes default system-defined movement types for a new tenant.
    /// This should be called during tenant setup to ensure all necessary movement types are available.
    /// </summary>
    /// <param name="tenantId">The identifier of the tenant for which to initialize movement types.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task InitializeDefaultMovementTypesAsync(string tenantId);

    /// <summary>
    /// Retrieves a system-defined movement type by its system type enumeration.
    /// </summary>
    /// <param name="systemType">The system-defined movement type to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation containing the movement type, or null if not found.</returns>
    Task<MovementType?> GetSystemMovementTypeAsync(SystemMovementTypes systemType);

    #endregion
}