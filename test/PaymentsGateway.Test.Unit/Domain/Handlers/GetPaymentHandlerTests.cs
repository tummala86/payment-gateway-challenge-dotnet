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
        public async Task HandleAsync_ShouldRetrunInternalError_WhenGetPaymentFailed()
        {
            // Arrange
            _getPaymentQuery.Setup(x => x.GetPayment(It.IsAny<GetPaymentRequest>()))
                .ReturnsAsync(() => new GetPaymentResponse.InternalError());

            // Act
            var result = await _sut.HandleAsync(new GetPaymentRequest(Guid.NewGuid()));

            // Assert
            result.IsInternalError.Should().BeTrue();
            _getPaymentQuery.Verify(d => d.GetPayment(It.IsAny<GetPaymentRequest>()), Times.Once());
        }

        [Fact]
        public async Task HandleAsync_ShouldRetrunNotFoundError_WhenPaymentDoesnotExist()
        {
            // Arrange
            _getPaymentQuery.Setup(x => x.GetPayment(It.IsAny<GetPaymentRequest>()))
                .ReturnsAsync(() => new GetPaymentResponse.NotFound());

            // Act
            var result = await _sut.HandleAsync(new GetPaymentRequest(Guid.NewGuid()));

            // Assert
            result.IsNotFound.Should().BeTrue();
            _getPaymentQuery.Verify(d => d.GetPayment(It.IsAny<GetPaymentRequest>()), Times.Once());
        }
    }
}
