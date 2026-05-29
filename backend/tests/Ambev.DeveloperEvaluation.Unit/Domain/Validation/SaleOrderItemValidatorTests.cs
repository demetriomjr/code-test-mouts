using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the <see cref="SaleOrderItemValidator"/> class.
/// Tests cover validation rules for sale order item identifiers, values, and quantity constraints.
/// </summary>
public class SaleOrderItemValidatorTests
{
    private readonly SaleOrderItemValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleOrderItemValidatorTests"/> class.
    /// </summary>
    public SaleOrderItemValidatorTests()
    {
        _validator = new SaleOrderItemValidator();
    }

    /// <summary>
    /// Tests that a valid sale order item passes all validation rules.
    /// </summary>
    [Fact(DisplayName = "Valid sale order item should pass validation")]
    public void Given_ValidSaleOrderItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var item = GenerateValidItem();

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that an empty sale order identifier fails validation.
    /// </summary>
    [Fact(DisplayName = "Empty sale order id should fail validation")]
    public void Given_EmptySaleOrderId_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var item = GenerateValidItem();
        item.SaleOrderId = Guid.Empty;

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SaleOrderId);
    }

    /// <summary>
    /// Tests that invalid price values fail validation.
    /// </summary>
    /// <param name="price">The invalid price value.</param>
    [Theory(DisplayName = "Invalid price should fail validation")]
    [InlineData(0)]
    [InlineData(-1)]
    public void Given_InvalidPrice_When_Validated_Then_ShouldHaveError(decimal price)
    {
        // Arrange
        var item = GenerateValidItem();
        item.Price = price;

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    /// <summary>
    /// Tests that invalid description lengths fail validation.
    /// </summary>
    /// <param name="description">The invalid description value.</param>
    [Theory(DisplayName = "Invalid description length should fail validation")]
    [InlineData("ab")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
    public void Given_InvalidDescription_When_Validated_Then_ShouldHaveError(string description)
    {
        // Arrange
        var item = GenerateValidItem();
        item.Description = description;

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    /// <summary>
    /// Tests that invalid amount values fail validation.
    /// </summary>
    /// <param name="amount">The invalid amount value.</param>
    [Theory(DisplayName = "Invalid amount should fail validation")]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(20)]
    [InlineData(21)]
    public void Given_InvalidAmount_When_Validated_Then_ShouldHaveError(int amount)
    {
        // Arrange
        var item = GenerateValidItem();
        item.Amount = amount;

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Amount);
    }

    /// <summary>
    /// Tests that invalid EAN/GTIN values fail validation.
    /// </summary>
    /// <param name="eanGtin">The invalid EAN/GTIN value.</param>
    [Theory(DisplayName = "Invalid EanGtin should fail validation")]
    [InlineData("123")]
    [InlineData("abc1234567890")]
    [InlineData("7891234567890")]
    public void Given_InvalidEanGtin_When_Validated_Then_ShouldHaveError(string eanGtin)
    {
        // Arrange
        var item = GenerateValidItem();
        item.EanGtin = eanGtin;

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.EanGtin);
    }

    /// <summary>
    /// Generates a valid sale order item for test setup.
    /// </summary>
    /// <returns>A valid <see cref="SaleOrderItem"/> instance.</returns>
    private static SaleOrderItem GenerateValidItem()
    {
        return new SaleOrderItem
        {
            SaleOrderId = Guid.NewGuid(),
            EanGtin = "7891234567895",
            Description = "Valid Item",
            Price = 10.50m,
            Amount = 1
        };
    }
}
