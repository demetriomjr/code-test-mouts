using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;


namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;

/// <summary>
/// Handler for processing GetSaleOrderCommand requests.
/// </summary>
public class GetSaleOrderHandler : IRequestHandler<GetSaleOrderCommand, GetSaleOrderResultCommon>
{
    private readonly ISaleOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetSaleOrderHandler.
    /// </summary>
    /// <param name="orderRepository">The sale order repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetSaleOrderHandler(
        ISaleOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSaleOrderCommand request.
    /// </summary>
    /// <param name="command">The GetSaleOrder command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The sale order details if found.</returns>
    public async Task<GetSaleOrderResultCommon> Handle(GetSaleOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new GetSaleOrderValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var order = await _orderRepository.GetByIdAsync(command.Id, cancellationToken);
        
        if (order == null)
            throw new KeyNotFoundException($"Sale order with ID {command.Id} not found");

        return _mapper.Map<GetSaleOrderResultCommon>(order);
    }
}
