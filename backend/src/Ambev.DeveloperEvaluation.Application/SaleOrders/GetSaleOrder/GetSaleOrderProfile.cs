using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;

/// <summary>
/// Profile for mapping between SaleOrder entity and common GetSaleOrder result.
/// </summary>
public class GetSaleOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSaleOrder operation.
    /// </summary>
    public GetSaleOrderProfile()
    {
        CreateMap<SaleOrder, GetSaleOrderResultCommon>();
        CreateMap<SaleOrderItem, GetSaleOrderItemResultCommon>();
    }
}
