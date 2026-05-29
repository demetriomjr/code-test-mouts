using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class NotCancelledSaleOrderSpecificationTests
{
    [Theory]
    [InlineData(CancelStatus.NotCancelled, true)]
    [InlineData(CancelStatus.Cancelled, false)]
    public void IsSatisfiedBy_ShouldValidateCancelStatus(CancelStatus cancelStatus, bool expectedResult)
    {
        // Arrange
        var saleOrder = NotCancelledSaleOrderSpecificationTestData.GenerateSaleOrder(cancelStatus);
        var specification = new NotCancelledSaleOrderSpecification();

        // Act
        var result = specification.IsSatisfiedBy(saleOrder);

        // Assert
        result.Should().Be(expectedResult);
    }
}
