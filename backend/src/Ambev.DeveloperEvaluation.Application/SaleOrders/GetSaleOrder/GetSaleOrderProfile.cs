using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;

public class GetSaleOrderProfile : Profile
{
    public GetSaleOrderProfile()
    {
        CreateMap<SaleOrder, GetSaleOrderResultCommon>();
        CreateMap<SaleOrderItem, GetSaleOrderItemResultCommon>();
    }
}
