using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for EAN/GTIN validation logic in <see cref="SaleOrderItemValidator"/>.
/// </summary>
public class SaleOrderItemEanGtinValidationTests
{
    /// <summary>
    /// Tests that valid EAN/GTIN values are accepted by the validator.
    /// </summary>
    /// <param name="eanGtin">The valid EAN/GTIN value.</param>
    [Theory(DisplayName = "Valid EanGtin values should pass validation")]
    [InlineData("96385074")] // EAN-8
    [InlineData("012345678905")] // UPC-A (12)
    [InlineData("7891234567895")] // EAN-13
    [InlineData("01234567890128")] // GTIN-14
    public void Given_ValidEanGtin_When_Validated_Then_ShouldReturnTrue(string eanGtin)
    {
        // Act
        var result = SaleOrderItemValidator.BeValidGtinOrEan(eanGtin);

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Tests that invalid EAN/GTIN values are rejected by the validator.
    /// </summary>
    /// <param name="eanGtin">The invalid EAN/GTIN value.</param>
    [Theory(DisplayName = "Invalid EanGtin values should fail validation")]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("ABCDEFGH")]
    [InlineData("7891234567890")] // invalid check digit
    [InlineData("0123456789013")] // invalid check digit
    [InlineData("0123456789012A")]
    [InlineData("0123456789012345")] // invalid length
    public void Given_InvalidEanGtin_When_Validated_Then_ShouldReturnFalse(string eanGtin)
    {
        // Act
        var result = SaleOrderItemValidator.BeValidGtinOrEan(eanGtin);

        // Assert
        result.Should().BeFalse();
    }
}
