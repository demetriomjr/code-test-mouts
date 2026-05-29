using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.GetSaleOrders;

/// <summary>
/// API response model for the paginated sale orders query
/// </summary>
public class GetSaleOrdersResponse
{
    /// <summary>
    /// The current page number
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The total number of pages available
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// The list of sale orders for the current page
    /// </summary>
    public IEnumerable<GetSaleOrderResultCommon> Orders { get; set; }
}
