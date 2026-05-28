using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrders;

public class GetSaleOrdersCommand : IRequest<GetSaleOrdersResult>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}
