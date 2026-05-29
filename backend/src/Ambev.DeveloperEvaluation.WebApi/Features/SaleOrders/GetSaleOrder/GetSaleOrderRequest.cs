namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.GetSaleOrder;

/// <summary>
/// Request model for getting a sale order by ID.
/// </summary>
public class GetSaleOrderRequest
{
    /// <summary>
    /// The unique identifier of the sale order to retrieve.
    /// </summary>
    public Guid Id { get; set; }
}
