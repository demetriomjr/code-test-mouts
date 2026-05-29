
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;

/// <summary>
/// Validator for <see cref="CreateSaleOrderCommand"/> that defines sale order creation rules.
/// </summary>
public class CreateSaleOrderValidator : AbstractValidator<CreateSaleOrderCommand>
{
    /// <summary>
    /// Initializes a new instance of <see cref="CreateSaleOrderValidator"/> with validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CustomerName: Required, length between 3 and 50 characters
    /// - BranchName: Required, length between 3 and 50 characters
    /// - Products: Must contain at least one item
    /// - Each product item:
    ///   - EanGtin: Must be a valid GTIN/EAN format when provided
    ///   - Price: Must be greater than zero and have up to 2 decimal places
    ///   - Description: Length between 3 and 50 characters
    ///   - Amount: Must be greater than zero and less than 20
    /// </remarks>
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
                item.RuleFor(item => item.EanGtin)
                    .Must(SaleOrderItemValidator.BeValidGtinOrEan)
                    .When(item => !string.IsNullOrWhiteSpace(item.EanGtin))
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
