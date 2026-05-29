using Ambev.DeveloperEvaluation.Application.SaleOrders.DeleteSaleOrder;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class DeleteSaleOrderHandlerTests
{
    private readonly ISaleOrderRepository _saleOrderRepository;
    private readonly ILogger<DeleteSaleOrderHandler> _logger;
    private readonly DeleteSaleOrderHandler _handler;

    public DeleteSaleOrderHandlerTests()
    {
        _saleOrderRepository = Substitute.For<ISaleOrderRepository>();
        _logger = Substitute.For<ILogger<DeleteSaleOrderHandler>>();
        _handler = new DeleteSaleOrderHandler(_saleOrderRepository, _logger);
    }

    [Fact(DisplayName = "Given valid id When deleting sale order Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = DeleteSaleOrderHandlerTestData.GenerateValidCommand();
        var order = new SaleOrder { Id = command.Id, OrderNumber = 1001 };

        _saleOrderRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(order);
        _saleOrderRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(true);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        await _saleOrderRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid id When deleting sale order Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = DeleteSaleOrderHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given missing sale order When deleting Then throws key not found exception")]
    public async Task Handle_OrderNotFound_ThrowsKeyNotFoundException()
    {
        // Given
        var command = DeleteSaleOrderHandlerTestData.GenerateValidCommand();
        _saleOrderRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((SaleOrder?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
