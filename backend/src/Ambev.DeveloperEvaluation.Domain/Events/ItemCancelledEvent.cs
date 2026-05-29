namespace Ambev.DeveloperEvaluation.Domain.Events;

public class ItemCancelledEvent
{
    public Guid SaleOrderId { get; }
    public int OrderNumber { get; }
    public Guid SaleOrderItemId { get; }
    public DateTime OccurredAtUtc { get; }

    public ItemCancelledEvent(Guid saleOrderId, int orderNumber, Guid saleOrderItemId, DateTime? occurredAtUtc = null)
    {
        SaleOrderId = saleOrderId;
        OrderNumber = orderNumber;
        SaleOrderItemId = saleOrderItemId;
        OccurredAtUtc = occurredAtUtc ?? DateTime.UtcNow;
    }
}
