using AutoMapper;
using Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.CreateSaleOrder;

/// <summary>
/// Profile for mapping between Application and API CreateSaleOrder responses
/// </summary>
public class CreateSaleOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSaleOrder feature
    /// </summary>
    public CreateSaleOrderProfile()
    {
        // Request to application command mappings.
        CreateMap<CreateSaleOrderRequest, CreateSaleOrderCommand>();
        CreateMap<CreateSaleOrderItemRequest, CreateSaleOrderItemCommand>();

        // Application result to API response mappings.
        CreateMap<CreateSaleOrderResult, CreateSaleOrderResponse>();
        CreateMap<SaleOrderItemResult, SaleOrderItemResponse>();
    }
}
