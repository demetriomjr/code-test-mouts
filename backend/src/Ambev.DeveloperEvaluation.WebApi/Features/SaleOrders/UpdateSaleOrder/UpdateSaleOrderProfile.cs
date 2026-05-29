using Ambev.DeveloperEvaluation.Application.SaleOrders.UpdateSaleOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.UpdateSaleOrder;

/// <summary>
/// Profile for mapping between Application and API UpdateSaleOrder requests and responses.
/// </summary>
public class UpdateSaleOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateSaleOrder feature.
    /// </summary>
    public UpdateSaleOrderProfile()
    {
        CreateMap<UpdateSaleOrderRequest, UpdateSaleOrderCommand>();
        CreateMap<UpdateSaleOrderItemRequest, UpdateSaleOrderItemCommand>();

        CreateMap<UpdateSaleOrderResult, UpdateSaleOrderResponse>();
        CreateMap<UpdateSaleOrderItemResult, UpdateSaleOrderItemResponse>();
    }
}
