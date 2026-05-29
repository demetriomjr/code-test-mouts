using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.GetSaleOrder;

/// <summary>
/// Validator for GetSaleOrderRequest.
/// </summary>
public class GetSaleOrderRequestValidator : AbstractValidator<GetSaleOrderRequest>
{
    /// <summary>
    /// Initializes validation rules for GetSaleOrderRequest.
    /// </summary>
    public GetSaleOrderRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale order ID is required");
    }
}
