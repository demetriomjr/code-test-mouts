using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.DeleteSaleOrder;

/// <summary>
/// Handler for processing DeleteSaleOrderCommand requests.
/// </summary>
public class DeleteSaleOrderHandler : IRequestHandler<DeleteSaleOrderCommand, DeleteSaleOrderResponse>
{
    private readonly ISaleOrderRepository _saleOrderRepository;

    /// <summary>
    /// Initializes a new instance of DeleteSaleOrderHandler.
    /// </summary>
    /// <param name="saleOrderRepository">The sale order repository.</param>
    public DeleteSaleOrderHandler(ISaleOrderRepository saleOrderRepository)
    {
        _saleOrderRepository = saleOrderRepository;
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

        var success = await _saleOrderRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Sale order with ID {request.Id} not found");

        return new DeleteSaleOrderResponse { Success = true };
    }
}
