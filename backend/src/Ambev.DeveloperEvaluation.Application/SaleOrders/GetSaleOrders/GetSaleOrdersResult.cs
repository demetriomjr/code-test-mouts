using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrders;

public class GetSaleOrdersResult
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<GetSaleOrderResultCommon> Orders { get; set; }
}