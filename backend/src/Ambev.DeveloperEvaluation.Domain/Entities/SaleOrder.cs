using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleOrder : BaseEntity
{
    public int OrderNumber { get; set; } = 0;
    public DateTime Date { get; init; } = DateTime.UtcNow;
    public string CustomerName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public decimal TotalSale  { get; set; } = 0.00m;
    public IEnumerable<SaleOrderItem> Products { get; set; } = [];
    public bool IsCancelled { get; set; } = false;

    public ValidationResultDetail Validate()
    {
        var validator = new SaleOrderValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    public void SetIsCancelled(bool cancelled) => this.IsCancelled = cancelled;

    public void ApplyDiscountAndCalcTotal()
    {
        foreach(var item in this.Products.Where(x => !x.IsCancelled))
        {
            item.Discount = item.Amount switch
            {
                >= 4 and < 10 => 10, //10% for 4-9
                >= 10 and <= 20 => 20, //20% for 10-20
                _ => 0 //0% for <4
            };

            item.TotalValue = (item.Price * item.Amount) * (1 - (item.Discount / 100m));
        }

        this.TotalSale = Products.Sum(item => item.TotalValue);
    }
}