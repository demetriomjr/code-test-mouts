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
        CreateMap<CreateSaleOrderCommand, SaleOrder>();
        CreateMap<SaleOrder, CreateSaleOrderResult>();
    }
}
