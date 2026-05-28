namespace Ambev.DeveloperEvaluation.Application.Users.CreateSaleOrder;

public class CreateSaleOrderCommand : IRequest<CreateSaleResult>
{
    public string CustomerName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public IEnumerable<SaleOrderItem> Products { get; set; } = [];
}