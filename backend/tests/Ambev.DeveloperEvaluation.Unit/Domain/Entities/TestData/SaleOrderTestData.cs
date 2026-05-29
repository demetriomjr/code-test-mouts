using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating SaleOrder test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleOrderTestData
{
    /// <summary>
    /// Configures the Faker to generate valid SaleOrderItem entities.
    /// The generated items will have valid:
    /// - CancelStatus
    /// - EanGtin
    /// - Description
    /// - Price
    /// - Amount
    /// </summary>
    private static readonly Faker<SaleOrderItem> SaleOrderItemFaker = new Faker<SaleOrderItem>()
        .RuleFor(i => i.CancelStatus, f => f.PickRandom(CancelStatus.NotCancelled, CancelStatus.Cancelled))
        .RuleFor(i => i.EanGtin, f => f.Commerce.Ean13())
        .RuleFor(i => i.Description, f => f.Commerce.ProductName())
        .RuleFor(i => i.Price, f => f.Random.Decimal(1, 1000))
        .RuleFor(i => i.Amount, f => f.Random.Int(1, 20));

    /// <summary>
    /// Configures the Faker to generate valid SaleOrder entities.
    /// The generated orders will have valid:
    /// - CustomerName
    /// - BranchName
    /// - CancelStatus
    /// - Products collection
    /// </summary>
    private static readonly Faker<SaleOrder> SaleOrderFaker = new Faker<SaleOrder>()
        .RuleFor(o => o.CustomerName, f => f.Person.FullName)
        .RuleFor(o => o.BranchName, f => $"{f.Company.CompanyName()} Branch")
        .RuleFor(o => o.CancelStatus, f => f.PickRandom(CancelStatus.NotCancelled, CancelStatus.Cancelled))
        .RuleFor(o => o.Products, f => SaleOrderItemFaker.Generate(f.Random.Int(1, 5)));

    /// <summary>
    /// Generates a valid SaleOrder entity with randomized data.
    /// The generated order will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid SaleOrder entity with randomly generated data.</returns>
    public static SaleOrder GenerateValidSaleOrder()
    {
        var order = SaleOrderFaker.Generate();

        foreach (var item in order.Products)
            item.SaleOrderId = order.Id;

        order.ApplyDiscountAndCalcTotal();
        return order;
    }

    /// <summary>
    /// Generates a collection of valid SaleOrderItem entities.
    /// </summary>
    /// <param name="count">The number of items to generate.</param>
    /// <returns>A collection of valid SaleOrderItem entities.</returns>
    public static IEnumerable<SaleOrderItem> GenerateValidItems(int count = 3)
    {
        return SaleOrderItemFaker.Generate(count);
    }

    /// <summary>
    /// Generates an invalid customer name for negative test scenarios.
    /// </summary>
    /// <returns>An invalid customer name.</returns>
    public static string GenerateInvalidCustomerName()
    {
        return string.Empty;
    }

    /// <summary>
    /// Generates an invalid branch name for negative test scenarios.
    /// </summary>
    /// <returns>An invalid branch name.</returns>
    public static string GenerateInvalidBranchName()
    {
        return string.Empty;
    }

    /// <summary>
    /// Generates an invalid SaleOrderItem with amount above the allowed maximum.
    /// This is useful for testing amount validation error scenarios.
    /// </summary>
    /// <returns>An invalid SaleOrderItem with amount greater than 20.</returns>
    public static SaleOrderItem GenerateInvalidItemWithTooManyUnits()
    {
        return new Faker<SaleOrderItem>()
            .RuleFor(i => i.CancelStatus, _ => CancelStatus.NotCancelled)
            .RuleFor(i => i.EanGtin, f => f.Commerce.Ean13())
            .RuleFor(i => i.Description, f => f.Commerce.ProductName())
            .RuleFor(i => i.Price, f => f.Random.Decimal(1, 1000))
            .RuleFor(i => i.Amount, _ => 21)
            .Generate();
    }

    /// <summary>
    /// Generates an invalid SaleOrderItem with zero price.
    /// This is useful for testing price validation error scenarios.
    /// </summary>
    /// <returns>An invalid SaleOrderItem with zero price.</returns>
    public static SaleOrderItem GenerateInvalidItemWithZeroPrice()
    {
        return new Faker<SaleOrderItem>()
            .RuleFor(i => i.CancelStatus, _ => CancelStatus.NotCancelled)
            .RuleFor(i => i.EanGtin, f => f.Commerce.Ean13())
            .RuleFor(i => i.Description, f => f.Commerce.ProductName())
            .RuleFor(i => i.Price, _ => 0m)
            .RuleFor(i => i.Amount, f => f.Random.Int(1, 20))
            .Generate();
    }
}
