using Core.Exceptions;
using Xunit;

namespace Core.Tests.Exceptions;

/// <summary>
/// Unit tests for UnauthorizedException
/// </summary>
public class UnauthorizedExceptionTests
{
    [Fact]
    public void UnauthorizedException_WithMessage_ShouldSetMessage()
    {
        // Arrange
        const string expectedMessage = "Test unauthorized message";

        // Act
        var exception = new UnauthorizedException(expectedMessage);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
        Assert.IsAssignableFrom<DomainException>(exception);
    }

    [Fact]
    public void UnauthorizedException_WithMessageAndInnerException_ShouldSetBoth()
    {
        // Arrange
        const string expectedMessage = "Test unauthorized message";
        var innerException = new InvalidOperationException("Inner exception");

        // Act
        var exception = new UnauthorizedException(expectedMessage, innerException);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
        Assert.Equal(innerException, exception.InnerException);
        Assert.IsAssignableFrom<DomainException>(exception);
    }
}

/// <summary>
/// Unit tests for TenantNotFoundException
/// </summary>
public class TenantNotFoundExceptionTests
{
    [Fact]
    public void TenantNotFoundException_WithTenantId_ShouldSetMessageAndProperty()
    {
        // Arrange
        const string tenantId = "test-tenant-123";
        const string expectedMessage = "Tenant 'test-tenant-123' bulunamadı.";

        // Act
        var exception = new TenantNotFoundException(tenantId);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
        Assert.Equal(tenantId, exception.TenantId);
        Assert.IsAssignableFrom<DomainException>(exception);
    }

    [Theory]
    [InlineData("")]
    [InlineData("tenant-1")]
    [InlineData("TENANT_2")]
    [InlineData("multi-word-tenant")]
    public void TenantNotFoundException_WithVariousTenantIds_ShouldHandleCorrectly(string tenantId)
    {
        // Act
        var exception = new TenantNotFoundException(tenantId);

        // Assert
        Assert.Equal(tenantId, exception.TenantId);
        Assert.Contains(tenantId, exception.Message);
        Assert.Contains("bulunamadı", exception.Message);
    }
}

/// <summary>
/// Unit tests for DomainException base class
/// </summary>
public class DomainExceptionTests
{
    private class TestDomainException : DomainException
    {
        public TestDomainException() : base() { }
        public TestDomainException(string message) : base(message) { }
        public TestDomainException(string message, Exception innerException) : base(message, innerException) { }
    }

    [Fact]
    public void DomainException_DefaultConstructor_ShouldCreateInstance()
    {
        // Act
        var exception = new TestDomainException();

        // Assert
        Assert.NotNull(exception);
        Assert.IsAssignableFrom<Exception>(exception);
    }

    [Fact]
    public void DomainException_WithMessage_ShouldSetMessage()
    {
        // Arrange
        const string expectedMessage = "Test domain exception";

        // Act
        var exception = new TestDomainException(expectedMessage);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Fact]
    public void DomainException_WithMessageAndInnerException_ShouldSetBoth()
    {
        // Arrange
        const string expectedMessage = "Test domain exception";
        var innerException = new ArgumentException("Inner exception");

        // Act
        var exception = new TestDomainException(expectedMessage, innerException);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }
}