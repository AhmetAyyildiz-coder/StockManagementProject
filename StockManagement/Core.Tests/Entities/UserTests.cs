using Core.Entities;
using Core.Entities.Enums;
using Xunit;

namespace Core.Tests.Entities;

/// <summary>
/// Unit tests for User entity
/// </summary>
public class UserTests
{
    [Fact]
    public void User_DefaultValues_ShouldBeSetCorrectly()
    {
        // Arrange & Act
        var user = new User();

        // Assert
        Assert.Equal(0, user.Id);
        Assert.Equal(string.Empty, user.Email);
        Assert.Equal(string.Empty, user.PasswordHash);
        Assert.Equal(string.Empty, user.FirstName);
        Assert.Equal(string.Empty, user.LastName);
        Assert.Equal(UserRole.TenantAdmin, user.Role);
        Assert.Null(user.LastLoginAt);
        // Inherited from TenantEntity
        Assert.Equal(string.Empty, user.TenantId);
        Assert.True(user.IsActive);
        Assert.True(user.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void User_FullName_ShouldCombineFirstAndLastName()
    {
        // Arrange
        var user = new User
        {
            FirstName = "John",
            LastName = "Doe"
        };

        // Act & Assert
        Assert.Equal("John Doe", user.FullName);
    }

    [Theory]
    [InlineData(UserRole.TenantAdmin, true)]
    [InlineData(UserRole.Manager, true)]
    [InlineData(UserRole.Employee, false)]
    public void User_CanManageMovementTypes_ShouldReturnCorrectValue(UserRole role, bool expected)
    {
        // Arrange
        var user = new User { Role = role };

        // Act
        var result = user.CanManageMovementTypes();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void User_SetProperties_ShouldRetainValues()
    {
        // Arrange
        var user = new User();
        var testEmail = "test@example.com";
        var testHash = "hashedpassword";
        var testFirstName = "Jane";
        var testLastName = "Smith";
        var testLoginTime = DateTime.UtcNow;

        // Act
        user.Id = 123;
        user.Email = testEmail;
        user.PasswordHash = testHash;
        user.FirstName = testFirstName;
        user.LastName = testLastName;
        user.Role = UserRole.Manager;
        user.LastLoginAt = testLoginTime;

        // Assert
        Assert.Equal(123, user.Id);
        Assert.Equal(testEmail, user.Email);
        Assert.Equal(testHash, user.PasswordHash);
        Assert.Equal(testFirstName, user.FirstName);
        Assert.Equal(testLastName, user.LastName);
        Assert.Equal(UserRole.Manager, user.Role);
        Assert.Equal(testLoginTime, user.LastLoginAt);
        Assert.Equal("Jane Smith", user.FullName);
    }

    [Theory]
    [InlineData("", "", " ")]
    [InlineData("John", "", "John ")]
    [InlineData("", "Doe", " Doe")]
    [InlineData("John", "Doe", "John Doe")]
    public void User_FullName_ShouldHandleEmptyNames(string firstName, string lastName, string expected)
    {
        // Arrange
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName
        };

        // Act & Assert
        Assert.Equal(expected, user.FullName);
    }
}