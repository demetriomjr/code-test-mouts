using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.DeleteSaleOrder;

/// <summary>
/// Validator for DeleteSaleOrderCommand.
/// </summary>
public class DeleteSaleOrderValidator : AbstractValidator<DeleteSaleOrderCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteSaleOrderCommand.
    /// </summary>
    public DeleteSaleOrderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale order ID is required");
    }
}
