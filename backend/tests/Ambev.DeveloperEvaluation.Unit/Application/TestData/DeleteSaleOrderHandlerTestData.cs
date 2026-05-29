using Ambev.DeveloperEvaluation.Application.SaleOrders.DeleteSaleOrder;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides test data builders for <see cref="DeleteSaleOrderCommand"/>.
/// </summary>
public static class DeleteSaleOrderHandlerTestData
{
    /// <summary>
    /// Generates a valid delete sale order command.
    /// </summary>
    /// <returns>A valid <see cref="DeleteSaleOrderCommand"/>.</returns>
    public static DeleteSaleOrderCommand GenerateValidCommand()
    {
        return new DeleteSaleOrderCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Generates an invalid delete sale order command.
    /// </summary>
    /// <returns>An invalid <see cref="DeleteSaleOrderCommand"/>.</returns>
    public static DeleteSaleOrderCommand GenerateInvalidCommand()
    {
        return new DeleteSaleOrderCommand(Guid.Empty);
    }
}
