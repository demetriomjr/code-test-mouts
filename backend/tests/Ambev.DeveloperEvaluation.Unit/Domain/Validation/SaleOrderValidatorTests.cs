using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the <see cref="SaleOrderValidator"/> class.
/// Tests cover validation rules for customer name, branch name, and product list requirements.
/// </summary>
public class SaleOrderValidatorTests
{
    private readonly SaleOrderValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleOrderValidatorTests"/> class.
    /// </summary>
    public SaleOrderValidatorTests()
    {
        _validator = new SaleOrderValidator();
    }

    /// <summary>
    /// Tests that a valid sale order passes all validation rules.
    /// </summary>
    [Fact(DisplayName = "Valid sale order should pass validation")]
    public void Given_ValidSaleOrder_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var order = SaleOrderTestData.GenerateValidSaleOrder();

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that invalid customer name values fail validation.
    /// </summary>
    /// <param name="customerName">The invalid customer name value.</param>
    [Theory(DisplayName = "Invalid customer name should fail validation")]
    [InlineData("")]
    [InlineData("ab")]
    public void Given_InvalidCustomerName_When_Validated_Then_ShouldHaveError(string customerName)
    {
        // Arrange
        var order = SaleOrderTestData.GenerateValidSaleOrder();
        order.CustomerName = customerName;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerName);
    }

    /// <summary>
    /// Tests that a customer name longer than the allowed maximum fails validation.
    /// </summary>
    [Fact(DisplayName = "Customer name longer than maximum should fail validation")]
    public void Given_CustomerNameLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = SaleOrderTestData.GenerateValidSaleOrder();
        order.CustomerName = new string('a', 51);

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerName);
    }

    /// <summary>
    /// Tests that invalid branch name values fail validation.
    /// </summary>
    /// <param name="branchName">The invalid branch name value.</param>
    [Theory(DisplayName = "Invalid branch name should fail validation")]
    [InlineData("")]
    [InlineData("ab")]
    public void Given_InvalidBranchName_When_Validated_Then_ShouldHaveError(string branchName)
    {
        // Arrange
        var order = SaleOrderTestData.GenerateValidSaleOrder();
        order.BranchName = branchName;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BranchName);
    }

    /// <summary>
    /// Tests that a branch name longer than the allowed maximum fails validation.
    /// </summary>
    [Fact(DisplayName = "Branch name longer than maximum should fail validation")]
    public void Given_BranchNameLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = SaleOrderTestData.GenerateValidSaleOrder();
        order.BranchName = new string('a', 51);

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BranchName);
    }

    /// <summary>
    /// Tests that an empty product list fails validation.
    /// </summary>
    [Fact(DisplayName = "Empty products list should fail validation")]
    public void Given_EmptyProducts_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = SaleOrderTestData.GenerateValidSaleOrder();
        order.Products = [];

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Products);
    }

    /// <summary>
    /// Tests that a null product list fails validation.
    /// </summary>
    [Fact(DisplayName = "Null products list should fail validation")]
    public void Given_NullProducts_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = SaleOrderTestData.GenerateValidSaleOrder();
        order.Products = null!;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Products);
    }
}
