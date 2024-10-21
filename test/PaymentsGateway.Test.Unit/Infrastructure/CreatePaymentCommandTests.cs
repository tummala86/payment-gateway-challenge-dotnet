using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentsGateway.Domain.Models;
using PaymentsGateway.Domain.Models.Enum;
using PaymentsGateway.Infrastructure;
using PaymentsGateway.Infrastructure.Database.Entities;
using PaymentsGateway.Infrastructure.ExternalServices;
using PaymentsGateway.Infrastructure.ExternalServices.Models;
using PaymentsGateway.Infrastructure.Repositories;
using Xunit;

namespace PaymentsGateway.Test.Unit.Infrastructure
{
    public class CreatePaymentCommandTests
    {
        private readonly CreatePaymentCommand _sut;
        private readonly Mock<IPaymentRepository> _paymentRepositoryMock = new();
        private readonly Mock<IAcquiringBankClient> _acquiringBankClient = new();
        private readonly Mock<ILogger<CreatePaymentCommand>> _logger = new();

        public CreatePaymentCommandTests()
        {
            _sut = new CreatePaymentCommand(_paymentRepositoryMock.Object, _acquiringBankClient.Object, _logger.Object);
        }

        [Fact]
        public async Task CreatePayment_Should_Retrun_Internal_Error()
        {
            // Arrange
            _paymentRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<CreatePaymentRequest>()));

            // Act
            var result = await _sut.CreatePayment(CreatePaymentRequest());

            // Assert
            result.IsInternalError.Should().BeTrue();
        }

        [Theory]
        [InlineData(PaymentStatus.Authorized)]
        public async Task CreatePayment_Should_Retrun_Success(
            PaymentStatus paymentStatus)
        {
            // Arrange
            _paymentRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<CreatePaymentRequest>()))
                .ReturnsAsync(() => new Payment());

            _acquiringBankClient.Setup(x => x.ProcessPayment(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(() => new PaymentResults.Success(
                    Authorized: true,
                    AuthorizationCode: Guid.NewGuid().ToString()));

            _paymentRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Payment>(), It.IsAny<string>()))
                .ReturnsAsync(() => new Payment() { Status = paymentStatus });

            // Act
            var result = await _sut.CreatePayment(CreatePaymentRequest());

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.AsSuccess.PaymentDetails.Status.Should().Be(paymentStatus);
        }

        private CreatePaymentRequest CreatePaymentRequest()
        {
            return new CreatePaymentRequest(10, Currency.GBP, "1111111111111111", 10, 2028, "123");
        }
    }
}
