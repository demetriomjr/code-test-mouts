namespace Ambev.DeveloperEvaluation.Application.SaleOrders.Common;

public class GetSaleOrderResultCommon
{
    public Guid Id { get; set; }
    public int OrderNumber { get; set; }
    public DateTime Date { get; set; }
    public string CustomerName { get; set; }
    public string BranchName { get; set; }
    public decimal TotalSale { get; set; }
    public IEnumerable<GetSaleOrderItemResultCommon> Products { get; set; }
    public bool IsCancelled { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class GetSaleOrderItemResultCommon
{
    public Guid Id { get; set; }
    public bool IsItemCancelled { get; set; }
    public string Ean_Gtin { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int Discount { get; set; }
    public decimal TotalValue { get; set; }
}