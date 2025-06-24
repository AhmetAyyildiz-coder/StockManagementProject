using Core.Exceptions;
using Xunit;

namespace Core.Tests.Exceptions;

/// <summary>
/// Unit tests for NotFoundException class
/// </summary>
public class NotFoundExceptionTests
{
    [Fact]
    public void NotFoundException_WithEntityNameAndKey_ShouldFormatMessage()
    {
        // Arrange
        var entityName = "Product";
        var key = 123;
        
        // Act
        var exception = new NotFoundException(entityName, key);
        
        // Assert
        Assert.Equal("Product with key '123' was not found.", exception.Message);
        Assert.Equal("NOT_FOUND", exception.ErrorCode);
    }
    
    [Fact]
    public void NotFoundException_WithCustomMessage_ShouldUseMessage()
    {
        // Arrange
        var message = "Custom not found message";
        
        // Act
        var exception = new NotFoundException(message);
        
        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal("NOT_FOUND", exception.ErrorCode);
    }
}

/// <summary>
/// Unit tests for ValidationException class
/// </summary>
public class ValidationExceptionTests
{
    [Fact]
    public void ValidationException_WithSingleProperty_ShouldCreateErrorDictionary()
    {
        // Arrange
        var propertyName = "Email";
        var errorMessage = "Invalid email format";
        
        // Act
        var exception = new ValidationException(propertyName, errorMessage);
        
        // Assert
        Assert.Equal($"Validation failed for {propertyName}: {errorMessage}", exception.Message);
        Assert.Equal("VALIDATION_ERROR", exception.ErrorCode);
        Assert.True(exception.Errors.ContainsKey(propertyName));
        Assert.Equal(errorMessage, exception.Errors[propertyName][0]);
    }
    
    [Fact]
    public void ValidationException_WithMultipleErrors_ShouldStoreAllErrors()
    {
        // Arrange
        var message = "Multiple validation errors";
        var errors = new Dictionary<string, string[]>
        {
            { "Email", new[] { "Invalid format" } },
            { "Password", new[] { "Too short", "Missing special character" } }
        };
        
        // Act
        var exception = new ValidationException(message, errors);
        
        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal("VALIDATION_ERROR", exception.ErrorCode);
        Assert.Equal(errors, exception.Errors);
    }
}

/// <summary>
/// Unit tests for enhanced DomainException base class
/// </summary>
public class DomainExceptionEnhancedTests
{
    private class TestDomainException : DomainException
    {
        public TestDomainException() : base() { }
        public TestDomainException(string message) : base(message) { }
        public TestDomainException(string message, string errorCode) : base(message, errorCode) { }
        public TestDomainException(string message, Exception innerException) : base(message, innerException) { }
    }

    [Fact]
    public void DomainException_DefaultConstructor_ShouldSetErrorCodeToTypeName()
    {
        // Act
        var exception = new TestDomainException();
        
        // Assert
        Assert.Equal("TestDomainException", exception.ErrorCode);
    }
    
    [Fact]
    public void DomainException_WithMessage_ShouldSetErrorCodeToTypeName()
    {
        // Arrange
        var message = "Test message";
        
        // Act
        var exception = new TestDomainException(message);
        
        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal("TestDomainException", exception.ErrorCode);
    }
    
    [Fact]
    public void DomainException_WithCustomErrorCode_ShouldUseProvidedErrorCode()
    {
        // Arrange
        var message = "Test message";
        var errorCode = "CUSTOM_ERROR";
        
        // Act
        var exception = new TestDomainException(message, errorCode);
        
        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal(errorCode, exception.ErrorCode);
    }
}