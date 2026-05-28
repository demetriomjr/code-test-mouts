
namespace Ambev.DeveloperEvaluation.Common.Security;

public interface ISaleOrderItem
{
    public Guid SaleOrderId { get; }
    public bool IsItemCancelled { get; }
}