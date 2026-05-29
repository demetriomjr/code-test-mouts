namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCreatedEvent
{
    public Guid SaleOrderId { get; }
    public int OrderNumber { get; }
    public DateTime OccurredAtUtc { get; }

    public SaleCreatedEvent(Guid saleOrderId, int orderNumber, DateTime? occurredAtUtc = null)
    {
        SaleOrderId = saleOrderId;
        OrderNumber = orderNumber;
        OccurredAtUtc = occurredAtUtc ?? DateTime.UtcNow;
    }
}
