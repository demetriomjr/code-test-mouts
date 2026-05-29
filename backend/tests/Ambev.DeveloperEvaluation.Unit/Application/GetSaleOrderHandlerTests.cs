using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;
using Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrder;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for <see cref="GetSaleOrderHandler"/>.
/// </summary>
public class GetSaleOrderHandlerTests
{
    private readonly ISaleOrderRepository _saleOrderRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleOrderHandler _handler;

    /// <summary>
    /// Initializes a new instance of <see cref="GetSaleOrderHandlerTests"/>.
    /// </summary>
    public GetSaleOrderHandlerTests()
    {
        _saleOrderRepository = Substitute.For<ISaleOrderRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleOrderHandler(_saleOrderRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid id returns a mapped sale order result.
    /// </summary>
    [Fact(DisplayName = "Given valid id When getting sale order Then returns mapped result")]
    public async Task Handle_ValidRequest_ReturnsMappedResult()
    {
        // Given
        var command = GetSaleOrderHandlerTestData.GenerateValidCommand();
        var order = new SaleOrder
        {
            Id = command.Id,
            OrderNumber = 1001,
            CustomerName = "Customer Test",
            BranchName = "Branch Test"
        };
        var mappedResult = new GetSaleOrderResultCommon
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            CustomerName = order.CustomerName,
            BranchName = order.BranchName
        };

        _saleOrderRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(order);
        _mapper.Map<GetSaleOrderResultCommon>(order).Returns(mappedResult);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(order.Id);
        result.OrderNumber.Should().Be(order.OrderNumber);
    }

    /// <summary>
    /// Tests that an invalid id throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid id When getting sale order Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = GetSaleOrderHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that missing sale order data throws key not found exception.
    /// </summary>
    [Fact(DisplayName = "Given missing sale order When getting by id Then throws key not found exception")]
    public async Task Handle_OrderNotFound_ThrowsKeyNotFoundException()
    {
        // Given
        var command = GetSaleOrderHandlerTestData.GenerateValidCommand();
        _saleOrderRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((SaleOrder?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
