using FluentAssertions;

using PaymentGateway.Api.Mappers;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Models.Enum;

using Xunit;

namespace PaymentGateway.Test.Unit.API.Mappers;

public class DomainToApiMapperTests
{
    [Fact]
    public void DomainPaymentDetails_ShouldMapToApiPostPaymentResponse()
    {
        // Arrange
        var paymentDetails = new PaymentDetails(
            Guid.NewGuid(),
            PaymentStatus.Authorized,
            CardNumber: "1234567898765432",
            Currency.GBP,
            ExpiryMonth: 10,
            ExpiryYear: 2028,
            Amount: 100);

        // Act
        var result = paymentDetails.ToPostPaymentResponse();

        // Assert
        result.Id.Should().Be(paymentDetails.Id);
        result.Status.Should().Be(paymentDetails.Status.ToString());
        result.CardNumberLastFour.Should().Be(paymentDetails.CardNumber[^4..]);
        result.Amount.Should().Be(paymentDetails.Amount);
        result.ExpiryMonth.Should().Be(paymentDetails.ExpiryMonth);
        result.ExpiryYear.Should().Be(paymentDetails.ExpiryYear);
    }

    [Fact]
    public void DomainPaymentDetails_ShouldMapToApiGetPaymentResponse()
    {
        // Arrange
        var paymentDetails = new PaymentDetails(
            Guid.NewGuid(),
            PaymentStatus.Declined,
            CardNumber: "1234567898765432",
            Currency.USD,
            ExpiryMonth: 10,
            ExpiryYear: 2028,
            Amount: 100);

        // Act
        var result = paymentDetails.ToGetPaymentResponse();

        // Assert
        result.Id.Should().Be(paymentDetails.Id);
        result.Status.Should().Be(paymentDetails.Status.ToString());
        result.CardNumberLastFour.Should().Be(paymentDetails.CardNumber[^4..]);
        result.Amount.Should().Be(paymentDetails.Amount);
        result.ExpiryMonth.Should().Be(paymentDetails.ExpiryMonth);
        result.ExpiryYear.Should().Be(paymentDetails.ExpiryYear);
    }
}