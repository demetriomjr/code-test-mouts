using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrders;

public class GetSaleOrdersHandler : IRequestHandler<GetSaleOrdersCommand, GetSaleOrdersResult>
{
    private readonly ISaleOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    public GetSaleOrdersHandler(ISaleOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

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