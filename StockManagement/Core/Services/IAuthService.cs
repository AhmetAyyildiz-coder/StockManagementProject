using Core.Entities;

namespace Core.Services;

/// <summary>
/// Service interface for authentication and security operations.
/// Provides JWT token management and password authentication capabilities.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to generate the token.</param>
    /// <returns>A task representing the asynchronous operation that returns the JWT token.</returns>
    Task<string> GenerateJwtTokenAsync(User user);

    /// <summary>
    /// Validates a JWT token and returns the associated user if valid.
    /// </summary>
    /// <param name="token">The JWT token to validate.</param>
    /// <returns>A task representing the asynchronous operation that returns the user if token is valid, null otherwise.</returns>
    Task<User?> ValidateTokenAsync(string token);

    /// <summary>
    /// Validates a password against the user's stored password hash.
    /// </summary>
    /// <param name="user">The user whose password to validate.</param>
    /// <param name="password">The plain text password to validate.</param>
    /// <returns>A task representing the asynchronous operation that returns true if password is valid.</returns>
    Task<bool> ValidatePasswordAsync(User user, string password);

    /// <summary>
    /// Hashes a plain text password using a secure hashing algorithm.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <returns>A task representing the asynchronous operation that returns the hashed password.</returns>
    Task<string> HashPasswordAsync(string password);
}