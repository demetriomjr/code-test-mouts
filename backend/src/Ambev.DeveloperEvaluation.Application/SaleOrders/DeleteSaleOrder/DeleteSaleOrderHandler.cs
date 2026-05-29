using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.DeleteSaleOrder;

public class DeleteSaleOrderHandler : IRequestHandler<DeleteSaleOrderCommand, DeleteSaleOrderResponse>
{
    private readonly ISaleOrderRepository _saleOrderRepository;

    public DeleteSaleOrderHandler(ISaleOrderRepository saleOrderRepository)
    {
        _saleOrderRepository = saleOrderRepository;
    }

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
