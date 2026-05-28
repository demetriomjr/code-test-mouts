namespace Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;

public class CreateSaleOrderResult
{
    public Guid Id { get; set; }
    public int OrderNumber { get; init; } 
    public DateTime Date { get; init; }
    public string CustomerName { get; set; }
    public string BranchName { get; set; }
    public decimal TotalSale  { get; set; }
    public IEnumerable<SaleOrderItemResult> Products { get; set; }
    public bool IsCancelled { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class SaleOrderItemResult
{
    public Guid Id { get; set; }
    public bool IsCancelled { get; set; }
    public string Ean_Gtin { get; set;}
    public string Description { get; set ;}
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int Discount { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}