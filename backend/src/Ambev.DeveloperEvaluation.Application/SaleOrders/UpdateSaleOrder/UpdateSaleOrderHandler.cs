using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.UpdateSaleOrder;

/// <summary>
/// Handler for processing UpdateSaleOrderCommand requests.
/// </summary>
public class UpdateSaleOrderHandler : IRequestHandler<UpdateSaleOrderCommand, UpdateSaleOrderResult>
{
    private readonly ISaleOrderRepository _saleOrderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSaleOrderHandler> _logger;

    /// <summary>
    /// Initializes a new instance of UpdateSaleOrderHandler.
    /// </summary>
    /// <param name="saleOrderRepository">The sale order repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public UpdateSaleOrderHandler(ISaleOrderRepository saleOrderRepository, IMapper mapper, ILogger<UpdateSaleOrderHandler> logger)
    {
        _saleOrderRepository = saleOrderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the UpdateSaleOrderCommand request.
    /// </summary>
    /// <param name="request">The UpdateSaleOrder command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated sale order details.</returns>
    public async Task<UpdateSaleOrderResult> Handle(UpdateSaleOrderCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleOrderValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingOrder = await _saleOrderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existingOrder == null)
            throw new KeyNotFoundException($"Sale order with ID {request.Id} not found");

        existingOrder.CustomerName = request.CustomerName;
        existingOrder.BranchName = request.BranchName;
        existingOrder.CancelStatus = request.CancelStatus;

        var products = _mapper.Map<List<SaleOrderItem>>(request.Products);
        foreach (var item in products)
            item.SaleOrderId = existingOrder.Id;

        existingOrder.Products = products;
        existingOrder.ApplyDiscountAndCalcTotal();

        var updatedOrder = await _saleOrderRepository.UpdateAsync(existingOrder, cancellationToken);

        var saleModifiedEvent = new SaleModifiedEvent(updatedOrder.Id, updatedOrder.OrderNumber);
        _logger.LogInformation("DomainEvent {EventType} {@Event}", nameof(SaleModifiedEvent), saleModifiedEvent);

        if (updatedOrder.CancelStatus == CancelStatus.Cancelled)
        {
            var saleCancelledEvent = new SaleCancelledEvent(updatedOrder.Id, updatedOrder.OrderNumber);
            _logger.LogInformation("DomainEvent {EventType} {@Event}", nameof(SaleCancelledEvent), saleCancelledEvent);
        }

        foreach (var item in updatedOrder.Products.Where(x => x.CancelStatus == CancelStatus.Cancelled))
        {
            var itemCancelledEvent = new ItemCancelledEvent(updatedOrder.Id, updatedOrder.OrderNumber, item.Id);
            _logger.LogInformation("DomainEvent {EventType} {@Event}", nameof(ItemCancelledEvent), itemCancelledEvent);
        }

        return _mapper.Map<UpdateSaleOrderResult>(updatedOrder);
    }
}
