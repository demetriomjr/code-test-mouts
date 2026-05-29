using Ambev.DeveloperEvaluation.Application.SaleOrders.CreateSaleOrder;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for <see cref="CreateSaleOrderHandler"/>.
/// </summary>
public class CreateSaleOrderHandlerTests
{
    private readonly ISaleOrderRepository _saleOrderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleOrderHandler> _logger;
    private readonly CreateSaleOrderHandler _handler;

    /// <summary>
    /// Initializes a new instance of <see cref="CreateSaleOrderHandlerTests"/>.
    /// </summary>
    public CreateSaleOrderHandlerTests()
    {
        _saleOrderRepository = Substitute.For<ISaleOrderRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateSaleOrderHandler>>();
        _handler = new CreateSaleOrderHandler(_saleOrderRepository, _mapper, _logger);
    }

    /// <summary>
    /// Tests that a valid create command returns a successful result.
    /// </summary>
    [Fact(DisplayName = "Given valid sale order data When creating sale order Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSaleOrderHandlerTestData.GenerateValidCommand();
        var order = BuildOrderFromCommand(command, 1001);
        var result = new CreateSaleOrderResult { Id = order.Id, OrderNumber = order.OrderNumber };
        SetupValidFlow(command, order, result);

        // When
        var createResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createResult.Should().NotBeNull();
        createResult.Id.Should().Be(order.Id);
        await _saleOrderRepository.Received(1).CreateAsync(Arg.Any<SaleOrder>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid create command throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale order data When creating sale order Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleOrderCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that discount and total values are applied before persistence.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then applies discount and total before persisting")]
    public async Task Handle_ValidRequest_AppliesDiscountAndTotalBeforeSaving()
    {
        // Given
        var unitPrice = 10m;
        var amount = 4;
        var expectedDiscount = 10;
        var expectedTotal = (unitPrice * amount) * (1 - (expectedDiscount / 100m));

        var command = new CreateSaleOrderCommand
        {
            CustomerName = "Customer Test",
            BranchName = "Branch Test",
            Products =
            [
                new CreateSaleOrderItemCommand
                {
                    EanGtin = "7891234567895",
                    Description = "Product Test",
                    Price = unitPrice,
                    Amount = amount
                }
            ]
        };

        var order = BuildOrderFromCommand(command, 2001);
        SetupValidFlow(command, order, new CreateSaleOrderResult { Id = order.Id, OrderNumber = order.OrderNumber });

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _saleOrderRepository.Received(1).CreateAsync(
            Arg.Is<SaleOrder>(o =>
                o.TotalSale == expectedTotal &&
                o.Products.First().Discount == expectedDiscount &&
                o.Products.First().TotalValue == expectedTotal),
            Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Builds a sale order entity from a create command.
    /// </summary>
    /// <param name="command">The source command.</param>
    /// <param name="orderNumber">The order number to assign.</param>
    /// <returns>A mapped <see cref="SaleOrder"/> entity.</returns>
    private static SaleOrder BuildOrderFromCommand(CreateSaleOrderCommand command, int orderNumber)
    {
        return new SaleOrder
        {
            Id = Guid.NewGuid(),
            OrderNumber = orderNumber,
            CustomerName = command.CustomerName,
            BranchName = command.BranchName,
            Products = command.Products.Select(x => new SaleOrderItem
            {
                EanGtin = x.EanGtin,
                Description = x.Description,
                Price = x.Price,
                Amount = x.Amount
            }).ToList()
        };
    }

    /// <summary>
    /// Sets up mapper and repository mocks for a valid create flow.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <param name="order">The mapped order entity.</param>
    /// <param name="result">The expected handler result.</param>
    private void SetupValidFlow(CreateSaleOrderCommand command, SaleOrder order, CreateSaleOrderResult result)
    {
        _mapper.Map<SaleOrder>(command).Returns(order);
        _mapper.Map<CreateSaleOrderResult>(order).Returns(result);
        _saleOrderRepository.CreateAsync(Arg.Any<SaleOrder>(), Arg.Any<CancellationToken>()).Returns(order);
    }
}
