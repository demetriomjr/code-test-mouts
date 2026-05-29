using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.DeleteSaleOrder;

public class DeleteSaleOrderValidator : AbstractValidator<DeleteSaleOrderCommand>
{
    public DeleteSaleOrderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale order ID is required");
    }
}
