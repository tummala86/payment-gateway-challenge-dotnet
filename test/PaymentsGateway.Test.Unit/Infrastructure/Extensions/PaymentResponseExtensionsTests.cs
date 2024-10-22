using FluentAssertions;
using PaymentsGateway.Domain.Models.Enum;
using PaymentsGateway.Infrastructure.Database.Entities;
using PaymentsGateway.Infrastructure.Extensions;
using Xunit;

namespace PaymentsGateway.Test.Unit.Infrastructure.Extensions
{
    public class PaymentResponseExtensionsTests
    {
        [Fact]
        public void PaymentEntity_ShouldMapToDomainPaymentDetails()
        {
            // Arrange
            var paymentId = Guid.NewGuid();
            var bankAuthCode = Guid.NewGuid().ToString();

            var payment = new Payment()
            {
                Id = paymentId,
                CardNumber = "1234567898765432",
                Cvv = "123",
                Amount = 100,
                Status = PaymentStatus.Authorized,
                BankAuthorizationCode = bankAuthCode,
                CreatedAt = DateTime.UtcNow,
                ExpiryMonth = 10,
                ExpiryYear = 2028
            };

            // Act
            var result = payment.ToDomainPaymentDetails();

            // Assert
            result.Id.Should().Be(payment.Id);
            result.Status.Should().Be(PaymentStatus.Authorized);
            result.CardNumber.Should().Be(payment.CardNumber);
            result.Amount.Should().Be(payment.Amount);
            result.ExpiryMonth.Should().Be(payment.ExpiryMonth);
            result.ExpiryYear.Should().Be(payment.ExpiryYear);
        }
    }
}