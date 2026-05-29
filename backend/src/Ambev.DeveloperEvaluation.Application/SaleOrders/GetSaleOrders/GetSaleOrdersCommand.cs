using MediatR;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrders;

/// <summary>
/// Command for retrieving a paginated list of sale orders
/// </summary>
public class GetSaleOrdersCommand : IRequest<GetSaleOrdersResult>
{
    /// <summary>
    /// The current page number to retrieve
    /// </summary>
    public int CurrentPage { get; set; } = 1;

    /// <summary>
    /// The number of items per page
    /// </summary>
    public int PageSize { get; set; } = 20;

    /// <summary>
    /// Include product list in each order response.
    /// </summary>
    public bool IncludeProductList { get; set; } = true;

    /// <summary>
    /// Filter by order number range start.
    /// </summary>
    public int? OrderNumberFrom { get; set; }

    /// <summary>
    /// Filter by order number range end.
    /// </summary>
    public int? OrderNumberTo { get; set; }

    /// <summary>
    /// Filter by customer name (contains).
    /// </summary>
    public string? CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Filter by branch name (contains).
    /// </summary>
    public string? BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Filter by cancellation status.
    /// </summary>
    public CancelStatus? CancelStatus { get; set; }

    /// <summary>
    /// Filter by order creation date range start (UTC).
    /// </summary>
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Filter by order creation date range end (UTC).
    /// </summary>
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Field used for ordering.
    /// </summary>
    public string? OrderBy { get; set; }
}
