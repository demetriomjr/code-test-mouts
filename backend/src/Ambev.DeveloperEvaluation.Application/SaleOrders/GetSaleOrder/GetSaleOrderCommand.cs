using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;

/// <summary>
/// Command for retrieving a sale order by its ID.
/// </summary>
public class GetSaleOrderCommand : IRequest<GetSaleOrderResultCommon>
{
    /// <summary>
    /// The unique identifier of the sale order to retrieve.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of GetSaleOrderCommand.
    /// </summary>
    /// <param name="id">The ID of the sale order to retrieve.</param>
    public GetSaleOrderCommand(Guid id)
    {
        Id = id;
    }
}
