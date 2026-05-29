using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.GetSaleOrders;

/// <summary>
/// Validator for GetSaleOrdersRequest.
/// </summary>
public class GetSaleOrdersRequestValidator : AbstractValidator<GetSaleOrdersRequest>
{
    private static readonly string[] AllowedOrderFields = ["ordernumber", "date", "customername", "branchname", "createdat", "totalsale"];

    /// <summary>
    /// Initializes validation rules for GetSaleOrdersRequest.
    /// </summary>
    public GetSaleOrdersRequestValidator()
    {
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
            .Must(BeValidOrderBy)
            .WithMessage("OrderBy must follow: field [asc|ascii|desc].");
    }

    private static bool BeValidOrderBy(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return true;

        var raw = value.Trim().ToLowerInvariant();
        var tokens = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length == 1)
        {
            if (AllowedOrderFields.Contains(tokens[0]))
                return true;

            foreach (var field in AllowedOrderFields)
            {
                if (raw == $"{field}asc" || raw == $"{field}ascii" || raw == $"{field}desc")
                    return true;
            }

            return false;
        }

        if (tokens.Length == 2)
            return AllowedOrderFields.Contains(tokens[0]) && (tokens[1] is "asc" or "ascii" or "desc");

        return false;
    }
}
