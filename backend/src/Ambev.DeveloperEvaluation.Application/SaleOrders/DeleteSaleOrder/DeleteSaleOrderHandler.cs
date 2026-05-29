using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.DeleteSaleOrder;

/// <summary>
/// Handler for processing DeleteSaleOrderCommand requests.
/// </summary>
public class DeleteSaleOrderHandler : IRequestHandler<DeleteSaleOrderCommand, DeleteSaleOrderResponse>
{
    private readonly ISaleOrderRepository _saleOrderRepository;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of DeleteSaleOrderHandler.
    /// </summary>
    /// <param name="saleOrderRepository">The sale order repository.</param>
    /// <param name="logger">The ILogger instance.</param>
    public DeleteSaleOrderHandler(ISaleOrderRepository saleOrderRepository, ILogger logger)
    {
        _saleOrderRepository = saleOrderRepository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the DeleteSaleOrderCommand request.
    /// </summary>
    /// <param name="request">The DeleteSaleOrder command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result of the delete operation.</returns>
    public async Task<DeleteSaleOrderResponse> Handle(DeleteSaleOrderCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleOrderValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var order = await _saleOrderRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if(order is null)
            throw new KeyNotFoundException($"Order not found for ID {request.Id}");

        var success = await _saleOrderRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Sale order with ID {request.Id} not found");
        
        //Uncomment this IF the Delete route actually Cancels the Sale Order
        //var saleCancelledEvent = new SaleCancelledEvent(request.Id, order.OrderNumber);
        //_logger.LogInformation("DomainEvent {EventType} {@Event}", nameof(SaleCancelledEvent), saleCancelledEvent);

        return new DeleteSaleOrderResponse { Success = true };
    }
}
