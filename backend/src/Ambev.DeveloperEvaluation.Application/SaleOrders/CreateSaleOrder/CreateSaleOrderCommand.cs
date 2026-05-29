using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;

/// <summary>
/// Command for creating a new sale order.
/// </summary>
/// <remarks>
/// This command captures all required input data for sale order creation,
/// including customer/branch information and the list of products.
/// It implements <see cref="IRequest{TResponse}"/> and returns a
/// <see cref="CreateSaleOrderResult"/> when successfully handled.
/// </remarks>
public class CreateSaleOrderCommand : IRequest<CreateSaleOrderResult>
{
    /// <summary>
    /// Gets or sets the customer name associated with the order.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch name where the sale was made.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of products included in the sale order.
    /// </summary>
    public IEnumerable<CreateSaleOrderItemCommand> Products { get; set; } = [];
}

/// <summary>
/// Represents a product item sent in a sale order creation command.
/// </summary>
public class CreateSaleOrderItemCommand
{
    /// <summary>
    /// Gets or sets the product GTIN/EAN code.
    /// </summary>
    public string EanGtin { get; set;} = string.Empty;

    /// <summary>
    /// Gets or sets the product description.
    /// </summary>
    public string Description { get; set ;} = string.Empty;

    /// <summary>
    /// Gets or sets the unit price for the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the quantity of this product in the order.
    /// </summary>
    public int Amount { get; set; }
}
