using System.Diagnostics;
using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;


namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;

public class GetOrderHandler : IRequestHandler<GetSaleOrderCommand, GetSaleOrderResultCommon>
{
    private readonly ISaleOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderHandler(
        ISaleOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<GetSaleOrderResultCommon> Handle(GetSaleOrderCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleOrderValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (order == null)
            throw new KeyNotFoundException($"Sale order with ID {request.Id} not found");

        return _mapper.Map<GetSaleOrderResultCommon>(order);
    }
}
