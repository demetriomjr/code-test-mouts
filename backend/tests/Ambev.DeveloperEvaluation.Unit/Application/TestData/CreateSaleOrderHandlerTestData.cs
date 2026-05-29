using Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;
using Bogus;
using Bogus.Extensions;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleOrderHandlerTestData
{
    private static readonly Faker<CreateSaleOrderItemCommand> CreateSaleOrderItemFaker = new Faker<CreateSaleOrderItemCommand>()
        .RuleFor(x => x.EanGtin, 
            f => f.PickRandom("7891234567895", "96385074", "012345678905"))
        .RuleFor(x => x.Description, 
            f => f.Commerce.ProductName().ClampLength(3, 50))
        .RuleFor(x => x.Price, 
            f => Math.Round(f.Random.Decimal(1, 999), 2))
        .RuleFor(x => x.Amount, 
            f => f.Random.Int(1, 19));

    private static readonly Faker<CreateSaleOrderCommand> CreateSaleOrderCommandFaker = new Faker<CreateSaleOrderCommand>()
        .RuleFor(x => x.CustomerName, 
            f => f.Person.FullName.ClampLength(3, 50))
        .RuleFor(x => x.BranchName, 
            f => f.Company.CompanyName().ClampLength(3, 50))
        .RuleFor(x => x.Products, 
            f => CreateSaleOrderItemFaker.Generate(f.Random.Int(1, 5)));

    public static CreateSaleOrderCommand GenerateValidCommand()
    {
        return CreateSaleOrderCommandFaker.Generate();
    }
}
