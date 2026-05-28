using FluentValidation;
using System.Text.RegularExpressions;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleOrderValidator : AbstractValidator<SaleOrder>
{
    public SaleOrderValidator()
    {
        RuleFor(order => order.CustomerName)
            .NotEmpty().WithMessage("Customer must have a name.")
            .MinimumLength(3).WithMessage("Customer's Name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Customer's Name cannot be longer than 50 characters.");
        
        RuleFor(order => order.BranchName)
            .NotEmpty().WithMessage("Branch must have a name.")
            .MinimumLength(3).WithMessage("Branch's Name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Branch's Name cannot be longer than 50 characters.");
        
        RuleFor(order => order.Products )
            .Must(products => products is not null && products.Any())
            .WithMessage("An order can only be saved having at least one product.");
    }

}