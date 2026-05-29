using Ambev.DeveloperEvaluation.Application.SaleOrders.UpdateSaleOrder;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;
using Bogus.Extensions;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides test data builders for <see cref="UpdateSaleOrderCommand"/>.
/// </summary>
public static class UpdateSaleOrderHandlerTestData
{
    private static readonly Faker<UpdateSaleOrderItemCommand> UpdateSaleOrderItemFaker = new Faker<UpdateSaleOrderItemCommand>()
        .RuleFor(x => x.EanGtin, 
            f => f.PickRandom("7891234567895", "96385074", "012345678905"))
        .RuleFor(x => x.Description, 
            f => f.Commerce.ProductName().ClampLength(3, 50))
        .RuleFor(x => x.Price, 
            f => Math.Round(f.Random.Decimal(1, 999), 2))
        .RuleFor(x => x.Amount, 
            f => f.Random.Int(1, 19))
        .RuleFor(x => x.CancelStatus, 
            f => f.PickRandom(CancelStatus.NotCancelled, CancelStatus.Cancelled));

    private static readonly Faker<UpdateSaleOrderCommand> UpdateSaleOrderCommandFaker = new Faker<UpdateSaleOrderCommand>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.CustomerName, 
            f => f.Person.FullName.ClampLength(3, 50))
        .RuleFor(x => x.BranchName, 
            f => f.Company.CompanyName().ClampLength(3, 50))
        .RuleFor(x => x.Products, 
            f => UpdateSaleOrderItemFaker.Generate(f.Random.Int(1, 5)))
        .RuleFor(x => x.CancelStatus, 
            f => f.PickRandom(CancelStatus.NotCancelled, CancelStatus.Cancelled));

    /// <summary>
    /// Generates a valid update sale order command.
    /// </summary>
    /// <returns>A valid <see cref="UpdateSaleOrderCommand"/>.</returns>
    public static UpdateSaleOrderCommand GenerateValidCommand()
    {
        return UpdateSaleOrderCommandFaker.Generate();
    }

    /// <summary>
    /// Generates an invalid update sale order command.
    /// </summary>
    /// <returns>An invalid <see cref="UpdateSaleOrderCommand"/>.</returns>
    public static UpdateSaleOrderCommand GenerateInvalidCommand()
    {
        return new UpdateSaleOrderCommand();
    }
}
