using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale order aggregate root.
/// </summary>
public class SaleOrder : BaseEntity
{
    /// <summary>
    /// Gets or sets the sequential order number.
    /// </summary>
    public int OrderNumber { get; set; } = 0;

    /// <summary>
    /// Gets the date when the order was created.
    /// </summary>
    public DateTime Date { get; init; } = DateTime.UtcNow;

    //MVP is gonna use a string as Customer due to time constraints, but a Customer Entity should be a better fit.
    /// <summary>
    /// Gets or sets the customer name associated with the order.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch name where the sale was made.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total sale amount after discount calculations.
    /// </summary>
    public decimal TotalSale  { get; set; } = 0.00m;

    /// <summary>
    /// Gets or sets the product items that compose this sale order.
    /// </summary>
    public IEnumerable<SaleOrderItem> Products { get; set; } = [];

    /// <summary>
    /// Gets or sets the cancellation status of the sale order.
    /// </summary>
    public CancelStatus CancelStatus { get; set; } = CancelStatus.NotCancelled;

    /// <summary>
    /// Validates the current sale order instance.
    /// </summary>
    /// <returns>The validation result containing status and errors.</returns>
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

    /// <summary>
    /// Sets the cancellation status for the sale order.
    /// </summary>
    /// <param name="cancelStatus">The cancellation status to apply.</param>
    public void SetCancelStatus(CancelStatus cancelStatus) => this.CancelStatus = cancelStatus;

    /// <summary>
    /// Applies discount rules to active items and recalculates order totals.
    /// </summary>
    public void ApplyDiscountAndCalcTotal()
    {
        foreach(var item in this.Products.Where(x => x.CancelStatus == CancelStatus.NotCancelled))
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
