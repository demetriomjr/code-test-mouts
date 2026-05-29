namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.DeleteSaleOrder;

/// <summary>
/// Request model for deleting a sale order.
/// </summary>
public class DeleteSaleOrderRequest
{
    /// <summary>
    /// The unique identifier of the sale order to delete.
    /// </summary>
    public Guid Id { get; set; }
}
