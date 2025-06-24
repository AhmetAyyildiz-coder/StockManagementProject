using Core.Constants;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Core.Helpers;

/// <summary>
/// Provides common validation methods for business rules and data integrity 
/// in the Stock Management system.
/// </summary>
public static class ValidationHelper
{
    /// <summary>
    /// Validates if an email address is in the correct format.
    /// </summary>
    /// <param name="email">The email address to validate.</param>
    /// <returns>True if the email is valid, false otherwise.</returns>
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
            
        try
        {
            var emailAddress = new MailAddress(email);
            return emailAddress.Address == email;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Validates if a subdomain follows the required naming conventions.
    /// Subdomains must be lowercase alphanumeric with hyphens allowed, 3-63 characters long.
    /// </summary>
    /// <param name="subdomain">The subdomain to validate.</param>
    /// <returns>True if the subdomain is valid, false otherwise.</returns>
    public static bool IsValidSubdomain(string subdomain)
    {
        if (string.IsNullOrWhiteSpace(subdomain))
            return false;
        
        // Must be 3-63 characters
        if (subdomain.Length < 3 || subdomain.Length > 63)
            return false;
            
        // Must be lowercase alphanumeric with hyphens allowed
        // Cannot start or end with hyphen
        var regex = new Regex("^[a-z0-9][a-z0-9-]*[a-z0-9]$");
        return regex.IsMatch(subdomain);
    }
    
    /// <summary>
    /// Validates if a product SKU meets the system requirements.
    /// </summary>
    /// <param name="sku">The SKU to validate.</param>
    /// <returns>True if the SKU is valid, false otherwise.</returns>
    public static bool IsValidSKU(string sku)
    {
        return !string.IsNullOrWhiteSpace(sku) && 
               sku.Length <= SystemDefaults.SKU_MAX_LENGTH;
    }
    
    /// <summary>
    /// Validates if a product name meets the system requirements.
    /// </summary>
    /// <param name="productName">The product name to validate.</param>
    /// <returns>True if the product name is valid, false otherwise.</returns>
    public static bool IsValidProductName(string productName)
    {
        return !string.IsNullOrWhiteSpace(productName) && 
               productName.Length <= SystemDefaults.PRODUCT_NAME_MAX_LENGTH;
    }
    
    /// <summary>
    /// Validates if a barcode meets the system requirements.
    /// </summary>
    /// <param name="barcode">The barcode to validate.</param>
    /// <returns>True if the barcode is valid, false otherwise.</returns>
    public static bool IsValidBarcode(string barcode)
    {
        return string.IsNullOrWhiteSpace(barcode) || 
               barcode.Length <= SystemDefaults.BARCODE_MAX_LENGTH;
    }
    
    /// <summary>
    /// Validates if a password meets the system security requirements.
    /// </summary>
    /// <param name="password">The password to validate.</param>
    /// <returns>True if the password is valid, false otherwise.</returns>
    public static bool IsValidPassword(string password)
    {
        return !string.IsNullOrWhiteSpace(password) &&
               password.Length >= SystemDefaults.PASSWORD_MIN_LENGTH &&
               password.Length <= SystemDefaults.PASSWORD_MAX_LENGTH;
    }
}