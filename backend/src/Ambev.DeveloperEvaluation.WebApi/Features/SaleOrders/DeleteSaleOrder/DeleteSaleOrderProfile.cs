using Ambev.DeveloperEvaluation.Application.SaleOrders.DeleteSaleOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.DeleteSaleOrder;

/// <summary>
/// Profile for mapping DeleteSaleOrder feature requests to commands.
/// </summary>
public class DeleteSaleOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteSaleOrder feature.
    /// </summary>
    public DeleteSaleOrderProfile()
    {
        CreateMap<Guid, DeleteSaleOrderCommand>()
            .ConstructUsing(id => new DeleteSaleOrderCommand(id));
    }
}
