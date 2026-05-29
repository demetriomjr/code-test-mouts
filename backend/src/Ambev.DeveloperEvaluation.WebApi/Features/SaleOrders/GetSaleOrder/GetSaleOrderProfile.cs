using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;
using Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.GetSaleOrder;

/// <summary>
/// Profile for mapping GetSaleOrder feature requests and responses.
/// </summary>
public class GetSaleOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSaleOrder feature.
    /// </summary>
    public GetSaleOrderProfile()
    {
        CreateMap<Guid, GetSaleOrderCommand>()
            .ConstructUsing(id => new GetSaleOrderCommand(id));

        CreateMap<GetSaleOrderResultCommon, GetSaleOrderResponse>();
        CreateMap<GetSaleOrderItemResultCommon, GetSaleOrderItemResponse>();
    }
}
