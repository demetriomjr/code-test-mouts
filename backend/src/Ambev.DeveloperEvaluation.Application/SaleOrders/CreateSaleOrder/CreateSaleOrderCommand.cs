using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;

public class CreateSaleOrderCommand : IRequest<CreateSaleOrderResult>
{
    public string CustomerName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public IEnumerable<CreateSaleOrderItemCommand> Products { get; set; } = [];
}

public class CreateSaleOrderItemCommand
{
    public string Ean_Gtin { get; set;}
    public string Description { get; set ;}
    public decimal Price { get; set; }
    public int Amount { get; set; }
}