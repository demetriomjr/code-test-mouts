using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.UpdateSaleOrder;

/// <summary>
/// Profile for mapping between SaleOrder entity and UpdateSaleOrder models.
/// </summary>
public class UpdateSaleOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateSaleOrder operation.
    /// </summary>
    public UpdateSaleOrderProfile()
    {
        CreateMap<UpdateSaleOrderCommand, SaleOrder>()
            .ForMember(dest => dest.OrderNumber, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ForMember(dest => dest.TotalSale, opt => opt.Ignore());

        CreateMap<UpdateSaleOrderItemCommand, SaleOrderItem>()
            .ForMember(dest => dest.SaleOrderId, opt => opt.Ignore())
            .ForMember(dest => dest.Discount, opt => opt.Ignore())
            .ForMember(dest => dest.TotalValue, opt => opt.Ignore());

        CreateMap<SaleOrder, UpdateSaleOrderResult>();
        CreateMap<SaleOrderItem, UpdateSaleOrderItemResult>();
    }
}
