using FluentAssertions;
using Moq;
using PaymentsGateway.Domain.Handlers;
using PaymentsGateway.Domain.Models;
using PaymentsGateway.Domain.Models.Enum;
using PaymentsGateway.Domain.Ports;
using Xunit;

namespace PaymentsGateway.Test.Unit.Domain.Handlers
{
    public class CreatePaymentHandlerTests
    {
        private readonly CreatePaymentHandler _sut;
        private readonly Mock<ICreatePaymentCommand> _createPaymentCommandMock = new();

        public CreatePaymentHandlerTests()
        {
            _sut = new CreatePaymentHandler(_createPaymentCommandMock.Object);
        }

        [Fact]
        public async Task Should_Retrun_Internal_Error()
        {
            // Arrange
            _createPaymentCommandMock.Setup(x => x.CreatePayment(It.IsAny<CreatePaymentRequest>()))
                .ReturnsAsync(() => new CreatePaymentResponse.InternalError());

            // Act
            var result = await _sut.HandleAsync(CreatePaymentRequest());

            // Assert
            result.IsInternalError.Should().BeTrue();
        }

        [Fact]
        public async Task Should_Retrun_Success()
        {
            // Arrange
            _createPaymentCommandMock.Setup(x => x.CreatePayment(It.IsAny<CreatePaymentRequest>()))
                .ReturnsAsync(() => new CreatePaymentResponse.Success(new PaymentDetails(Guid.NewGuid(), 
                PaymentStatus.Authorized,
                "1234567891234567",
                Currency.GBP,
                10,
                2028,
                200)));

            // Act
            var result = await _sut.HandleAsync(CreatePaymentRequest());

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        private CreatePaymentRequest CreatePaymentRequest()
        {
            return new CreatePaymentRequest(10, Currency.GBP, "1111111111111111", 10, 2023, "123");
        }
    }
}
