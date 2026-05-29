using Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class GetSaleOrderHandlerTestData
{
    public static GetSaleOrderCommand GenerateValidCommand()
    {
        return new GetSaleOrderCommand(Guid.NewGuid());
    }

    public static GetSaleOrderCommand GenerateInvalidCommand()
    {
        return new GetSaleOrderCommand(Guid.Empty);
    }
}
