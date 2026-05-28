namespace Ambev.DeveloperEvaluation.Application.Users.CreateSaleOrder;

public class CreateSaleOrderResult
{
    public Guid Id { get; set; }
    public int OrderNumber { get; init; } 
    public DateTime Date { get; init; }
    public string CustomerName { get; set; }
    public string BranchName { get; set; }
    public decimal TotalSale  { get; private set; }
    public IEnumerable<SaleOrderItemResult> Products { get; set; }
    public bool IsCancelled { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class SaleOrderItemResult
{
    public Guid Id { get; set; }
    public bool IsItemCancelled { get; set; }
    public string Ean_Gtin { get; set;}
    public string Description { get; set ;}
    public decimal Price { get; set; }
    public int Amount { get; private set; }
    public int Discount = { get;  private set; }
    public decimal TotalValue { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}