using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Models.Enum;
using PaymentGateway.Infrastructure;
using PaymentGateway.Infrastructure.Database.Entities;
using PaymentGateway.Infrastructure.Repositories;
using Xunit;

namespace PaymentGateway.Test.Unit.Infrastructure
{
    public class GetPaymentQueryTests
    {
        private readonly GetPaymentQuery _sut;
        private readonly Mock<IPaymentRepository> _paymentRepository = new();
        private readonly Mock<ILogger<GetPaymentQuery>> _logger = new();

        public GetPaymentQueryTests()
        {
            _sut = new GetPaymentQuery(_paymentRepository.Object, _logger.Object);
        }

        [Fact]
        public async Task GetPayment_ShouldRetrunPaymentNotFound_WhenPaymentDoesnotExists()
        {
            // Arrange
            _paymentRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()));

            var getPaymentRequest = new GetPaymentRequest(Guid.NewGuid());

            // Act
            var result = await _sut.GetPayment(getPaymentRequest);

            // Assert
            result.IsNotFound.Should().BeTrue();
        }

        [Fact]
        public async Task GetPayment_ShouldRetrunSuccess_WhenPaymentExists()
        {
            // Arrange
            var paymentId = Guid.NewGuid();

            _paymentRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => new Payment()
                {
                    Id = paymentId,
                    CardNumber = "1234567898765432",
                    Cvv = "123",
                    ExpiryYear = 2028,
                    ExpiryMonth = 10,
                    Amount = 200,
                    Currency = Currency.GBP,
                    Status = PaymentStatus.Authorized
                });

            var getPaymentRequest = new GetPaymentRequest(paymentId);

            // Act
            var result = await _sut.GetPayment(getPaymentRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.AsSuccess.PaymentDetails.Id.Should().Be(paymentId);
            result.AsSuccess.PaymentDetails.Status.Should().Be(PaymentStatus.Authorized);
        }
    }
}