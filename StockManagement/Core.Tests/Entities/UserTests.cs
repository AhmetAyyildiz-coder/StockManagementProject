using Core.Entities;
using Core.Enums;
using Xunit;

namespace Core.Tests.Entities;

/// <summary>
/// Unit tests for User entity
/// </summary>
public class UserTests
{
    [Theory]
    [InlineData(UserRole.SystemAdmin, true)]
    [InlineData(UserRole.TenantAdmin, true)]
    [InlineData(UserRole.Manager, true)]
    [InlineData(UserRole.Employee, false)]
    [InlineData(UserRole.ReadOnly, false)]
    public void CanManageMovementTypes_ShouldReturnCorrectValue(UserRole userRole, bool expectedResult)
    {
        // Arrange
        var user = new User { Role = userRole };

        // Act
        var result = user.CanManageMovementTypes();

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void User_DefaultValues_ShouldBeSetCorrectly()
    {
        // Act
        var user = new User();

        // Assert
        Assert.Equal(string.Empty, user.Username);
        Assert.Equal(string.Empty, user.Email);
        Assert.Equal(string.Empty, user.PasswordHash);
        Assert.Equal(UserRole.ReadOnly, user.Role);
        Assert.Equal(0, user.Id);
    }

    [Fact]
    public void User_InheritsFromTenantEntity_ShouldHaveBaseProperties()
    {
        // Arrange
        var user = new User
        {
            TenantId = "tenant-123",
            IsActive = false
        };

        // Act & Assert
        Assert.Equal("tenant-123", user.TenantId);
        Assert.False(user.IsActive);
        Assert.True(user.CreatedAt <= DateTime.UtcNow);
        Assert.Null(user.UpdatedAt);
    }

    [Theory]
    [InlineData("user123", "user@example.com", "hashedPassword123")]
    [InlineData("", "", "")]
    [InlineData("admin", "admin@company.com", "secureHash")]
    public void User_Properties_ShouldBeSettableAndGettable(string username, string email, string passwordHash)
    {
        // Arrange & Act
        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash
        };

        // Assert
        Assert.Equal(username, user.Username);
        Assert.Equal(email, user.Email);
        Assert.Equal(passwordHash, user.PasswordHash);
    }
}
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
>>>>>>> main

        // Act
        var result = user.CanManageMovementTypes();

        // Assert
<<<<<<< HEAD
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void User_DefaultValues_ShouldBeSetCorrectly()
    {
        // Act
        var user = new User();

        // Assert
        Assert.Equal(string.Empty, user.Username);
        Assert.Equal(string.Empty, user.Email);
        Assert.Equal(string.Empty, user.PasswordHash);
        Assert.Equal(UserRole.ReadOnly, user.Role);
        Assert.Equal(0, user.Id);
    }

    [Fact]
    public void User_InheritsFromTenantEntity_ShouldHaveBaseProperties()
=======
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
>>>>>>> main
    {
        // Arrange
        var user = new User
        {
<<<<<<< HEAD
            TenantId = "tenant-123",
            IsActive = false
        };

        // Act & Assert
        Assert.Equal("tenant-123", user.TenantId);
        Assert.False(user.IsActive);
        Assert.True(user.CreatedAt <= DateTime.UtcNow);
        Assert.Null(user.UpdatedAt);
    }

    [Theory]
    [InlineData("user123", "user@example.com", "hashedPassword123")]
    [InlineData("", "", "")]
    [InlineData("admin", "admin@company.com", "secureHash")]
    public void User_Properties_ShouldBeSettableAndGettable(string username, string email, string passwordHash)
    {
        // Arrange & Act
        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash
        };

        // Assert
        Assert.Equal(username, user.Username);
        Assert.Equal(email, user.Email);
        Assert.Equal(passwordHash, user.PasswordHash);
=======
            FirstName = firstName,
            LastName = lastName
        };

        // Act & Assert
        Assert.Equal(expected, user.FullName);
>>>>>>> main
    }
}