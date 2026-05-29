namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.CreateSaleOrder;

/// <summary>
/// Represents a request to create a new sale order in the system.
/// </summary>
/// <remarks>
/// This request provides the customer and branch information plus
/// all products required to create a new sale order.
/// </remarks>
public class CreateSaleOrderRequest
{
    /// <summary>
    /// Gets or sets the customer name for the sale order.
    /// Must be a valid name with at least 3 characters.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch name where the sale occurs.
    /// Must be a valid name with at least 3 characters.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of products included in the sale order.
    /// The order must contain at least one product.
    /// </summary>
    public IEnumerable<CreateSaleOrderItemRequest> Products { get; set; } = [];
}

/// <summary>
/// Represents an individual product item within a sale order request.
/// </summary>
public class CreateSaleOrderItemRequest
{
    /// <summary>
    /// Gets or sets the GTIN/EAN code of the product.
    /// Must be a valid GTIN-8, UPC-A, EAN-13, or GTIN-14.
    /// </summary>
    public string Ean_Gtin { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the product.
    /// Must be between 3 and 50 characters long.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// Must be greater than zero.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product being ordered.
    /// Must be between 1 and 20.
    /// </summary>
    public int Amount { get; set; }
}
