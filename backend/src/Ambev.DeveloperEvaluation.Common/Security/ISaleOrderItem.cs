
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Common.Security;

public interface ISaleOrderItem
{
    public Guid SaleOrderId { get; }
    public CancelStatus CancelStatus { get; }
}
