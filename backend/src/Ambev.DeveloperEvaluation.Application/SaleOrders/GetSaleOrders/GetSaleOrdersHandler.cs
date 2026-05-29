using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrders;

/// <summary>
/// Handler for processing GetSaleOrdersCommand requests
/// </summary>
public class GetSaleOrdersHandler : IRequestHandler<GetSaleOrdersCommand, GetSaleOrdersResult>
{
    private readonly ISaleOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetSaleOrdersHandler
    /// </summary>
    /// <param name="orderRepository">The sale order repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetSaleOrdersHandler(ISaleOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSaleOrdersCommand request
    /// </summary>
    /// <param name="command">The GetSaleOrders command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of sale orders</returns>
    public async Task<GetSaleOrdersResult> Handle(GetSaleOrdersCommand command, CancellationToken cancellationToken)
    {
        var repoResult = await _orderRepository.GetOrders(command.CurrentPage, command.PageSize, cancellationToken);
        var orders = _mapper.Map<IEnumerable<GetSaleOrderResultCommon>>(repoResult.orders);
        return new GetSaleOrdersResult
        {
            Orders = orders,
            TotalPages = repoResult.totalPages,
            CurrentPage = command.CurrentPage
        };
    }
}