using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the SaleOrder entity class.
/// Tests cover status changes, discount rules, and validation scenarios.
/// </summary>
public class SaleOrderTests
{
    /// <summary>
    /// Tests that when SetCancelStatus is called, the order cancellation status is updated accordingly.
    /// </summary>
    [Fact(DisplayName = "SetCancelStatus should set order status")]
    public void Given_Order_When_SetCancelStatusCalled_Then_StatusShouldBeUpdated()
    {
        // Arrange
        var order = SaleOrderTestData.GenerateValidSaleOrder();

        // Act
        order.SetCancelStatus(CancelStatus.Cancelled);

        // Assert
        Assert.Equal(CancelStatus.Cancelled, order.CancelStatus);
    }

    /// <summary>
    /// Tests that discount and total are calculated correctly for item quantities between 4 and 9.
    /// </summary>
    [Fact(DisplayName = "ApplyDiscountAndCalcTotal should apply 10 percent discount for amount between 4 and 9")]
    public void Given_ItemAmountBetween4And9_When_ApplyDiscountAndCalcTotal_Then_ShouldApply10PercentDiscount()
    {
        // Arrange
        var order = new SaleOrder
        {
            CustomerName = "Customer Test",
            BranchName = "Branch Test",
            Products =
            [
                new SaleOrderItem
                {
                    EanGtin = "7891234567895",
                    Description = "Product Test",
                    Price = 10m,
                    Amount = 4,
                    CancelStatus = CancelStatus.NotCancelled
                }
            ]
        };

        // Act
        order.ApplyDiscountAndCalcTotal();
        var item = Assert.Single(order.Products);

        // Assert
        Assert.Equal(10, item.Discount);
        Assert.Equal(36m, item.TotalValue);
        Assert.Equal(36m, order.TotalSale);
    }

    /// <summary>
    /// Tests that discount and total are calculated correctly for item quantities between 10 and 20.
    /// </summary>
    [Fact(DisplayName = "ApplyDiscountAndCalcTotal should apply 20 percent discount for amount between 10 and 20")]
    public void Given_ItemAmountBetween10And20_When_ApplyDiscountAndCalcTotal_Then_ShouldApply20PercentDiscount()
    {
        // Arrange
        var order = new SaleOrder
        {
            CustomerName = "Customer Test",
            BranchName = "Branch Test",
            Products =
            [
                new SaleOrderItem
                {
                    EanGtin = "7891234567895",
                    Description = "Product Test",
                    Price = 10m,
                    Amount = 10,
                    CancelStatus = CancelStatus.NotCancelled
                }
            ]
        };

        // Act
        order.ApplyDiscountAndCalcTotal();
        var item = Assert.Single(order.Products);

        // Assert
        Assert.Equal(20, item.Discount);
        Assert.Equal(80m, item.TotalValue);
        Assert.Equal(80m, order.TotalSale);
    }

    /// <summary>
    /// Tests that cancelled items are ignored when calculating discounts and order total.
    /// </summary>
    [Fact(DisplayName = "ApplyDiscountAndCalcTotal should skip cancelled items")]
    public void Given_CancelledItem_When_ApplyDiscountAndCalcTotal_Then_ShouldIgnoreCancelledItemInTotal()
    {
        // Arrange
        var order = new SaleOrder
        {
            CustomerName = "Customer Test",
            BranchName = "Branch Test",
            Products =
            [
                new SaleOrderItem
                {
                    EanGtin = "7891234567895",
                    Description = "Product A",
                    Price = 10m,
                    Amount = 5,
                    CancelStatus = CancelStatus.NotCancelled
                },
                new SaleOrderItem
                {
                    EanGtin = "7891234567895",
                    Description = "Product B",
                    Price = 10m,
                    Amount = 10,
                    CancelStatus = CancelStatus.Cancelled
                }
            ]
        };

        // Act
        order.ApplyDiscountAndCalcTotal();
        var items = order.Products.ToArray();

        // Assert
        Assert.Equal(45m, items[0].TotalValue);
        Assert.Equal(0m, items[1].TotalValue);
        Assert.Equal(45m, order.TotalSale);
    }

    /// <summary>
    /// Tests that validation passes when all sale order properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validate should pass for valid sale order")]
    public void Given_ValidSaleOrder_When_Validate_Then_ShouldReturnValid()
    {
        // Arrange
        var order = SaleOrderTestData.GenerateValidSaleOrder();

        // Act
        var result = order.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when sale order properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validate should fail for invalid sale order")]
    public void Given_InvalidSaleOrder_When_Validate_Then_ShouldReturnInvalid()
    {
        // Arrange
        var order = new SaleOrder
        {
            CustomerName = SaleOrderTestData.GenerateInvalidCustomerName(),
            BranchName = SaleOrderTestData.GenerateInvalidBranchName(),
            Products = []
        };

        // Act
        var result = order.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
