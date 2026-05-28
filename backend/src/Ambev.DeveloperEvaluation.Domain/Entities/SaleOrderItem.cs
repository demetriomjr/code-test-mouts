using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleOrderItem : BaseEntity, ISaleOrderItem
{
    public Guid SaleOrderId { get; set; } = Guid.Empty;
    public bool IsCancelled { get; set; } = false;
    public string Ean_Gtin { get; set;} = string.Empty;
    public string Description { get; set ;} = string.Empty;
    public decimal Price { get; set; } = 0.00m;
    public int Amount { get; set; } = 1;
    public int Discount { get; set; } = 0;
    public decimal TotalValue { get; set; } = 0.00m;

    public ValidationResultDetail Validate()
    {
        var validator = new SaleOrderItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}