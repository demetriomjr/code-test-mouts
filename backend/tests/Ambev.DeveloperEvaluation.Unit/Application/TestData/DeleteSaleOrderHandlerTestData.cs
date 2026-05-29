using Ambev.DeveloperEvaluation.Application.SaleOrders.DeleteSaleOrder;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class DeleteSaleOrderHandlerTestData
{
    public static DeleteSaleOrderCommand GenerateValidCommand()
    {
        return new DeleteSaleOrderCommand(Guid.NewGuid());
    }

    public static DeleteSaleOrderCommand GenerateInvalidCommand()
    {
        return new DeleteSaleOrderCommand(Guid.Empty);
    }
}
