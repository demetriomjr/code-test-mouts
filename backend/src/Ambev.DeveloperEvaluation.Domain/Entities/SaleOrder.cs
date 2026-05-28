using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleOrder : BaseEntity
{
    public int OrderNumber { get; init; } = 0;
    public DateTime Date { get; init; } = DateTime.UtcNow;
    public string CustomerName { get; set; } = string.Empty
    public string BranchName { get; set; } = string.Empty;
    public decimal TotalSale  { get; private set; }
    public IEnumerable<SaleOrderItem> Products { get; set; } = [];
    public bool IsCancelled { get; private set; } = false;

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

    public void IsCancelled(bool cancelled) => this.IsCancelled = cancelled;

    public void ApplyDiscountAndCalcTotal()
    {
        foreach(var item in this.Products.Where(x => !x.IsCancelled))
        {
            item.Discount = item.Amount switch
            {
                >= 4 and < 10 => 10; //10% for 4-9
                >= 10 and <= 20 => 20; //20% for 10-20
                _ => 0; //0% for <4
            }

            item.TotalValue = (item.Price * item.Amount) * (1 - (Item.Discount / 100));
        }

        this.TotalSale = Products.Sum(item => item.TotalValue);
    }
}