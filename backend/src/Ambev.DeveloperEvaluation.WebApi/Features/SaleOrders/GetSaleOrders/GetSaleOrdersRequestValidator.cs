using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.GetSaleOrders;

/// <summary>
/// Validator for GetSaleOrdersRequest.
/// </summary>
public class GetSaleOrdersRequestValidator : AbstractValidator<GetSaleOrdersRequest>
{
    /// <summary>
    /// Initializes validation rules for GetSaleOrdersRequest.
    /// </summary>
    public GetSaleOrdersRequestValidator()
    {
        var allowedOrderBy = new[] { "ordernumber", "date", "customername", "branchname", "createdat", "totalsale" };
        var allowedOrderDirection = new[] { "asc", "desc" };

        RuleFor(x => x.CurrentPage)
            .GreaterThan(0)
            .WithMessage("CurrentPage must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0.");

        RuleFor(x => x)
            .Must(x => !x.OrderNumberFrom.HasValue || !x.OrderNumberTo.HasValue || x.OrderNumberFrom <= x.OrderNumberTo)
            .WithMessage("OrderNumberFrom must be less than or equal to OrderNumberTo.");

        RuleFor(x => x)
            .Must(x => !x.DateFrom.HasValue || !x.DateTo.HasValue || x.DateFrom <= x.DateTo)
            .WithMessage("DateFrom must be less than or equal to DateTo.");

        RuleFor(x => x.OrderBy)
            .Must(value => string.IsNullOrWhiteSpace(value) || allowedOrderBy.Contains(value.Trim().ToLowerInvariant()))
            .WithMessage("OrderBy must be one of: orderNumber, date, customerName, branchName, createdAt, totalSale.");

        RuleFor(x => x.OrderDirection)
            .Must(value => string.IsNullOrWhiteSpace(value) || allowedOrderDirection.Contains(value.Trim().ToLowerInvariant()))
            .WithMessage("OrderDirection must be either asc or desc.");
    }
}
