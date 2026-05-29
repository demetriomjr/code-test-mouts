using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.DeleteSaleOrder;

/// <summary>
/// Command for deleting a sale order.
/// </summary>
public class DeleteSaleOrderCommand : IRequest<DeleteSaleOrderResponse>
{
    /// <summary>
    /// The unique identifier of the sale order to delete.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteSaleOrderCommand.
    /// </summary>
    /// <param name="id">The ID of the sale order to delete.</param>
    public DeleteSaleOrderCommand(Guid id)
    {
        Id = id;
    }
}
