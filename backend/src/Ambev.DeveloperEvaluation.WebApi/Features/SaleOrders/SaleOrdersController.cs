using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.CreateSaleOrder;
using Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders;

/// <summary>
/// Controller for managing sale order operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SaleOrdersController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SaleOrdersController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SaleOrdersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale order
    /// </summary>
    /// <param name="request">The sale order creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale order details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleOrderResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSaleOrder([FromBody] CreateSaleOrderRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleOrderRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleOrderCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleOrderResponse>
        {
            Success = true,
            Message = "Sale order created successfully",
            Data = _mapper.Map<CreateSaleOrderResponse>(response)
        });
    }
}
