using Ambev.DeveloperEvaluation.Application.orders.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.orders.Getorder;

public class GetorderHandler : IRequestHandler<GetorderCommand, GetorderResultCommon>
{
    private readonly IorderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetorderHandler(
        IorderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<GetorderResultCommon> Handle(GetorderCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetorderValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (order == null)
            throw new KeyNotFoundException($"Sale order with ID {request.Id} not found");

        return _mapper.Map<GetorderResultCommon>(order);
    }
}
