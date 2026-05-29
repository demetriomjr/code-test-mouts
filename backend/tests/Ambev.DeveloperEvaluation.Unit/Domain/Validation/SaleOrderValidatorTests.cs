using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class SaleOrderValidatorTests
{
    private readonly SaleOrderValidator _validator;

    public SaleOrderValidatorTests()
    {
        _validator = new SaleOrderValidator();
    }

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
