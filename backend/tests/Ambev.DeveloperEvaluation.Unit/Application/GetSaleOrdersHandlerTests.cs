using Ambev.DeveloperEvaluation.Application.SaleOrders.Common;
using Ambev.DeveloperEvaluation.Application.SaleOrders.GetSaleOrders;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class GetSaleOrdersHandlerTests
{
    private readonly ISaleOrderRepository _saleOrderRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleOrdersHandler _handler;

    public GetSaleOrdersHandlerTests()
    {
        _saleOrderRepository = Substitute.For<ISaleOrderRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleOrdersHandler(_saleOrderRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid filters When listing sale orders Then returns paginated result")]
    public async Task Handle_ValidRequest_ReturnsPaginatedResult()
    {
        // Given
        var command = GetSaleOrdersHandlerTestData.GenerateValidCommand();
        var repoOrders = new List<SaleOrder>
        {
            new() { Id = Guid.NewGuid(), OrderNumber = 1001, CustomerName = "Customer 1", BranchName = "Branch 1" }
        };
        var mappedOrders = new List<GetSaleOrderResultCommon>
        {
            new() { Id = repoOrders[0].Id, OrderNumber = repoOrders[0].OrderNumber, CustomerName = repoOrders[0].CustomerName, BranchName = repoOrders[0].BranchName }
        };

        _saleOrderRepository.GetOrders(
            Arg.Any<int>(),
            Arg.Any<int>(),
            Arg.Any<bool>(),
            Arg.Any<int?>(),
            Arg.Any<int?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<Ambev.DeveloperEvaluation.Domain.Enums.CancelStatus?>(),
            Arg.Any<DateTime?>(),
            Arg.Any<DateTime?>(),
            Arg.Any<string?>(),
            Arg.Any<CancellationToken>())
            .Returns((2, repoOrders.AsEnumerable()));

        _mapper.Map<IEnumerable<GetSaleOrderResultCommon>>(repoOrders).Returns(mappedOrders);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.CurrentPage.Should().Be(command.CurrentPage);
        result.TotalPages.Should().Be(2);
        result.Orders.Should().NotBeNull();
        result.Orders.Should().HaveCount(1);
    }

    [Fact(DisplayName = "Given invalid command When listing sale orders Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetSaleOrdersCommand { CurrentPage = 0, PageSize = 0 };

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given command filters When listing sale orders Then forwards filters to repository")]
    public async Task Handle_ValidRequest_ForwardsFiltersToRepository()
    {
        // Given
        var command = GetSaleOrdersHandlerTestData.GenerateValidCommand();
        var repoOrders = new List<SaleOrder>();
        _saleOrderRepository.GetOrders(
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<bool>(),
                Arg.Any<int?>(),
                Arg.Any<int?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<Ambev.DeveloperEvaluation.Domain.Enums.CancelStatus?>(),
                Arg.Any<DateTime?>(),
                Arg.Any<DateTime?>(),
                Arg.Any<string?>(),
                Arg.Any<CancellationToken>())
            .Returns((0, repoOrders.AsEnumerable()));
        _mapper.Map<IEnumerable<GetSaleOrderResultCommon>>(repoOrders).Returns(new List<GetSaleOrderResultCommon>());

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _saleOrderRepository.Received(1).GetOrders(
            command.CurrentPage,
            command.PageSize,
            command.IncludeProductList,
            command.OrderNumberFrom,
            command.OrderNumberTo,
            command.CustomerName,
            command.BranchName,
            command.CancelStatus,
            command.DateFrom,
            command.DateTo,
            command.OrderBy,
            Arg.Any<CancellationToken>());
    }
}
