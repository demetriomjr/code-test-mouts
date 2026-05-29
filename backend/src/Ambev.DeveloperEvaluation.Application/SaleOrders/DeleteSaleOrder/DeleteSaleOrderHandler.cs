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
    private readonly ISaleOrderRepository _orderRepository;
    private readonly ILogger<DeleteSaleOrderHandler> _logger;

    /// <summary>
    /// Initializes a new instance of DeleteSaleOrderHandler.
    /// </summary>
    /// <param name="orderRepository">The sale order repository.</param>
    /// <param name="logger">The ILogger instance.</param>
    public DeleteSaleOrderHandler(ISaleOrderRepository orderRepository, ILogger<DeleteSaleOrderHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the DeleteSaleOrderCommand request.
    /// </summary>
    /// <param name="command">The DeleteSaleOrder command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result of the delete operation.</returns>
    public async Task<DeleteSaleOrderResponse> Handle(DeleteSaleOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleOrderValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _orderRepository.DeleteAsync(command.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Sale order with ID {command.Id} not found");

        _logger.LogInformation("Sale order {SaleOrderId} deleted successfully", command.Id);
        
        //Uncomment this IF the Delete route actually Cancels the Sale Order
        //var order = await _orderRepository.GetByIdAsync(command.Id, cancellationToken);
        //if(order is null)
        //    throw new KeyNotFoundException($"Sale order with ID {command.Id} not found");
        //var saleCancelledEvent = new SaleCancelledEvent(command.Id, order.OrderNumber);
        //_logger.LogInformation("DomainEvent {EventType} {@Event}", nameof(SaleCancelledEvent), saleCancelledEvent);

        return new DeleteSaleOrderResponse { Success = true };
    }
}
