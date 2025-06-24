using Core.Enums;
using Xunit;

namespace Core.Tests.Entities.Enums;

/// <summary>
/// Unit tests for SystemMovementTypes enum
/// </summary>
public class SystemMovementTypesTests
{
    [Fact]
    public void SystemMovementTypes_AllValues_ShouldBeDefined()
    {
        // Act & Assert - Ensure all expected values are defined
        Assert.True(Enum.IsDefined(typeof(SystemMovementTypes), SystemMovementTypes.Purchase));
        Assert.True(Enum.IsDefined(typeof(SystemMovementTypes), SystemMovementTypes.Sale));
        Assert.True(Enum.IsDefined(typeof(SystemMovementTypes), SystemMovementTypes.Loss));
        Assert.True(Enum.IsDefined(typeof(SystemMovementTypes), SystemMovementTypes.Found));
        Assert.True(Enum.IsDefined(typeof(SystemMovementTypes), SystemMovementTypes.Return));
        Assert.True(Enum.IsDefined(typeof(SystemMovementTypes), SystemMovementTypes.Damage));
    }

    [Theory]
    [InlineData(SystemMovementTypes.Purchase, 1)]
    [InlineData(SystemMovementTypes.Sale, 2)]
    [InlineData(SystemMovementTypes.Loss, 3)]
    [InlineData(SystemMovementTypes.Found, 4)]
    [InlineData(SystemMovementTypes.Return, 5)]
    [InlineData(SystemMovementTypes.Damage, 6)]
    public void SystemMovementTypes_Values_ShouldHaveCorrectIntegerValues(SystemMovementTypes type, int expectedValue)
    {
        // Act
        var actualValue = (int)type;

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void SystemMovementTypes_Count_ShouldMatchExpectedNumber()
    {
        // Arrange
        var expectedCount = 6;

        // Act
        var actualCount = Enum.GetValues(typeof(SystemMovementTypes)).Length;

        // Assert
        Assert.Equal(expectedCount, actualCount);
    }

    [Fact]
    public void SystemMovementTypes_Names_ShouldMatchExpectedValues()
    {
        // Act
        var names = Enum.GetNames(typeof(SystemMovementTypes));

        // Assert
        Assert.Contains("Purchase", names);
        Assert.Contains("Sale", names);
        Assert.Contains("Loss", names);
        Assert.Contains("Found", names);
        Assert.Contains("Return", names);
        Assert.Contains("Damage", names);
    }

    [Theory]
    [InlineData("Purchase", SystemMovementTypes.Purchase)]
    [InlineData("Sale", SystemMovementTypes.Sale)]
    [InlineData("Loss", SystemMovementTypes.Loss)]
    [InlineData("Found", SystemMovementTypes.Found)]
    [InlineData("Return", SystemMovementTypes.Return)]
    [InlineData("Damage", SystemMovementTypes.Damage)]
    public void SystemMovementTypes_Parse_ShouldWorkCorrectly(string name, SystemMovementTypes expectedValue)
    {
        // Act
        var parsed = Enum.Parse<SystemMovementTypes>(name);

        // Assert
        Assert.Equal(expectedValue, parsed);
    }

    [Fact]
    public void SystemMovementTypes_ToString_ShouldReturnCorrectNames()
    {
        // Act & Assert
        Assert.Equal("Purchase", SystemMovementTypes.Purchase.ToString());
        Assert.Equal("Sale", SystemMovementTypes.Sale.ToString());
        Assert.Equal("Loss", SystemMovementTypes.Loss.ToString());
        Assert.Equal("Found", SystemMovementTypes.Found.ToString());
        Assert.Equal("Return", SystemMovementTypes.Return.ToString());
        Assert.Equal("Damage", SystemMovementTypes.Damage.ToString());
    }
}