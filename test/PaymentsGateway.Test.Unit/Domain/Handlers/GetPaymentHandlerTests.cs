using FluentAssertions;
using Moq;
using PaymentsGateway.Domain.Handlers;
using PaymentsGateway.Domain.Models;
using PaymentsGateway.Domain.Ports;
using Xunit;

namespace PaymentsGateway.Test.Unit.Domain.Handlers
{
    public class GetPaymentHandlerTests
    {
        private readonly GetPaymentHandler _sut;
        private readonly Mock<IGetPaymentQuery> _getPaymentQuery = new();

        public GetPaymentHandlerTests()
        {
            _sut = new GetPaymentHandler(_getPaymentQuery.Object);
        }

        [Fact]
        public async Task Should_Retrun_Internal_Error()
        {
            // Arrange
            _getPaymentQuery.Setup(x => x.GetPayment(It.IsAny<GetPaymentRequest>()))
                .ReturnsAsync(() => new GetPaymentResponse.InternalError());

            // Act
            var result = await _sut.HandleAsync(new GetPaymentRequest(Guid.NewGuid()));

            // Assert
            result.IsInternalError.Should().BeTrue();
        }

        [Fact]
        public async Task Should_Retrun_NotFound_Error()
        {
            // Arrange
            _getPaymentQuery.Setup(x => x.GetPayment(It.IsAny<GetPaymentRequest>()))
                .ReturnsAsync(() => new GetPaymentResponse.NotFound());

            // Act
            var result = await _sut.HandleAsync(new GetPaymentRequest(Guid.NewGuid()));

            // Assert
            result.IsNotFound.Should().BeTrue();
        }
    }
}
