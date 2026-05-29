using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.DeleteSaleOrder;

/// <summary>
/// Validator for DeleteSaleOrderRequest.
/// </summary>
public class DeleteSaleOrderRequestValidator : AbstractValidator<DeleteSaleOrderRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteSaleOrderRequest.
    /// </summary>
    public DeleteSaleOrderRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale order ID is required");
    }
}
