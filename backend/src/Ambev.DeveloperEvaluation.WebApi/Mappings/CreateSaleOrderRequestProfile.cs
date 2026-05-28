using Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.CreateSaleOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class CreateSaleOrderRequestProfile : Profile
{
    public CreateSaleOrderRequestProfile()
    {
        CreateMap<CreateSaleOrderRequest, CreateSaleOrderCommand>();
    }
}
