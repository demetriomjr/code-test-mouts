using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class NotCancelledSaleOrderSpecification : ISpecification<SaleOrder>
{
    public bool IsSatisfiedBy(SaleOrder saleOrder)
    {
        return saleOrder.CancelStatus == CancelStatus.NotCancelled;
    }
}
