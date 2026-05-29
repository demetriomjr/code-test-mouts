using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;

public class GetSaleOrderValidator : AbstractValidator<GetSaleOrderCommand>
{
    public GetSaleOrderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale order ID is required");
    }
}
