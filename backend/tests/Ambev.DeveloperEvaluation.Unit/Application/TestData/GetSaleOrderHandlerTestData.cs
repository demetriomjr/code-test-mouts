using Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides test data builders for <see cref="GetSaleOrderCommand"/>.
/// </summary>
public static class GetSaleOrderHandlerTestData
{
    /// <summary>
    /// Generates a valid get sale order command.
    /// </summary>
    /// <returns>A valid <see cref="GetSaleOrderCommand"/>.</returns>
    public static GetSaleOrderCommand GenerateValidCommand()
    {
        return new GetSaleOrderCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Generates an invalid get sale order command.
    /// </summary>
    /// <returns>An invalid <see cref="GetSaleOrderCommand"/>.</returns>
    public static GetSaleOrderCommand GenerateInvalidCommand()
    {
        return new GetSaleOrderCommand(Guid.Empty);
    }
}
