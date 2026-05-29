using MediatR;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.UpdateSaleOrder;

public record UpdateSaleOrderCommand : IRequest<UpdateSaleOrderResult>
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public IEnumerable<UpdateSaleOrderItemCommand> Products { get; set; } = [];
    public CancelStatus CancelStatus { get; set; }
}

public class UpdateSaleOrderItemCommand
{
    public string Ean_Gtin { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public CancelStatus CancelStatus { get; set; }
}
