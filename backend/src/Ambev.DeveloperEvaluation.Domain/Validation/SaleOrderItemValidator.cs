using FluentValidation;
using System.Text.RegularExpressions;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleOrderItemValidator : AbstractValidator<SaleOrderItem>
{
    public SaleOrderItemValidator()
    {
        RuleFor(item => item.SaleOrderId)
            .NotEmpty()
            .WithMessage("A valida GUID Sale Order ID must Be provided");

        RuleFor(item => item.Ean_Gtin)
            .Must(BeValidGtinOrEan)
            .When(item => !string.IsNullOrWhiteSpace(item))
            .WithMessage("Invalid GTIN/EAN value");

        RuleFor(item => item.Price)
            .GreaterThan(0).WithMessage("Price cannot be zero.")
            .ScalePrecision(2, 18).WithMessage("Please, use only 2 decimal cases for cents");

        RuleFor(item => item.Description)
            .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Description cannot be longer than 50 characters.");
        
        RuleFor(item => item.Amount)
            .GreaterThan(0).WithMessage("The amount of item must be 1 or more.")
            .LessThan(20).WithMessage("The total amount cannot be greater than 20.");
    }

    //common known gtin/ean validator
    public static bool BeValidGtinOrEan(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        var code = value.Trim();

        // GTIN/EAN accepted lengths:
        // EAN-8, UPC-A, EAN-13, GTIN-14
        if (code.Length is not (8 or 12 or 13 or 14))
            return false;

        if (!code.All(char.IsDigit))
            return false;

        var sum = 0;
        var multiplyByThree = true;

        for (var i = code.Length - 2; i >= 0; i--)
        {
            var digit = code[i] - '0';
            sum += multiplyByThree ? digit * 3 : digit;
            multiplyByThree = !multiplyByThree;
        }

        var expectedCheckDigit = (10 - (sum % 10)) % 10;
        var actualCheckDigit = code[^1] - '0';

        return expectedCheckDigit == actualCheckDigit;
    }
}