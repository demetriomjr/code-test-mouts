using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;

/// <summary>
/// Validator for GetSaleOrderCommand.
/// </summary>
public class GetSaleOrderValidator : AbstractValidator<GetSaleOrderCommand>
{
    /// <summary>
    /// Initializes validation rules for GetSaleOrderCommand.
    /// </summary>
    public GetSaleOrderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale order ID is required");
    }
}
