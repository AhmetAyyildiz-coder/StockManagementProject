using Core.Entities;
using Core.Enums;
using Core.Services;
using Xunit;

namespace Core.Tests.Interfaces.Services;

/// <summary>
/// Mock implementation of IAuthService for testing
/// </summary>
public class MockAuthService : IAuthService
{
    private readonly Dictionary<string, User> _tokenUserMap = new();
    private readonly Dictionary<string, string> _userPasswordMap = new();

    public Task<string> GenerateJwtTokenAsync(User user)
    {
        var token = $"jwt-token-{user.Id}-{Guid.NewGuid():N}";
        _tokenUserMap[token] = user;
        return Task.FromResult(token);
    }

    public Task<User?> ValidateTokenAsync(string token)
    {
        _tokenUserMap.TryGetValue(token, out var user);
        return Task.FromResult(user);
    }

    public Task<bool> ValidatePasswordAsync(User user, string password)
    {
        var userKey = $"user-{user.Id}";
        if (_userPasswordMap.TryGetValue(userKey, out var storedPassword))
        {
            return Task.FromResult(storedPassword == password);
        }
        return Task.FromResult(false);
    }

    public Task<string> HashPasswordAsync(string password)
    {
        // Simple mock hash (in real implementation, use BCrypt or similar)
        var hash = $"hashed-{password}-{password.Length}";
        return Task.FromResult(hash);
    }

    // Helper method for testing
    public void SetUserPassword(User user, string password)
    {
        var userKey = $"user-{user.Id}";
        _userPasswordMap[userKey] = password;
    }
}

/// <summary>
/// Unit tests for IAuthService interface
/// </summary>
public class IAuthServiceTests
{
    [Fact]
    public async Task GenerateJwtTokenAsync_WithValidUser_ShouldReturnToken()
    {
        // Arrange
        var user = new User 
        { 
            Id = 1, 
            Username = "testuser", 
            Email = "test@example.com",
            Role = UserRole.Manager,
            TenantId = "tenant-1"
        };
        var service = new MockAuthService();

        // Act
        var token = await service.GenerateJwtTokenAsync(user);

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);
        Assert.Contains("jwt-token", token);
        Assert.Contains(user.Id.ToString(), token);
    }

    [Fact]
    public async Task ValidateTokenAsync_WithValidToken_ShouldReturnUser()
    {
        // Arrange
        var user = new User 
        { 
            Id = 1, 
            Username = "testuser", 
            Role = UserRole.Employee,
            TenantId = "tenant-1"
        };
        var service = new MockAuthService();
        var token = await service.GenerateJwtTokenAsync(user);

        // Act
        var result = await service.ValidateTokenAsync(token);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Username, result.Username);
        Assert.Equal(user.Role, result.Role);
        Assert.Equal(user.TenantId, result.TenantId);
    }

    [Fact]
    public async Task ValidateTokenAsync_WithInvalidToken_ShouldReturnNull()
    {
        // Arrange
        var service = new MockAuthService();

        // Act
        var result = await service.ValidateTokenAsync("invalid-token");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ValidatePasswordAsync_WithCorrectPassword_ShouldReturnTrue()
    {
        // Arrange
        var user = new User { Id = 1, Username = "testuser", TenantId = "tenant-1" };
        var password = "correct-password";
        var service = new MockAuthService();
        service.SetUserPassword(user, password);

        // Act
        var result = await service.ValidatePasswordAsync(user, password);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidatePasswordAsync_WithIncorrectPassword_ShouldReturnFalse()
    {
        // Arrange
        var user = new User { Id = 1, Username = "testuser", TenantId = "tenant-1" };
        var correctPassword = "correct-password";
        var incorrectPassword = "wrong-password";
        var service = new MockAuthService();
        service.SetUserPassword(user, correctPassword);

        // Act
        var result = await service.ValidatePasswordAsync(user, incorrectPassword);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidatePasswordAsync_WithUnknownUser_ShouldReturnFalse()
    {
        // Arrange
        var user = new User { Id = 999, Username = "unknownuser", TenantId = "tenant-1" };
        var service = new MockAuthService();

        // Act
        var result = await service.ValidatePasswordAsync(user, "any-password");

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("password123")]
    [InlineData("")]
    [InlineData("very-long-password-with-special-characters!@#$%")]
    [InlineData("短い")]
    public async Task HashPasswordAsync_WithVariousPasswords_ShouldReturnHash(string password)
    {
        // Arrange
        var service = new MockAuthService();

        // Act
        var hash = await service.HashPasswordAsync(password);

        // Assert
        Assert.NotNull(hash);
        Assert.NotEmpty(hash);
        Assert.Contains("hashed", hash);
        Assert.Contains(password.Length.ToString(), hash);
    }

    [Fact]
    public async Task HashPasswordAsync_SamePassword_ShouldReturnSameHash()
    {
        // Arrange
        var password = "test-password";
        var service = new MockAuthService();

        // Act
        var hash1 = await service.HashPasswordAsync(password);
        var hash2 = await service.HashPasswordAsync(password);

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public async Task AuthService_FullWorkflow_ShouldWorkCorrectly()
    {
        // Arrange
        var user = new User 
        { 
            Id = 1, 
            Username = "testuser", 
            Email = "test@example.com",
            Role = UserRole.TenantAdmin,
            TenantId = "tenant-1"
        };
        var password = "secure-password";
        var service = new MockAuthService();

        // Act 1: Hash password
        var hashedPassword = await service.HashPasswordAsync(password);
        user.PasswordHash = hashedPassword;

        // Act 2: Set user password for validation
        service.SetUserPassword(user, password);

        // Act 3: Generate JWT token
        var token = await service.GenerateJwtTokenAsync(user);

        // Act 4: Validate token
        var tokenUser = await service.ValidateTokenAsync(token);

        // Act 5: Validate password
        var passwordValid = await service.ValidatePasswordAsync(user, password);

        // Assert
        Assert.NotNull(hashedPassword);
        Assert.NotNull(token);
        Assert.NotNull(tokenUser);
        Assert.True(passwordValid);
        Assert.Equal(user.Id, tokenUser.Id);
        Assert.Equal(user.Username, tokenUser.Username);
    }
}