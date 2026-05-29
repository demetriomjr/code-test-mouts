using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.CreateSaleOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.DeleteSaleOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.GetSaleOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.GetSaleOrders;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleOrders.UpdateSaleOrder;
using Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;
using Ambev.DeveloperEvaluation.Application.SaleOrders.DeleteSaleOrder;
using Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;
using Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrders;
using Ambev.DeveloperEvaluation.Application.SaleOrders.UpdateSaleOrder;

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

    /// <summary>
    /// Retrieves a paginated list of sale orders
    /// </summary>
    /// <param name="request">The pagination parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of sale orders</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleOrdersResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSaleOrders([FromQuery] GetSaleOrdersRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<GetSaleOrdersCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetSaleOrdersResponse>
        {
            Success = true,
            Message = "Sale orders retrieved successfully",
            Data = _mapper.Map<GetSaleOrdersResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a sale order by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the sale order.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The sale order details if found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleOrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleOrder([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleOrderRequest { Id = id };
        var validator = new GetSaleOrderRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetSaleOrderCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetSaleOrderResponse>
        {
            Success = true,
            Message = "Sale order retrieved successfully",
            Data = _mapper.Map<GetSaleOrderResponse>(response)
        });
    }

    /// <summary>
    /// Updates an existing sale order.
    /// </summary>
    /// <param name="id">The unique identifier of the sale order to update.</param>
    /// <param name="request">The sale order update request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated sale order details.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleOrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSaleOrder([FromRoute] Guid id, [FromBody] UpdateSaleOrderRequest request, CancellationToken cancellationToken)
    {
        request.Id = id;
        var validator = new UpdateSaleOrderRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateSaleOrderCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<UpdateSaleOrderResponse>
        {
            Success = true,
            Message = "Sale order updated successfully",
            Data = _mapper.Map<UpdateSaleOrderResponse>(response)
        });
    }

    /// <summary>
    /// Deletes a sale order by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the sale order to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Success response if the sale order was deleted.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSaleOrder([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleOrderRequest { Id = id };
        var validator = new DeleteSaleOrderRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleOrderCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale order deleted successfully"
        });
    }
}
