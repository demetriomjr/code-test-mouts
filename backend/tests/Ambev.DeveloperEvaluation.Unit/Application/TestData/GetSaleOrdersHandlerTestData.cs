using Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrders;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides test data builders for <see cref="GetSaleOrdersCommand"/>.
/// </summary>
public static class GetSaleOrdersHandlerTestData
{
    private static readonly string[] OrderByOptions =
    [
        "date",
        "ordernumber",
        "ordernumber desc",
        "customername asc",
        "branchname desc",
        "createdat"
    ];

    private static readonly Faker<GetSaleOrdersCommand> GetSaleOrdersCommandFaker = new Faker<GetSaleOrdersCommand>()
        .RuleFor(x => x.CurrentPage, 
            f => f.Random.Int(1, 10))
        .RuleFor(x => x.PageSize, 
            f => f.Random.Int(1, 50))
        .RuleFor(x => x.IncludeProductList, 
            f => f.Random.Bool())
        .RuleFor(x => x.OrderNumberFrom, 
            f => f.Random.Bool() ? f.Random.Int(1, 5000) : null)
        .RuleFor(x => x.OrderNumberTo, 
            (f, x) => x.OrderNumberFrom.HasValue
            ? f.Random.Int(x.OrderNumberFrom.Value, x.OrderNumberFrom.Value + 5000)
            : (f.Random.Bool() ? f.Random.Int(1, 5000) : null))
        .RuleFor(x => x.CustomerName, 
            f => f.Random.Bool() ? f.Name.FullName() : null)
        .RuleFor(x => x.BranchName, 
            f => f.Random.Bool() ? f.Company.CompanyName() : null)
        .RuleFor(x => x.CancelStatus, 
            f => f.Random.Bool() ? f.PickRandom(CancelStatus.NotCancelled, CancelStatus.Cancelled) : null)
        .RuleFor(x => x.DateFrom, 
            f => f.Random.Bool() ? f.Date.Past(1).ToUniversalTime() : null)
        .RuleFor(x => x.DateTo, 
            (f, x) => x.DateFrom.HasValue
            ? f.Date.Between(x.DateFrom.Value, DateTime.UtcNow).ToUniversalTime()
            : (f.Random.Bool() ? f.Date.Recent(30).ToUniversalTime() : null))
        .RuleFor(x => x.OrderBy, 
            f => f.PickRandom(OrderByOptions));

    /// <summary>
    /// Generates a valid get sale orders command.
    /// </summary>
    /// <returns>A valid <see cref="GetSaleOrdersCommand"/>.</returns>
    public static GetSaleOrdersCommand GenerateValidCommand()
    {
        return GetSaleOrdersCommandFaker.Generate();
    }
}
