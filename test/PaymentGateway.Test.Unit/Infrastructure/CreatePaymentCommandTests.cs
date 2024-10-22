using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Models.Enum;
using PaymentGateway.Infrastructure;
using PaymentGateway.Infrastructure.Database.Entities;
using PaymentGateway.Infrastructure.ExternalServices;
using PaymentGateway.Infrastructure.ExternalServices.Models;
using PaymentGateway.Infrastructure.Repositories;
using Xunit;

namespace PaymentGateway.Test.Unit.Infrastructure
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
        public async Task CreatePayment_ShouldRetrunInternalError_WhenInsertAsyncFailed()
        {
            // Arrange
            _paymentRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<CreatePaymentRequest>()));

            var paymentRequest = CreatePaymentRequest();

            // Act
            var result = await _sut.CreatePayment(paymentRequest);

            // Assert
            result.IsInternalError.Should().BeTrue();
            _paymentRepositoryMock.Verify(d => d.InsertAsync(It.IsAny<CreatePaymentRequest>()), Times.Once());
            _acquiringBankClient.Verify(d => d.ProcessPayment(It.IsAny<PaymentRequest>()), Times.Never());
        }

        [Fact]
        public async Task CreatePayment_ShouldRetrunInternalError_WhenProcessPaymentFailed()
        {
            // Arrange
            _paymentRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<CreatePaymentRequest>()))
                .ReturnsAsync(() => new Payment());

            _acquiringBankClient.Setup(x => x.ProcessPayment(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(() => new PaymentResult.Error());

            var paymentRequest = CreatePaymentRequest();

            // Act
            var result = await _sut.CreatePayment(paymentRequest);

            // Assert
            result.IsInternalError.Should().BeTrue();
            _paymentRepositoryMock.Verify(d => d.InsertAsync(It.IsAny<CreatePaymentRequest>()), Times.Once());
            _acquiringBankClient.Verify(d => d.ProcessPayment(It.IsAny<PaymentRequest>()), Times.Once());
        }

        [Fact]
        public async Task CreatePayment_ShouldRetrunSuccess_WhenPaymentAuthorized()
        {
            // Arrange
            var paymentStatus = PaymentStatus.Authorized;
            _paymentRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<CreatePaymentRequest>()))
                .ReturnsAsync(() => new Payment());

            _acquiringBankClient.Setup(x => x.ProcessPayment(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(() => new PaymentResult.Success(
                    Authorized: true,
                    AuthorizationCode: Guid.NewGuid().ToString()));

            _paymentRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Payment>(), It.IsAny<string>()))
                .ReturnsAsync(() => new Payment() { Status = paymentStatus });

            var paymentRequest = CreatePaymentRequest();

            // Act
            var result = await _sut.CreatePayment(paymentRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.AsSuccess.PaymentDetails.Status.Should().Be(paymentStatus);
            _paymentRepositoryMock.Verify(d => d.InsertAsync(It.IsAny<CreatePaymentRequest>()), Times.Once());
            _acquiringBankClient.Verify(d => d.ProcessPayment(It.IsAny<PaymentRequest>()), Times.Once());
        }

        [Fact]
        public async Task CreatePayment_ShouldRetrunSuccess_WhenPaymentDeclined()
        {
            // Arrange
            var paymentStatus = PaymentStatus.Declined;
            _paymentRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<CreatePaymentRequest>()))
                .ReturnsAsync(() => new Payment());

            _acquiringBankClient.Setup(x => x.ProcessPayment(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(() => new PaymentResult.Success(
                    Authorized: false,
                    AuthorizationCode: ""));

            _paymentRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Payment>(), It.IsAny<string>()))
                .ReturnsAsync(() => new Payment() { Status = paymentStatus });

            var paymentRequest = CreatePaymentRequest();

            // Act
            var result = await _sut.CreatePayment(paymentRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.AsSuccess.PaymentDetails.Status.Should().Be(paymentStatus);
            _paymentRepositoryMock.Verify(d => d.InsertAsync(It.IsAny<CreatePaymentRequest>()), Times.Once());
            _acquiringBankClient.Verify(d => d.ProcessPayment(It.IsAny<PaymentRequest>()), Times.Once());
        }

        private CreatePaymentRequest CreatePaymentRequest()
        {
            return new CreatePaymentRequest("1111111111111111", 10, 2028, 10, Currency.GBP, "123");
        }
    }
}