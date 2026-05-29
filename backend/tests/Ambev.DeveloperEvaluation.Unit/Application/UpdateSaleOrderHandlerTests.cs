using Ambev.DeveloperEvaluation.Application.SaleOrders.UpdateSaleOrder;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class UpdateSaleOrderHandlerTests
{
    private readonly ISaleOrderRepository _saleOrderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSaleOrderHandler> _logger;
    private readonly UpdateSaleOrderHandler _handler;

    public UpdateSaleOrderHandlerTests()
    {
        _saleOrderRepository = Substitute.For<ISaleOrderRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<UpdateSaleOrderHandler>>();
        _handler = new UpdateSaleOrderHandler(_saleOrderRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid sale order data When updating sale order Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = UpdateSaleOrderHandlerTestData.GenerateValidCommand();
        var existingOrder = BuildExistingOrder(command.Id);
        var mappedItems = BuildMappedItems(command.Products, existingOrder.Id);
        var mappedResult = new UpdateSaleOrderResult { Id = existingOrder.Id, OrderNumber = existingOrder.OrderNumber };

        _saleOrderRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingOrder);
        _mapper.Map<List<SaleOrderItem>>(command.Products).Returns(mappedItems);
        _saleOrderRepository.UpdateAsync(Arg.Any<SaleOrder>(), Arg.Any<CancellationToken>()).Returns(existingOrder);
        _mapper.Map<UpdateSaleOrderResult>(existingOrder).Returns(mappedResult);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(existingOrder.Id);
        await _saleOrderRepository.Received(1).UpdateAsync(Arg.Any<SaleOrder>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid sale order data When updating sale order Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = UpdateSaleOrderHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given missing sale order When updating Then throws key not found exception")]
    public async Task Handle_OrderNotFound_ThrowsKeyNotFoundException()
    {
        // Given
        var command = UpdateSaleOrderHandlerTestData.GenerateValidCommand();
        _saleOrderRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((SaleOrder?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    private static SaleOrder BuildExistingOrder(Guid id)
    {
        return new SaleOrder
        {
            Id = id,
            OrderNumber = 1001,
            CustomerName = "Customer Current",
            BranchName = "Branch Current",
            CancelStatus = CancelStatus.NotCancelled,
            Products =
            [
                new SaleOrderItem
                {
                    Id = Guid.NewGuid(),
                    SaleOrderId = id,
                    EanGtin = "7891234567895",
                    Description = "Current Item",
                    Price = 10m,
                    Amount = 1,
                    CancelStatus = CancelStatus.NotCancelled
                }
            ]
        };
    }

    private static List<SaleOrderItem> BuildMappedItems(IEnumerable<UpdateSaleOrderItemCommand> products, Guid saleOrderId)
    {
        return products.Select(x => new SaleOrderItem
        {
            Id = Guid.NewGuid(),
            SaleOrderId = saleOrderId,
            EanGtin = x.EanGtin,
            Description = x.Description,
            Price = x.Price,
            Amount = x.Amount,
            CancelStatus = x.CancelStatus
        }).ToList();
    }
}
