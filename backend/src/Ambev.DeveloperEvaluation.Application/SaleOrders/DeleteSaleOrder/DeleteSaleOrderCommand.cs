using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.DeleteSaleOrder;

public record DeleteSaleOrderCommand : IRequest<DeleteSaleOrderResponse>
{
    public Guid Id { get; }

    public DeleteSaleOrderCommand(Guid id)
    {
        Id = id;
    }
}
