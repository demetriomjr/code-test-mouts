using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;

/// <summary>
/// Profile for mapping between SaleOrder entity and CreateSaleOrderResponse
/// </summary>
public class CreateSaleOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSaleOrder operation
    /// </summary>
    public CreateSaleOrderProfile()
    {
        CreateMap<CreateSaleOrderCommand, SaleOrder>()
            .ForMember(dest => dest.Date, opt => opt.Ignore());
            //This will ensure that AutoMapper will ignore Date property when mapping, as well as any external attempt to map it
        CreateMap<CreateSaleOrderItemCommand, SaleOrderItem>();
        CreateMap<SaleOrder, CreateSaleOrderResult>();
        CreateMap<SaleOrderItem, SaleOrderItemResult>();
    }
}
