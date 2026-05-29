using MediatR;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.UpdateSaleOrder;

/// <summary>
/// Command for updating an existing sale order.
/// </summary>
public class UpdateSaleOrderCommand : IRequest<UpdateSaleOrderResult>
{
    /// <summary>
    /// The unique identifier of the sale order to update.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The customer name associated with the order.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// The branch name where the sale was made.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// The list of products included in the sale order.
    /// </summary>
    public IEnumerable<UpdateSaleOrderItemCommand> Products { get; set; } = [];

    /// <summary>
    /// The cancellation status of the sale order.
    /// </summary>
    public CancelStatus CancelStatus { get; set; }
}

/// <summary>
/// Represents a product item sent in an update sale order command.
/// </summary>
public class UpdateSaleOrderItemCommand
{
    /// <summary>
    /// The GTIN/EAN code of the product.
    /// </summary>
    public string EanGtin { get; set; } = string.Empty;

    /// <summary>
    /// The description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The unit price for the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The quantity of this product in the order.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// The cancellation status of this item.
    /// </summary>
    public CancelStatus CancelStatus { get; set; }
}
