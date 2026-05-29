using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.UpdateSaleOrder;

public class UpdateSaleOrderResult
{
    public Guid Id { get; set; }
    public int OrderNumber { get; set; }
    public DateTime Date { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public decimal TotalSale { get; set; }
    public IEnumerable<UpdateSaleOrderItemResult> Products { get; set; } = [];
    public CancelStatus CancelStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class UpdateSaleOrderItemResult
{
    public Guid Id { get; set; }
    public Guid SaleOrderId { get; set; }
    public CancelStatus CancelStatus { get; set; }
    public string Ean_Gtin { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int Discount { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
