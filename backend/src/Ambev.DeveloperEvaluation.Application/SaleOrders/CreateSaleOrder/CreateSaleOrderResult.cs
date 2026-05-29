namespace Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;

/// <summary>
/// Represents the result returned after successfully creating a new sale order.
/// </summary>
public class CreateSaleOrderResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the created sale order.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sequential order number.
    /// </summary>
    public int OrderNumber { get; init; } 

    /// <summary>
    /// Gets or sets the date when the sale was created.
    /// </summary>
    public DateTime Date { get; init; }

    /// <summary>
    /// Gets or sets the customer name associated with the order.
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// Gets or sets the branch name where the sale was made.
    /// </summary>
    public string BranchName { get; set; }

    /// <summary>
    /// Gets or sets the total sale amount after discounts.
    /// </summary>
    public decimal TotalSale  { get; set; }

    /// <summary>
    /// Gets or sets the list of items that compose the sale order.
    /// </summary>
    public IEnumerable<SaleOrderItemResult> Products { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the order is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the order was created in persistence.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the order was last updated in persistence.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// Represents a sale order item in the create sale order result.
/// </summary>
public class SaleOrderItemResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale order item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this item is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the GTIN/EAN code of the product.
    /// </summary>
    public string Ean_Gtin { get; set;}

    /// <summary>
    /// Gets or sets the product description.
    /// </summary>
    public string Description { get; set ;}

    /// <summary>
    /// Gets or sets the product unit price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the ordered quantity for this item.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage applied to this item.
    /// </summary>
    public int Discount { get; set; }

    /// <summary>
    /// Gets or sets the total value for this item after discount.
    /// </summary>
    public decimal TotalValue { get; set; }

    /// <summary>
    /// Gets or sets the date and time when this item was created in persistence.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when this item was last updated in persistence.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
