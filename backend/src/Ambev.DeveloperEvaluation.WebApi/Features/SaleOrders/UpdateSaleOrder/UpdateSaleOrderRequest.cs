using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.UpdateSaleOrder;

/// <summary>
/// Request model for updating an existing sale order.
/// </summary>
public class UpdateSaleOrderRequest
{
    /// <summary>
    /// The unique identifier of the sale order to update.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the customer name for the sale order.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch name where the sale occurs.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of products included in the sale order.
    /// </summary>
    public IEnumerable<UpdateSaleOrderItemRequest> Products { get; set; } = [];

    /// <summary>
    /// Gets or sets the cancellation status of the sale order.
    /// </summary>
    public CancelStatus CancelStatus { get; set; }
}

/// <summary>
/// Represents an individual product item within a sale order update request.
/// </summary>
public class UpdateSaleOrderItemRequest
{
    /// <summary>
    /// Gets or sets the GTIN/EAN code of the product.
    /// </summary>
    public string EanGtin { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product being ordered.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Gets or sets the cancellation status of the item.
    /// </summary>
    public CancelStatus CancelStatus { get; set; }
}
