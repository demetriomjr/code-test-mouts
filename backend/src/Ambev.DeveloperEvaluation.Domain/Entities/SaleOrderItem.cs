using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item that belongs to a sale order.
/// </summary>
public class SaleOrderItem : BaseEntity, ISaleOrderItem
{
    /// <summary>
    /// Gets or sets the identifier of the parent sale order.
    /// </summary>
    public Guid SaleOrderId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the cancellation status of the item.
    /// </summary>
    public CancelStatus CancelStatus { get; set; } = CancelStatus.NotCancelled;

    /// <summary>
    /// Gets or sets the GTIN/EAN product code.
    /// </summary>
    public string EanGtin { get; set;} = string.Empty;

    /// <summary>
    /// Gets or sets the item description.
    /// </summary>
    public string Description { get; set ;} = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the item.
    /// </summary>
    public decimal Price { get; set; } = 0.00m;

    /// <summary>
    /// Gets or sets the ordered amount of this item.
    /// </summary>
    public int Amount { get; set; } = 1;

    /// <summary>
    /// Gets or sets the discount percentage applied to this item.
    /// </summary>
    public int Discount { get; set; } = 0;

    /// <summary>
    /// Gets or sets the total value for this item after discount.
    /// </summary>
    public decimal TotalValue { get; set; } = 0.00m;

    /// <summary>
    /// Validates the current sale order item instance.
    /// </summary>
    /// <returns>The validation result containing status and errors.</returns>
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
