using AutoMapper;
using Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrders;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.GetSaleOrders;

/// <summary>
/// Profile for mapping between Application and API GetSaleOrders responses
/// </summary>
public class GetSaleOrdersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSaleOrders feature
    /// </summary>
    public GetSaleOrdersProfile()
    {
        CreateMap<GetSaleOrdersRequest, GetSaleOrdersCommand>();
        CreateMap<GetSaleOrdersResult, GetSaleOrdersResponse>();
    }
}
