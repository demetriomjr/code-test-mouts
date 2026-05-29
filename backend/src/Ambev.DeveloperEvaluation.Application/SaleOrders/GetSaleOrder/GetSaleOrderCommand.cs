using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;

public record GetSaleOrderCommand : IRequest<GetSaleOrderResultCommon>
{
    public Guid Id { get; }

    public GetSaleOrderCommand(Guid id)
    {
        Id = id;
    }
}
