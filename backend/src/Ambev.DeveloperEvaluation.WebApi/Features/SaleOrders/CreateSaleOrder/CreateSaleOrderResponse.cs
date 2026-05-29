using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.CreateSaleOrder;

/// <summary>
/// API response model for CreateSaleOrder operation
/// </summary>
public class CreateSaleOrderResponse
{
    /// <summary>
    /// The unique identifier of the created sale order
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The auto-generated order number
    /// </summary>
    public int OrderNumber { get; set; }

    /// <summary>
    /// The date when the sale order was created
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The customer's name for the sale order
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// The branch name where the sale occurred
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// The total sale amount after discounts
    /// </summary>
    public decimal TotalSale { get; set; }

    /// <summary>
    /// The list of products in the sale order
    /// </summary>
    public IEnumerable<SaleOrderItemResponse> Products { get; set; } = [];

    /// <summary>
    /// Indicates whether the sale order has been cancelled
    /// </summary>
    public CancelStatus CancelStatus { get; set; }

    /// <summary>
    /// The date and time when the sale order was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date and time when the sale order was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// Represents a product item within a sale order response
/// </summary>
public class SaleOrderItemResponse
{
    /// <summary>
    /// The unique identifier of the sale order item
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Indicates whether the item has been cancelled
    /// </summary>
    public CancelStatus CancelStatus { get; set; }

    /// <summary>
    /// The GTIN/EAN code of the product
    /// </summary>
    public string EanGtin { get; set; } = string.Empty;

    /// <summary>
    /// The description of the product
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The unit price of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The quantity of the product ordered
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// The discount percentage applied to this item
    /// </summary>
    public int Discount { get; set; }

    /// <summary>
    /// The total value for this item after discount
    /// </summary>
    public decimal TotalValue { get; set; }

    /// <summary>
    /// The date and time when the item was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date and time when the item was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
