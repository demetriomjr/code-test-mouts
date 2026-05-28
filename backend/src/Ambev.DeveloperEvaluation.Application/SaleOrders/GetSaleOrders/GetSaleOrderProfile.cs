using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrders;

/// <summary>
/// Profile for mapping between SaleOrder entity and GetSaleOrdersResponse
/// </summary>
public class GetSaleOrdersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSaleOrders operation
    /// </summary>
    public GetSaleOrdersProfile()
    {
        CreateMap<GetSaleOrdersCommand, SaleOrder>();
        CreateMap<SaleOrder, GetSaleOrdersResult>();
    }
}
