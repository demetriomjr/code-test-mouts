
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;

public class CreateSaleOrderValidator : AbstractValidator<CreateSaleOrderCommand>
{
    public CreateSaleOrderValidator()
    {
        RuleFor(order => order.CustomerName)
            .NotEmpty().WithMessage("Customer must have a name.")
            .MinimumLength(3).WithMessage("Customer's Name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Customer's Name cannot be longer than 50 characters.");
        
        RuleFor(order => order.BranchName)
            .NotEmpty().WithMessage("Branch must have a name.")
            .MinimumLength(3).WithMessage("Branch's Name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Branch's Name cannot be longer than 50 characters.");
        
        RuleFor(order => order.Products)
            .Must(products => products is not null && products.Any())
            .WithMessage("An order can only be saved having at least one product.");
        
        RuleForEach(order => order.Products)
            .ChildRules(item => {
                item.RuleFor(item => item.Ean_Gtin)
                    .Must(SaleOrderItemValidator.BeValidGtinOrEan)
                    .When(item => !string.IsNullOrWhiteSpace(item.Ean_Gtin))
                    .WithMessage("Invalid GTIN/EAN value");

                item.RuleFor(item => item.Price)
                    .GreaterThan(0).WithMessage("Price cannot be zero.")
                    .ScalePrecision(2, 18).WithMessage("Please, use only 2 decimal cases for cents");

                item.RuleFor(item => item.Description)
                    .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
                    .MaximumLength(50).WithMessage("Description cannot be longer than 50 characters.");
                
                item.RuleFor(item => item.Amount)
                    .GreaterThan(0).WithMessage("The amount of item must be 1 or more.")
                    .LessThan(20).WithMessage("The total amount cannot be greater than 20.");
            });
    }
}