using Core.Helpers;
using Xunit;

namespace Core.Tests.Helpers;

/// <summary>
/// Unit tests for ValidationHelper class
/// </summary>
public class ValidationHelperTests
{
    [Theory]
    [InlineData("test@example.com", true)]
    [InlineData("user.name@domain.co.uk", true)]
    [InlineData("valid+email@test.org", true)]
    [InlineData("", false)]
    [InlineData("invalid-email", false)]
    [InlineData("@domain.com", false)]
    [InlineData("user@", false)]
    [InlineData(null, false)]
    public void IsValidEmail_ShouldValidateEmailAddresses(string email, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidEmail(email);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("test", true)]
    [InlineData("my-shop", true)]
    [InlineData("shop123", true)]
    [InlineData("a1b2c3", true)]
    [InlineData("", false)]
    [InlineData("ab", false)] // Too short
    [InlineData("Test", false)] // Contains uppercase
    [InlineData("test_shop", false)] // Contains underscore
    [InlineData("-test", false)] // Starts with hyphen
    [InlineData("test-", false)] // Ends with hyphen
    [InlineData(null, false)]
    public void IsValidSubdomain_ShouldValidateSubdomainFormat(string subdomain, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidSubdomain(subdomain);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("PROD-001", true)]
    [InlineData("SKU123", true)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValidSKU_ShouldValidateSKUFormat(string sku, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidSKU(sku);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("Valid Product Name", true)]
    [InlineData("Product", true)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValidProductName_ShouldValidateProductNameFormat(string productName, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidProductName(productName);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("123456", true)] // Valid password at minimum length
    [InlineData("ValidPassword123", true)] // Valid longer password
    [InlineData("12345", false)] // Too short
    [InlineData("", false)] // Empty
    [InlineData(null, false)] // Null
    public void IsValidPassword_ShouldValidatePasswordRequirements(string password, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidPassword(password);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("1234567890", true)] // Valid barcode
    [InlineData("", true)] // Empty barcode is allowed
    [InlineData(null, true)] // Null barcode is allowed
    public void IsValidBarcode_ShouldValidateBarcodeFormat(string barcode, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidBarcode(barcode);
        
        // Assert
        Assert.Equal(expected, result);
    }
}