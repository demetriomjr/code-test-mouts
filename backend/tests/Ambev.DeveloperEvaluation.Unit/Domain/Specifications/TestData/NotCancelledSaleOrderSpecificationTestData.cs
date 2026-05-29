using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;

public static class NotCancelledSaleOrderSpecificationTestData
{
    private static readonly Faker<SaleOrder> SaleOrderFaker = new Faker<SaleOrder>()
        .RuleFor(x => x.CustomerName, f => f.Person.FullName)
        .RuleFor(x => x.BranchName, f => f.Company.CompanyName())
        .RuleFor(x => x.CancelStatus, f => f.PickRandom(CancelStatus.NotCancelled, CancelStatus.Cancelled));

    public static SaleOrder GenerateSaleOrder(CancelStatus cancelStatus)
    {
        var order = SaleOrderFaker.Generate();
        order.CancelStatus = cancelStatus;
        return order;
    }
}
