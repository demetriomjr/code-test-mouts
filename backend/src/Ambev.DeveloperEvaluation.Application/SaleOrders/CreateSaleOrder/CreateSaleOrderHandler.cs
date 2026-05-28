using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateSaleOrder;

public class CreateSaleOrderHandler : IRequestHandler<CreateSaleOrderCommand, CreateSaleOrderResult>
{
    //private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ISaleOrderRepository _orderRepository;

    public CreateUserHandler(IUserRepository orderRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Handles the CreateSaleOrderCommand request
    /// </summary>
    /// <param name="command">The CreateSaleOrder command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale order details</returns>
    public async Task<CreateUserResult> Handle(CreateSaleOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleOrderValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        int nextOrderNumber = await _orderRepository.GetLastOrderNumber + 1; //not good, beter use an incremental number generator
        var order = _mapper.Map<SaleOrder>(command);
        var createdOrder = await _orderRepository.CreateAsync(order);
        var result = _mapper.Map<CreatedOrderResult>(createdOrder);
        return result;
    }
}