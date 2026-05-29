namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCancelledEvent
{
    public Guid SaleOrderId { get; }
    public int OrderNumber { get; }
    public DateTime OccurredAtUtc { get; }

    public SaleCancelledEvent(Guid saleOrderId, int orderNumber, DateTime? occurredAtUtc = null)
    {
        SaleOrderId = saleOrderId;
        OrderNumber = orderNumber;
        OccurredAtUtc = occurredAtUtc ?? DateTime.UtcNow;
    }
}
