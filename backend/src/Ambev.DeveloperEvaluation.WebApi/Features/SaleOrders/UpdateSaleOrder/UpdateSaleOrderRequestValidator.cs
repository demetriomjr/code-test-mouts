using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.UpdateSaleOrder;

/// <summary>
/// Validator for UpdateSaleOrderRequest that defines validation rules for sale order update.
/// </summary>
public class UpdateSaleOrderRequestValidator : AbstractValidator<UpdateSaleOrderRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleOrderRequestValidator with defined validation rules.
    /// </summary>
    public UpdateSaleOrderRequestValidator()
    {
        RuleFor(order => order.Id)
            .NotEmpty()
            .WithMessage("Sale order ID is required");

        RuleFor(order => order.CustomerName)
            .NotEmpty().WithMessage("Customer must have a name.")
            .MinimumLength(3).WithMessage("Customer's Name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Customer's Name cannot be longer than 50 characters.");

        RuleFor(order => order.BranchName)
            .NotEmpty().WithMessage("Branch must have a name.")
            .MinimumLength(3).WithMessage("Branch's Name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Branch's Name cannot be longer than 50 characters.");

        RuleFor(order => order.Products)
            .NotNull().WithMessage("An order must have at least one product.")
            .Must(products => products.Any()).WithMessage("An order can only be saved having at least one product.");

        RuleForEach(order => order.Products)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.EanGtin)
                    .Must(SaleOrderItemValidator.BeValidGtinOrEan)
                    .When(x => !string.IsNullOrWhiteSpace(x.EanGtin))
                    .WithMessage("Invalid GTIN/EAN value");

                item.RuleFor(x => x.Price)
                    .GreaterThan(0).WithMessage("Price cannot be zero.")
                    .PrecisionScale(18, 2, false).WithMessage("Please, use only 2 decimal cases for cents");

                item.RuleFor(x => x.Description)
                    .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
                    .MaximumLength(50).WithMessage("Description cannot be longer than 50 characters.");

                item.RuleFor(x => x.Amount)
                    .GreaterThan(0).WithMessage("The amount of item must be 1 or more.")
                    .LessThan(20).WithMessage("The total amount cannot be greater than 20.");
            });
    }
}
