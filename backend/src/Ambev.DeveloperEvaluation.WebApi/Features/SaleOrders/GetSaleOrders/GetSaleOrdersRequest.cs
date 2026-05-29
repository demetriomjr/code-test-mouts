namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.GetSaleOrders;

/// <summary>
/// Request model for retrieving a paginated list of sale orders
/// </summary>
public class GetSaleOrdersRequest
{
    /// <summary>
    /// The current page number to retrieve
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The number of items per page
    /// </summary>
    public int PageSize { get; set; }
}
