using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.UpdateSaleOrder;

public class UpdateSaleOrderHandler : IRequestHandler<UpdateSaleOrderCommand, UpdateSaleOrderResult>
{
    private readonly ISaleOrderRepository _saleOrderRepository;
    private readonly IMapper _mapper;

    public UpdateSaleOrderHandler(ISaleOrderRepository saleOrderRepository, IMapper mapper)
    {
        _saleOrderRepository = saleOrderRepository;
        _mapper = mapper;
    }

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
        return _mapper.Map<UpdateSaleOrderResult>(updatedOrder);
    }
}
