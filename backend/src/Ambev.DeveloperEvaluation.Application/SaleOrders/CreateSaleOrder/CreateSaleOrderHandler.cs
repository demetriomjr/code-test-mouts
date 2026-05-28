using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;

public class CreateSaleOrderHandler : IRequestHandler<CreateSaleOrderCommand, CreateSaleOrderResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleOrderRepository _orderRepository;

    public CreateSaleOrderHandler(ISaleOrderRepository orderRepository, IMapper mapper)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Handles the CreateSaleOrderCommand request
    /// </summary>
    /// <param name="command">The CreateSaleOrder command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale order details</returns>
    public async Task<CreateSaleOrderResult> Handle(CreateSaleOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleOrderValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        int nextOrderNumber = await _orderRepository.GetLastOrderNumber(cancellationToken) + 1;
        var order = _mapper.Map<SaleOrder>(command);
        order.OrderNumber = nextOrderNumber;
        order.ApplyDiscountAndCalcTotal();
        var createdOrder = await _orderRepository.CreateAsync(order, cancellationToken);
        var result = _mapper.Map<CreateSaleOrderResult>(createdOrder);
        return result;
    }
}