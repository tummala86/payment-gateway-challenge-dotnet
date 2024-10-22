
using FluentAssertions;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Extensions;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Models.Enum;
using Xunit;

namespace PaymentGateway.Test.Unit.API.Extensions;

public class CreatePaymentRequestExtensionsTests
{
    [Fact]
    public void ApiPostPaymentRequest_ShouldMapToDomainCreatePaymentResponse()
    {
        // Arrange
        var postPaymentRequest = new PostPaymentRequest(
            CardNumber: "1234567898765432",
            ExpiryMonth: 10,
            ExpiryYear: 2028,
            Currency: "GBP",
            Amount: 100,
            Cvv: "123");

        // Act
        var result = postPaymentRequest.ToDomain();

        // Assert
        result.CardNumber.Should().Be(postPaymentRequest.CardNumber);
        result.ExpiryMonth.Should().Be(postPaymentRequest.ExpiryMonth);
        result.ExpiryYear.Should().Be(postPaymentRequest.ExpiryYear);
        result.Currency.Should().Be(Currency.GBP);
        result.Amount.Should().Be(postPaymentRequest.Amount);
        result.Cvv.Should().Be(postPaymentRequest.Cvv);
    }
}